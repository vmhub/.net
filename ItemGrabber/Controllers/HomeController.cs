
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ItemGrabber.Methods;
using ItemGrabber.Models;
using System.Collections.ObjectModel;
namespace ItemGrabber.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {   //if(DateTime.Now < XML.Date) {create XML} else read(); -->ei jõudnud
            CurrentValue.createXML();
            ItemInfo itemInfo = new ItemInfo();
            itemInfo.currentValue = CurrentValue.getData();
            Search searchPorp = new Search();
            Session["itemInfo"] = itemInfo;
            return View(searchPorp);
        }

        public ActionResult Output(Search search, string submit, ItemInfo obj)
        {

            ItemInfo itemInf = Session["itemInfo"] as ItemInfo;
            string myItem;
            string searchIndex;
            uint maxPage;

            switch (submit)
            {
                case "Search for items on Amazon":

                    string[] searchProps = new[] { search.neededItem, search.searchIndex };
                    if (searchProps[0] == null)
                        return PartialView("Error");
                    itemInf.ItemList = GetXML.getItemList(Requezt.GenerateSignedRequestUrl(searchProps[0], searchProps[1]), out itemInf.totalPages);
                    Session["searchProps"] = searchProps;
                    if (searchProps[1].Equals("All"))
                        itemInf.maxPage = 5;
                    else itemInf.maxPage = 10;
                    itemInf.currentPage = 1;
                    return PartialView(itemInf);

                case "next":
                    itemInf.currency = obj.currency;
                    myItem = ((string[])Session["searchProps"])[0];
                    searchIndex = ((string[])Session["searchProps"])[1];
                    maxPage = itemInf.maxPage;
                    ++itemInf.currentPage;
                    if (itemInf.totalPages > maxPage)                
                        if (itemInf.currentPage > maxPage)
                            itemInf.currentPage = 1;                 
                    else
                        if (itemInf.currentPage > itemInf.totalPages)
                            itemInf.currentPage = 1;
                    itemInf.ItemList = GetXML.getItemList(Requezt.GenerateSignedRequestUrl(myItem, itemInf.currentPage.ToString(), searchIndex));
                    return PartialView(itemInf);

                case "previous":
                    itemInf.currency = obj.currency;
                    myItem = ((string[])Session["searchProps"])[0];
                    searchIndex = ((string[])Session["searchProps"])[1];
                    --itemInf.currentPage;
                    maxPage = itemInf.maxPage;
                    if (itemInf.currentPage < 1)
                        if (itemInf.totalPages > maxPage)
                            itemInf.currentPage = unchecked((int)maxPage);
                        else itemInf.currentPage = unchecked((int)itemInf.totalPages);
                    itemInf.ItemList = GetXML.getItemList(Requezt.GenerateSignedRequestUrl(myItem, itemInf.currentPage.ToString(), searchIndex));
                    return PartialView(itemInf);
            }
            return PartialView();
        }
        [HttpGet]
        public ActionResult SelectedValue(string curr)
        {
           ItemInfo itemInf = Session["itemInfo"] as ItemInfo;
           itemInf.currency = curr;
            return PartialView(itemInf);
        }
    }
}

