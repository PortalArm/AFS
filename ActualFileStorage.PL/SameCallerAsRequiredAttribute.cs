using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace ActualFileStorage.PL
{
    public class SameCallerAsRequiredAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            int? fromSessionId = (int?)filterContext.HttpContext.Session["userId"];
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || !fromSessionId.HasValue)
                filterContext.Result = new HttpUnauthorizedResult("Неавторизованным запрещено это действие");
            var callerId = int.Parse(((ClaimsIdentity)filterContext.HttpContext.User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
            if (callerId != fromSessionId)
                filterContext.Result = new HttpUnauthorizedResult("Данное действие разрешено только создателю");
            //
        }
    }
}