using Paragon.Analytics.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Paragon.Analytics.Models
{
    public class GTMActionField
    {
        //private readonly Dictionary<string, object> _info = new Dictionary<string, object>();
        
        public GTMActionField(string id, string affiliation, string revenue, string tax, string shipiping, string coupon = "")
        {
            Id = id;
            Affiliation = affiliation;
            Revenue = revenue;
            Tax = tax;
            Shipping = shipiping;
            Coupon = coupon;

        }
        public GTMActionField(string id, string affiliation, decimal revenue, decimal tax, decimal shipping, string coupon = "") : this(id, affiliation, revenue.ToGTMCurrencyString(), tax.ToGTMCurrencyString(), shipping.ToGTMCurrencyString(), coupon) { }
     
        public GTMActionField()
        {

        }


        [GTMData]
        public string Id { get; set; }
        [GTMData]
        public string Affiliation { get; set; }
        [GTMData]
        public string Revenue { get; set; }
        [GTMData]
        public string Tax { get; set; }
        [GTMData]
        public string Shipping { get; set; }
        [GTMData]
        public string Coupon { get; set; }

        public Dictionary<string, object> Info {
            get {
                
                return this.ToDictionary() as Dictionary<string, object>;
            }
        }


        private void initInfo()
        {
            //_info["id"] = Id;
            //_info["affiliation"] = Affiliation;
            //_info["revenue"] = Revenue;
            //_info["tax"] = Tax;
            //_info["shipping"] = Shipping;
            //_info["coupon"] = Coupon;
          
        }
    }
}