using Login.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Login.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        //public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{

        //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

        //    //Dummy check here, you need to do your DB checks against memebrship system http://bit.ly/SPAAuthCode
        //    //if (context.UserName != context.Password)
        //    //{
        //    //    context.SetError("invalid_grant", "The user name or password is incorrect");
        //    //    //return;
        //    //    return Task.FromResult<object>(null);
        //    //}

        //    var identity = new ClaimsIdentity("JWT");

        //    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
        //    identity.AddClaim(new Claim("sub", context.UserName));
        //    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
        //    identity.AddClaim(new Claim(ClaimTypes.Role, "Supervisor"));

        //    var props = new AuthenticationProperties(new Dictionary<string, string>
        //    {
        //        {
        //         "audience", (context.ClientId == null) ? string.Empty : context.ClientId
        //        }
        //    });

        //    var ticket = new AuthenticationTicket(identity, props);
        //    context.Validated(ticket);
        //    return Task.FromResult<object>(null);
        //}

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            if (!user.EmailConfirmed)
            {
                context.SetError("invalid_grant", "User did not confirm email.");
                return;
            }

            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add("UserId", user.Id);
            properties.Add("UserName", user.UserName);
            properties.Add("Role", "Admin");

            AuthenticationProperties aProperties = new AuthenticationProperties(properties);

            AuthenticationTicket ticket = new AuthenticationTicket(identity, aProperties);


            DateTime currentUtc = DateTime.UtcNow;

            DateTime expireUtc = currentUtc.Add(TimeSpan.FromHours(24));

            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = expireUtc;
            context.Validated(ticket);

        }
    }
}