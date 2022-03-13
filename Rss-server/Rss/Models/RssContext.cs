using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Rss.Models
{
    //контекс для вывода данных
    public class RssContext : DbContext
    {
        public DbSet<RSS> RSST { get; set; }
        public DbSet<UserRSS> UserRSSes { get; set; }
    }
}