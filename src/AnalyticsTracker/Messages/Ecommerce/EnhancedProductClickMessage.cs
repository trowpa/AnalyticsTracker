using System.Collections.Generic;
using System.Linq;
using Paragon.Analytics;
using Paragon.Analytics.Models;

namespace Paragon.Analytics.Messages
{
    public class EnhancedProductClickMessage : MessageBase
    {
        private string _eventString = "productClick";
        private string _actionString = "click";

        private ConfigurationObject _commerceConfig;
       
       
        public EnhancedProductClickMessage(GTMProduct productClicked,string list, string currencyISO)
        {
            initInfo(productClicked,currencyISO,list);
           

        }
        public EnhancedProductClickMessage(GTMProduct productClicked, string currencyISO)
        {

            initInfo(productClicked, currencyISO);

        }

        public override string RenderMessage(string dataLayerName)
        {
            return Push(dataLayerName, _commerceConfig);
        }


        private void initInfo(GTMProduct p, string currencyISO, string List = "")
        {
            List<GTMProduct> li = new List<GTMProduct>{p};

            Dictionary<string, object> configWrap = new Dictionary<string, object>();
            Dictionary<string, object> ecomWrap = new Dictionary<string, object>();
            Dictionary<string, object> actionWrap = new Dictionary<string, object>();
            Dictionary<string, object> actionFieldWrap = new Dictionary<string, object>();

            actionFieldWrap["list"] = List;

            actionWrap["actionField"] = actionFieldWrap;
            actionWrap["products"] = li.Select(i => i.Info).ToArray();

            ecomWrap[_actionString] = actionWrap;
            ecomWrap["currencyCode"] = currencyISO;

            configWrap["event"] = _eventString;
            configWrap["ecommerce"] = ecomWrap;

            _commerceConfig = new ConfigurationObject(configWrap);

        }
        public void Push()
        {
            TagManager.Current.AddMessage(this);

        }

    }
}