using Paragon.Analytics.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Paragon.Analytics.Models
{
    public class GTMPromotion
    {

        public GTMPromotion(string id, string name, string creative, string position)
        {
            Id = id;
            Name = name;
            Creative = creative;
            Position = position;

        }
        public GTMPromotion(string id, string creative, string position) : this(id, null, creative, position) { }

        public GTMPromotion(string id, string position) : this(id, null,position) { }


        [GTMData]
        public string Id { get; set; }

        [GTMData]
        public string Name { get; set; }

        [GTMData]
        public string Creative { get; set; }

        [GTMData]
        public string  Position { get; set; }
       

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