using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ItemGrabber.Models
{
    public class ItemInfo
    {
        public ReadOnlyCollection<Items> ItemList { get; set; }
        public ReadOnlyDictionary<string, string> currentValue { get; set; }
        public uint totalPages; // out param
        public int currentPage{get;set;}
        public string currency { get; set; }
        public ushort maxPage { get; set; }
        
        public ItemInfo()
        {
            currentPage=1;
            currency = "1";
        }
    }
}