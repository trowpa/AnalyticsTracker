using System.Collections.Generic;
using System.Linq;
using Paragon.Analytics;
using Paragon.Analytics.Models;
using System;
using Newtonsoft.Json.Linq;

namespace Paragon.Analytics.Messages
{
    public class EnhancedProductClickMessage : MessageBase
    {
        private string _eventString = "productClick";
        private string _actionString = "click";

        private ConfigurationObject _commerceConfig;

        public EnhancedProductClickMessage(GTMProduct productClicked, string list, string currencyISO, string productUrl)
        {
            initInfo(productClicked, currencyISO, list, productUrl);


        }
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


        private void initInfo(GTMProduct p, string currencyISO, string List = "", string productUrl = "")
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

            //Use below if callbacks are needed - however right now the thought is not to make the links dependant on gtm success.
            //Don't break navigation because gtm js fails - so let the non-js href work at risk of losing a gtm hit due to race conditions
            //if (!string.IsNullOrEmpty(productUrl))
            //{

            //    var navFunctionFormat = "function() {{ document.location = {0};}}";
            //    var cb = string.Format(navFunctionFormat, productUrl);

            //    configWrap["eventCallback"] = new JRaw(cb);
            //}

            _commerceConfig = new ConfigurationObject(configWrap);

        }
        public void Push()
        {
            TagManager.Current.AddMessage(this);

        }

    }
}