using System.Web;
using System.Web.Mvc;

namespace ActualFileStorage.PL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new JWTAuthenticateAttribute());
            filters.Add(new HandleErrorAttribute());
            //(DefaultAuthenticationTypes.ExternalBearer)
            //filters.Add(new JWTAuthorizeAttribute());
        }
    }
}
