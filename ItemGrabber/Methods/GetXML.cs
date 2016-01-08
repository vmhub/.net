using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using ItemGrabber.Models;
using System.Collections.ObjectModel;
namespace ItemGrabber.Methods
{
    public class GetXML
    {
        public static XDocument InvokeService(string requestUrl)
        {
            XDocument responseDocument = null;
            try
            {

                Uri requestUri = new Uri(requestUrl, UriKind.Absolute);
                WebRequest request = HttpWebRequest.Create(requestUri);
                request.Timeout = 20000;
                WebResponse response = request.GetResponse();
                XmlReader reader = XmlReader.Create(response.GetResponseStream());
                responseDocument = XDocument.Load(reader);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return responseDocument;
        }

        public static ReadOnlyCollection<Items> getItemList(string str, out uint totalPages)
        {
            XDocument xdoc = null;
            try {
                xdoc = InvokeService(str);
            }
            catch (Exception ex)
            {
              
                totalPages = 0;
                return null;
            }
            XNamespace ns = xdoc.Root.GetDefaultNamespace();
            IList<XElement> templist = xdoc.Descendants(ns + "Item").ToList();

            IList<Items> lizt = new List<Items>();
            String[] forconst = new String[4];
            for (int i = 0; i < templist.Count; i++)
            {

                foreach (XElement item2 in templist[i].Descendants())
                {
                    if (item2.Name.LocalName.Equals("Title"))
                    {
                        forconst[0] = item2.Value;
                    }

                    if (item2.Name.LocalName.Equals("DetailPageURL"))
                    {
                        forconst[1] = item2.Value;
                    }
                    if (item2.Name.LocalName.Equals("MediumImage"))
                    {
                        forconst[2] = item2.Descendants().First(e => e.Name.LocalName.Equals("URL")).Value; 
                          
                    }
                    if (item2.Name.LocalName.Equals("Price"))
                    {
                        forconst[3] = item2.Descendants().First(e => e.Name.LocalName.Equals("FormattedPrice")).Value.Replace("$", "");
                    }
                }
                lizt.Add(new Items(forconst[0], forconst[1], forconst[2], forconst[3]));
            }
            totalPages = uint.Parse(xdoc.Descendants().First(e => e.Name.LocalName.Equals("TotalPages")).Value);
            return new ReadOnlyCollection<Items> (lizt);
        }


        //overload without pages
        public static ReadOnlyCollection<Items> getItemList(string str)
        {

            XDocument xdoc = InvokeService(str);
            XNamespace ns = xdoc.Root.GetDefaultNamespace();
            IList<XElement> templist = xdoc.Descendants(ns + "Item").ToList();

            IList<Items> lizt = new List<Items>();
            String[] forconst = new String[4];
            for (int i = 0; i < templist.Count; i++)
            {

                foreach (XElement item2 in templist[i].Descendants())
                {
                    if (item2.Name.LocalName.Equals("Title"))
                    {
                        forconst[0] = item2.Value;
                    }

                    if (item2.Name.LocalName.Equals("DetailPageURL"))
                    {
                        forconst[1] = item2.Value;
                    }
                    if (item2.Name.LocalName.Equals("MediumImage"))
                    {
                        forconst[2] = item2.Descendants().First(e => e.Name.LocalName.Equals("URL")).Value; 
                    }
                    if (item2.Name.LocalName.Equals("Price"))
                    {
                        forconst[3] = item2.Descendants().First(e => e.Name.LocalName.Equals("FormattedPrice")).Value.Replace("$", "");
                    }
                }
                lizt.Add(new Items(forconst[0], forconst[1], forconst[2], forconst[3]));
            }
            return new ReadOnlyCollection<Items>(lizt);
        }


    }
}