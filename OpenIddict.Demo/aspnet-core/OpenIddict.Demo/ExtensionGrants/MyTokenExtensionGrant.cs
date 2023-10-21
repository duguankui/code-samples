using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using Volo.Abp.DependencyInjection;
using Volo.Abp.OpenIddict.Controllers;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;

namespace OpenIddict.Demo.ExtensionGrants
{
    [IgnoreAntiforgeryToken]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MyTokenExtensionGrant : AbpOpenIdDictControllerBase, ITokenExtensionGrant
    {
        public const string ExtensionGrantName = "extension_grant_code";
        public string Name => ExtensionGrantName;

        public async Task<IActionResult> HandleAsync(ExtensionGrantContext context)
        {
            LazyServiceProvider = context.HttpContext.RequestServices.GetRequiredService<IAbpLazyServiceProvider>();

            var claimsPrincipal = await GetPrincipal(context, "admin");
            return claimsPrincipal == null ?
                ForbidResult("User not found.")
                :
                SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        private ForbidResult ForbidResult(string msg)
        {
            return new ForbidResult(
                new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                GetAuthenticationProperties(msg));
        }

        private AuthenticationProperties GetAuthenticationProperties(string msg)
        {
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidRequest,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = msg
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private async Task<ClaimsPrincipal> GetPrincipal(ExtensionGrantContext context, string userName)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }

            var principal = await SignInManager.CreateUserPrincipalAsync(user);

            var scopes = context.Request.GetScopes();
            principal.SetScopes(scopes);
            var resources = await GetResourcesAsync(scopes);
            principal.SetResources(resources);

            return principal;
        }
    }

}
