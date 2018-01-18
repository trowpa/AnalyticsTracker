using Paragon.Analytics.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Paragon.Analytics.Models
{
    public class GTMImpression
    {
      
        public GTMImpression(string id,string list, int position, string price)
        {
            Id = id;
            List = list;
            Position = position;
            Price = price;
         
        }
        public GTMImpression(string id, string list, int position, decimal price) : this(id, list, position, price.ToGTMCurrencyString()) { }
       
       
       [GTMData]
        public string Id { get; set; }

        // public string Name { get; set; }

        [GTMData]
        public string Price { get; set; }

        [GTMData]
        public string List { get; set; }

        // public string Brand { get; set; }
        // public string Category { get; set; }
        // public string Variant { get; set; }

        [GTMData]
        public int Position { get; set; }



        public Dictionary<string, object> Info
        {
            get
            {
              
                return this.ToDictionary() as Dictionary<string, object>;
            }
        }
        public string InfoAsJSON
        {
            get
            {
                var infoCO = new ConfigurationObject(this.Info);

                return infoCO.Render();
            }
        }
    }
}