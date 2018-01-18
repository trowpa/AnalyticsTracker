using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paragon.Analytics.Models
{
    public class GTMPurchase
    {
        public GTMPurchase()
        {

        }
        public GTMPurchase(GTMActionField orderInfo, List<GTMProduct> lineItems)
        {
            OrderInfo = orderInfo;
            LineItems = lineItems;
        }
        public GTMActionField OrderInfo { get; set; }
        public List<GTMProduct> LineItems { get; set; }

    }
}