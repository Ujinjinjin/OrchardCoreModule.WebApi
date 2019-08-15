using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using OrchardCore.Apis.GraphQL;

namespace OrchardCore.WebApi
{
    public class WebApiMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebApiSettings _settings;
        private readonly IDocumentExecuter _executer;
        private readonly IDocumentWriter _writer;

        private readonly static JsonSerializer _serializer = new JsonSerializer();
        private readonly static MediaType _jsonMediaType = new MediaType("application/json");

        public WebApiMiddleware(
            RequestDelegate next,
            WebApiSettings settings,
            IDocumentExecuter executer,
            IDocumentWriter writer)
        {
            _next = next;
            _settings = settings;
            _executer = executer;
            _writer = writer;
        }

        public async Task Invoke(HttpContext context, IAuthorizationService authorizationService, IAuthenticationService authenticationService, ISchemaFactory schemaService)
        {
            if (!IsWebApiRequest(context))
            {
                await _next(context);
            }
            else
            {
                var principal = context.User;

                var authenticateResult = await authenticationService.AuthenticateAsync(context, "Api");

                if (authenticateResult.Succeeded)
                {
                    principal = authenticateResult.Principal;
                }

                var authorized = await authorizationService.AuthorizeAsync(principal, Permissions.ExecuteWebApi);

                if (authorized)
                {
                    await ExecuteAsync(context, schemaService);
                }
                else
                {
                    await context.ChallengeAsync("Api");
                }
            }
        }

        private bool IsWebApiRequest(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments(_settings.Path);
        }

        private async Task ExecuteAsync(HttpContext context, ISchemaFactory schemaService)
        {
            var schema = await schemaService.GetSchemaAsync();

            WebAdminRequest request = null;

            if (HttpMethods.IsPost(context.Request.Method))
            {
                var mediaType = new MediaType(context.Request.ContentType);

                try
                {
                    using (var sr = new StreamReader(context.Request.Body))
                    {
                        using (var jsonTextReader = new JsonTextReader(sr))
                        {
                            request = _serializer.Deserialize<WebAdminRequest>(jsonTextReader);
                        }
                    }
                }
                catch (Exception e)
                {
                    await WriteErrorAsync(context, "An error occurred while processing the GraphQL query", e);
                    return;
                }
            }
        }

        private async Task WriteErrorAsync(HttpContext context, string message, Exception e = null)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var errorResult = new ExecutionResult
            {
                Errors = new ExecutionErrors
                {
                    e == null ? new ExecutionError(message) : new ExecutionError(message, e)
                }
            };

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            await _writer.WriteAsync(context.Response.Body, errorResult);
        }
    }
}