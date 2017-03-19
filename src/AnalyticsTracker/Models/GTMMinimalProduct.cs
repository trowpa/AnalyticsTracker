using Paragon.Analytics.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Paragon.Analytics.Models
{
    public class GTMMinimalProduct
    {
        //private readonly Dictionary<string, object> _info = new Dictionary<string, object>();

      
        public GTMMinimalProduct(string id, string price, int quantity)
        {
            Id = id;
            Price = price;
            Quantity = quantity;

        }
        public GTMMinimalProduct(string id, decimal price, int quantity) : this(id, price.ToGTMCurrencyString(), quantity) { }
        
        public GTMMinimalProduct()
        {
        }

        

        [GTMData]
        public string Id { get; set; }

        //  public string Name { get; set; }

        [GTMData]
        public string Price { get; set; }

        // public string Brand { get; set; }
        // public string Category { get; set; }
        // public string Variant { get; set; }

        [GTMData]
        public int? Quantity { get; set; }

        // public string Coupon { get; set; }
     
        public Dictionary<string, object> Info
        {
            get
            {
                return this.ToDictionary() as Dictionary<string, object>;
            }
        }

     
    }
}