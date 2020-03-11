using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

//[assembly: OwinStartup(typeof(ActualFileStorage.PL.OwinStartup))]
namespace ActualFileStorage.PL
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCookieAuthentication(new CookieAuthenticationOptions {
            //    AuthenticationType = .ApplicationCookie,
            //    //CookieSecure = CookieSecureOption.Always,
            //    ExpireTimeSpan = TimeSpan.FromMinutes(30),
            //    LoginPath = new PathString("/Login/Index"),
            //    SlidingExpiration = true
            //});
            //app.UseCookieAuthentication(new CookieAuthenticationOptions() { 

            //});

            //app.UseOAuthAuthorizationServer(new MyOAuthOptions());
            app.UseCookieAuthentication(new CookieAuthenticationOptions() {
                Provider = new CookieProvider1(),
                CookieDomain = ConfigurationManager.AppSettings["validissuer"],
                AuthenticationType = ConfigurationManager.AppSettings["authtype"],
                AuthenticationMode = AuthenticationMode.Active,
                CookieHttpOnly = true,
                 LoginPath = new PathString("/"), 
                //LoginPath = new PathString("/Auth/AuthForm"),
                //LogoutPath = new PathString("/Auth/Logout"),
                ExpireTimeSpan = TimeSpan.FromDays(7),
                TicketDataFormat = new MyJwtFormat()
            }) ;
            app.UseJwtBearerAuthentication(new MyJwtOptions());

            //app.UseCookieAuthentication(new MyCookieOptions());
        }
    }

    //public class MyCookieOptions : CookieAuthenticationOptions
    //{
    //    public MyCookieOptions()
    //    {

    //    }

    public class CookieProvider2 : ICookieAuthenticationProvider
    {
        public void ApplyRedirect(CookieApplyRedirectContext context)
        {
            throw new NotImplementedException();
        }

        public void Exception(CookieExceptionContext context)
        {
            throw new NotImplementedException();
        }

        public void ResponseSignedIn(CookieResponseSignedInContext context)
        {
            throw new NotImplementedException();
        }

        public void ResponseSignIn(CookieResponseSignInContext context)
        {
            throw new NotImplementedException();
        }

        public void ResponseSignOut(CookieResponseSignOutContext context)
        {
            throw new NotImplementedException();
        }

        public Task ValidateIdentity(CookieValidateIdentityContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class CookieProvider1 : CookieAuthenticationProvider
    {
        public override void ResponseSignedIn(CookieResponseSignedInContext context)
        {
            base.ResponseSignedIn(context);
        }

        public override void ResponseSignOut(CookieResponseSignOutContext context)
        {
            base.ResponseSignOut(context);
        }
        public override void ResponseSignIn(CookieResponseSignInContext context)
        {
            base.ResponseSignIn(context);
        }
        public override Task ValidateIdentity(CookieValidateIdentityContext context)
        {
            if(false)
                context.RejectIdentity();
            return base.ValidateIdentity(context);
        }
        public override void ApplyRedirect(CookieApplyRedirectContext context)
        {
            base.ApplyRedirect(context);
        }
        public override void Exception(CookieExceptionContext context)
        {
            base.Exception(context);
        }
    }
    //}
    public class MyOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(ConfigurationManager.AppSettings["ExternalBearer"]);
            var username = context.OwinContext.Get<string>("username");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "user"));
            context.Validated(identity);
            if (!System.IO.File.Exists(@"F:\log.txt"))
                System.IO.File.Create(@"F:\log.txt").Close();
            System.IO.File.AppendAllText(@"F:\log.txt", "granting creds");
            //context.Response.Cookies.Append("ha","ha", new CookieOptions() { HttpOnly = true });
            return Task.FromResult(0);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            try
            {
                var username = context.Parameters["username"];
                var password = context.Parameters["password"];

                if (username == password)
                {
                    context.OwinContext.Set("username", username);
                    context.Validated();
                } else
                {
                    context.SetError("Invalid credentials");
                    context.Rejected();
                }
            }
            catch
            {
                context.SetError("Server error");
                context.Rejected();
            }
            return Task.FromResult(0);
        }
    }
    public class MyJwtOptions : JwtBearerAuthenticationOptions
    {
        public MyJwtOptions()
        {
            var issuer = ConfigurationManager.AppSettings["validissuer"];
            var audience = ConfigurationManager.AppSettings["audience"];
            var key = Convert.FromBase64String(ConfigurationManager.AppSettings["jwtsecretkeybase64"]);//Convert.FromBase64String("UHxNtYMRYwvfpO1dS5pWLKL0M2DgOj40EbN4SoBWgfc");
             
            AllowedAudiences = new[] { audience };
            IssuerSecurityKeyProviders = new[]{ new SymmetricKeyIssuerSecurityKeyProvider(issuer, key) };
            TokenValidationParameters = new TokenValidationParameters() {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                 ValidAudiences = new[] { audience },
                 ValidIssuer = issuer
            };

        }
    }


    public class MyJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        //private readonly OAuthAuthorizationServerOptions _options;

        public MyJwtFormat()//OAuthAuthorizationServerOptions options)
        {
            //_options = options;
        }

        public string SignatureAlgorithm
        {
            get { return "HS256"; } //"http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"; }
        }

        public string DigestAlgorithm
        {
            get { return "SHA256"; }//"http://www.w3.org/2001/04/xmlenc#sha256"; }
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException("data");

            var issuer = ConfigurationManager.AppSettings["validissuer"];
            var audience = ConfigurationManager.AppSettings["audience"];
            var key = Convert.FromBase64String(ConfigurationManager.AppSettings["jwtsecretkeybase64"]); //Convert.FromBase64String("UHxNtYMRYwvfpO1dS5pWLKL0M2DgOj40EbN4SoBWgfc");
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(60*24*7);//_options.AccessTokenExpireTimeSpan.TotalMinutes);
            var signingCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(key),
                                        SignatureAlgorithm,
                                        DigestAlgorithm);
            
            var token = new JwtSecurityToken(issuer, audience, data.Identity.Claims,
                                             now, expires, signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            string audienceId = ConfigurationManager.AppSettings["audience"];

            var signingKey = new SymmetricSecurityKey(Convert.FromBase64String(ConfigurationManager.AppSettings["jwtsecretkeybase64"]));

            var tokenValidationParameters = new TokenValidationParameters {
                ValidAudience = audienceId,
                ValidIssuer = ConfigurationManager.AppSettings["validissuer"],
                IssuerSigningKey = signingKey,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true
            };

            var handler = new JwtSecurityTokenHandler();

            // Unpack token
            var pt = handler.ReadJwtToken(protectedText);
            string t = pt.RawData;

            SecurityToken token;
            var principal = handler.ValidateToken(t, tokenValidationParameters, out token);

            var identity = principal.Identities;

            return new AuthenticationTicket(identity.First(), new AuthenticationProperties());
        }
    }
//    public class MyOAuthOptions : OAuthAuthorizationServerOptions
//    {
//        public MyOAuthOptions()
//        {
//            AuthenticationType = ConfigurationManager.AppSettings["jwtauthtype"];
//            TokenEndpointPath = new PathString("/token");
            
//            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60);
//            AccessTokenFormat = new MyJwtFormat(new OAuthAuthorizationServerOptions() {
                
//                //AccessTokenExpireTimeSpan = TimeSpan.FromDays(2)
//            });
//            Provider = new MyOAuthProvider();
            
//#if DEBUG
//            AllowInsecureHttp = true;
//#endif
//        }
//    }


}