using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActualFileStorage.PL
{
    public class JWTAuthorizeAttribute : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            ///filterContext
        }
    }
}