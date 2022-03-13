using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Rss.Models;
using System.Web.UI;

namespace Rss.Controllers
{
    //Контроллер - Получение конкретной ленты


    public class ItemController : ApiController
    {
        RssContext rsscontext = new RssContext();
        [HttpGet]

        [Authorize(Roles = "Admin,User")]
        //метод для получения информации с конкретной ленты
        [System.Web.Mvc.OutputCache(Duration = 600, Location = OutputCacheLocation.Any)]
        public HttpResponseMessage Get(int id)
        {



            RSS rssValue = rsscontext.RSST.Find(id);
            XDocument feedXml = XDocument.Load(rssValue.Link);
            var feeds = from feed in feedXml.Descendants("item")
                        select new RSS
                        {
                            Title = feed.Element("title").Value,
                            Link = feed.Element("link").Value,
                            Description = Regex.Match(feed.Element("description").Value, @"^.{1,180}\b(?<!\s)").Value
                        };
            return Request.CreateResponse(HttpStatusCode.OK, feeds);

        }
    }
}
