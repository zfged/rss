using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Permissions;
using System.Web.Http;

namespace Rss.Controllers
{
    // контроллер проверяющий права доступа (только для клиента на стороне сервера это проверяется менеджером "identity")
    public class AuthorizeController : ApiController
    {
        [HttpGet]

        public HttpResponseMessage GETUSer()
        {
            try
            {

                PrincipalPermission pp = new PrincipalPermission(null, "Admin");
                pp.Demand();
                return Request.CreateResponse(HttpStatusCode.OK, "Admin");

                // Если управление достигло этой точки, требование удовлетворено.
                // Текущий пользователь является администратором
            }
            catch (SecurityException)
            {
                try
                {

                    PrincipalPermission pp = new PrincipalPermission(null, "User");
                    pp.Demand();
                    return Request.CreateResponse(HttpStatusCode.OK, "User");

                    // Если управление достигло этой точки, требование удовлетворено.
                    // Текущий пользователь является администратором
                }
                catch (SecurityException)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "noauth");
                }
            }
        }
    }
}
