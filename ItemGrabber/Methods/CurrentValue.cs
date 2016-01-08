using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
namespace ItemGrabber.Methods
{
    public class CurrentValue
    {
        public static void createXML()
        {
            XDocument xdoc = null;
            try
            {
               xdoc = GetXML.InvokeService("http://www.floatrates.com/daily/usd.xml");
            }
            catch (Exception ex)
            {

                return;
            }

            xdoc.Save(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/")+"data.xml");
    
        }
        public static ReadOnlyDictionary<string, string> getData()
        {   
            IList<XElement> templist = null;
            try { 
            templist = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/") + "data.xml").Descendants().Where(e => e.Name.LocalName.Equals("item")).ToList();
                }
            catch (Exception ex)
            {

                return null;
            }
            string[] keyval = new string[2];
            IDictionary<string, string> currentValues = new Dictionary<string, string>();
            currentValues.Add("USD", "1");
            for (int i = 0; i < templist.Count; i++)
            {
                foreach (XElement item2 in templist[i].Descendants())
                {
                    if (item2.Name.LocalName.Equals("targetCurrency"))
                    {
                        keyval[0] = item2.Value;
                    }
                    if (item2.Name.LocalName.Equals("exchangeRate"))
                    {
                        keyval[1] = item2.Value;
                    }
                }
                currentValues.Add(keyval[0], keyval[1]);
            }
            return new ReadOnlyDictionary<string,string>(currentValues);
            }
    }
}