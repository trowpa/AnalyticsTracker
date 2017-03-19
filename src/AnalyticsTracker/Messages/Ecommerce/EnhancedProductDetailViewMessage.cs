using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.Analytics;
using Paragon.Analytics.Commands.EnhancedEcommerce.FieldObjects;
using Paragon.Analytics.Models;

namespace Paragon.Analytics.Messages
{
    public class EnhancedProductDetailViewMessage : MessageBase
    {
        private string _eventString = "productDetailView";
        private string _actionString = "detail";

        private ConfigurationObject _commerceConfig;


        public EnhancedProductDetailViewMessage(GTMProduct productClicked, string list, string currencyISO)
        {
            initInfo(productClicked, currencyISO, list);


        }
        public EnhancedProductDetailViewMessage(GTMProduct productClicked, string currencyISO)
        {

            initInfo(productClicked, currencyISO);

        }
        public EnhancedProductDetailViewMessage(string list, string currencyISO)
        {

            initInfo(currencyISO, list);

        }
        
        public string RenderJSONBody()
        {
            return _commerceConfig.Render();
        }
        public override string RenderMessage(string dataLayerName)
        {
            return Push(dataLayerName, _commerceConfig);
        }
        private void initInfo(string currencyISO, string List = "")
        {
            initInfo(null, currencyISO, List);
        }
        private void initInfo(GTMProduct p, string currencyISO, string List = "")
        {
            List<GTMProduct> li = new List<GTMProduct>();
            if (p != null)
            {
                li.Add(p);
            }


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