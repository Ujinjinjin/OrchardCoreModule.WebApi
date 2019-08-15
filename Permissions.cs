using System.Collections.Generic;
using OrchardCore.Security.Permissions;

namespace OrchardCore.WebApi
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ExecuteWebApi = new Permission("ExecuteWebApi", "Execute WebApi.");

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
                ExecuteWebApi
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = GetPermissions()
                }
            };
        }
    }
}