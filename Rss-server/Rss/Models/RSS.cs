using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rss.Models
{
    public class RSS
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите название Ленты")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите сылку для Ленты")]
        public string Link { get; set; }
        [Required(ErrorMessage = "Укажите заголовок для Ленты")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Укажите описание для Ленты")]
        public string Description { get; set; }

    }
}