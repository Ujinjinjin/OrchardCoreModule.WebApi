# OrchardCoreModule.WebApi

Custom made web api module for OrchardCore.Cms gnerally used as synchronization entry point. E.g. you have main back-end service and CMS (in this case OrchardCore CMS) used by your content managers, and let's say you need to retrieve content from CMS to show it to users or cache in youe main database, this is where OrchardCoreModule.WebApi can help you.

[![Build Status](https://dev.azure.com/gallkam/OrchardCoreModules/_apis/build/status/%5BBuild%5D%20Dev.OrchardCoreModules.WebApi?branchName=dev)](https://dev.azure.com/gallkam/OrchardCoreModules/_build/latest?definitionId=11&branchName=dev) ![Nuget](https://img.shields.io/nuget/v/OrchardCoreModule.WebApi) ![Nuget](https://img.shields.io/nuget/dt/OrchardCoreModule.WebApi) ![GitHub](https://img.shields.io/github/license/ujinjinjin/OrchardCoreModule.WebApi)

# Test coverage

At the moment, this project only tested manually by the team of two QA's and cutting edge bugs not found, but automated tests are on their way.

# How to configure

There are 5 major steps to add this module to your OrchardCore CMS instance:

1. Create an empty ASP.NET Core project
2. Configure it to use OrchardCore.Cms
3. Add OrchardCoreModule.WebApi as a reference to this project
4. Apply procedures scripts from Database folder
5. Enable WebApi module in OrchardCore CMS settings

For more information on each of the steps visit projects wiki

# Supported DB providers

OrchardCoreModule.WebApi currently supports only following DB providers:

1. PostgreSQL (9.5+)
2. Microsoft SQL Server (2017+)

# Implemented API methods

List of implemented API methods can be found on this wiki page.

# Advanced synchronization options

To be written
