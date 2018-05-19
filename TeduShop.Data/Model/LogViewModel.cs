using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models.Log
{
    public class LogViewModel
    {
        public int ID { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
    }
}