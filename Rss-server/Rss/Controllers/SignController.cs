using Rss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rss.Controllers
{
    public class SignController : ApiController
    {
        RssContext rsscontext = new RssContext();

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        //метод для получения списка лент
        public HttpResponseMessage GetRSSList()
        {
            string idUser = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            return Request.CreateResponse(HttpStatusCode.OK, rsscontext.UserRSSes.Where(p => p.IdUser == idUser));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        //метод для добавления подписки или отписки
        public HttpResponseMessage AddRSS([FromBody] UserRSS rss)
        {
            UserRSS rssbool;
            string idUser = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            try
            {
                rssbool = rsscontext.UserRSSes.Where(p => p.IdRSS == rss.IdRSS).Where(p => p.IdUser == idUser).First();
            }
            catch
            {
                rssbool = null;
            }

            if (rssbool == null)
            {
                rss.IdUser = idUser;
                rsscontext.UserRSSes.Add(rss);
                rsscontext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, "Вы успешно подписались");
            }
            else
            {
                rsscontext.UserRSSes.Remove(rssbool);
                rsscontext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Вы успешно удалили подписку");
            }
        }
    }
}
