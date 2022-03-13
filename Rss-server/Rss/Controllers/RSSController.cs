using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Rss.Models;
using System.Data.Entity;
using System.Security.Permissions;
using System.Security;
using System.Web.UI;

namespace Rss.Controllers
{
    //Контроллер - Управление  лентами



    public class RSSController : ApiController
    {
        RssContext rsscontext = new RssContext();



        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        //метод для получения списка лент

        //кешируем вывод ленты (списка) на 10 минут
        [System.Web.Mvc.OutputCache(Duration = 600, Location = OutputCacheLocation.Any)]
        public HttpResponseMessage GetRSSList()
        {
            return Request.CreateResponse(HttpStatusCode.OK, rsscontext.RSST);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        //метод для получения конкретной ленты

        //кешируем вывод ленты на 10 минут
        [System.Web.Mvc.OutputCache(Duration = 600, Location = OutputCacheLocation.Any)]
        public HttpResponseMessage GetRSS(int id)
        {

            RSS rss = rsscontext.RSST.Find(id);




            if (rss != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, rss);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Лента не Найдена");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        //метод для добавления ленты ленты
        public HttpResponseMessage AddRSS([FromBody]RSS rss)
        {
            if (!ModelState.IsValid) //валидация
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

            rsscontext.RSST.Add(rss);
            rsscontext.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, rss);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        //метод для редактирования ленты ленты
        public HttpResponseMessage EditRSS(int id, [FromBody] RSS rss)
        {
            if (id == rss.Id)
            {
                if (!ModelState.IsValid) //валидация
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                rsscontext.Entry(rss).State = EntityState.Modified;
                rsscontext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, rss);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Лента не найдена!");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        //метод для удаления ленты 
        public HttpResponseMessage DeleteRSS(int id)
        {
            RSS rss = rsscontext.RSST.Find(id);
            if (rss != null)
            {
                rsscontext.RSST.Remove(rss);
                rsscontext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, rss);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
