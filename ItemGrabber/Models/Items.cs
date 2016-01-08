using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItemGrabber.Models
{
    public class Items
    {
        public string price { get; private set; }
        public string title { get; private set; }
        public string URL { get; private set; }
        public string img { get; private set; }

        public Items(string title, string url, string img, string price)
        {
            this.title = title;
            this.URL = url;
            this.img = img;
            this.price = price;
        }

    }
}