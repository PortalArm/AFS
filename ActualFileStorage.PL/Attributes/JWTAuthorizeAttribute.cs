﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web.Mvc.Filters;

namespace ActualFileStorage.PL.Attributes
{
    //public class JWTAuthenticateAttribute : ActionFilterAttribute, IAuthenticationFilter
    //{
    //    public void OnAuthentication(AuthenticationContext filterContext)
    //    {
    //        string token = string.Empty;
    //        filterContext.HttpContext.Request.Headers.Add("Authorization", $"Bearer {token}");

    //    } 

    //    public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
    //    {
            
    //    }
    //}

    //public class JWTAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    //{
    //    public void OnAuthorization(AuthorizationContext filterContext)
    //    {
    //        if (filterContext.HttpContext.Request.Headers["Authorization"] != null)
    //        {
    //            filterContext.HttpContext.Response.Write("Ну, ты попытался");
    //        }
    //        //byte[] keyBytes = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["jwtsecretkey"]);
    //        ////filterContext.HttpContext.User.Identity
    //        //JwtHeader jh = new JwtHeader(new EncryptingCredentials(new SymmetricSecurityKey(keyBytes), JsonWebKeyECTypes.P512));
    //        //JwtPayload jp = new JwtPayload(new List<Claim>() { new Claim("CLASS_1", "val1") });
    //        ////ужас
    //        //var token = new JwtSecurityToken(jh,jp);
            
    //        //filterContext.HttpContext.Response.Headers.Add("Authorization","Bearer " + token.ToString());
    //        //JwtSecurityToken token = new JwtSecurityToken()
    //        //filterContext
    //        ///filterContext
    //    }
    //}
}