using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AmazonProductAdvtApi;
namespace ItemGrabber.Methods
{
    public class Requezt
    {
        public static string GenerateSignedRequestUrl(string search, string ind)
        {
            SignedRequestHelper helper = new SignedRequestHelper("AKIAICBPC6GRXG5RSJVQ", "sVEJc5ntFfiBU8k4DriOLWk2mBT2Nqt1BbOLbTn5", "webservices.amazon.com");

            IDictionary<string, string> req = new Dictionary<string, String>();
            req["Service"] = "AWSECommerceService";
            req["Keywords"] = search;
            req["Operation"] = "ItemSearch";
            req["SearchIndex"] = ind;
            req["AssociateTag"] = "givmemyidcom-20";
            req["ItemPage"] = "1";
            req["ResponseGroup"] = "Offers,Images,ItemAttributes";
            req["Timestamp"] = DateTime.Now.ToString("yyyy-mm-ddThh:mm:ssZ");
            return helper.Sign(req);
        }
        //overload with page set
        public static string GenerateSignedRequestUrl(string search, string page, string ind)
        {
            SignedRequestHelper helper = new SignedRequestHelper("AKIAICBPC6GRXG5RSJVQ", "sVEJc5ntFfiBU8k4DriOLWk2mBT2Nqt1BbOLbTn5", "webservices.amazon.com");
            IDictionary<string, string> req = new Dictionary<string, String>();
            req["Service"] = "AWSECommerceService";
            req["Keywords"] = search;
            req["Operation"] = "ItemSearch";
            req["SearchIndex"] = ind;
            req["AssociateTag"] = "givmemyidcom-20";
            req["ItemPage"] = page;
            req["ResponseGroup"] = "Offers,Images,ItemAttributes";
            req["Timestamp"] = DateTime.Now.ToString("yyyy-mm-ddThh:mm:ssZ");
            return helper.Sign(req);
        }

    }
}