using Paragon.Analytics.Models;
using System.Collections.Generic;
using System.Linq;

namespace Paragon.Analytics.Messages
{
    public class EnhancedProductImpressionMessage : MessageBase
    {
      
        private ConfigurationObject _commerceConfig;

        public EnhancedProductImpressionMessage(List<GTMImpression> productImpressions,  string currencyISO)
        {
            initInfo(productImpressions, currencyISO);
        }
        public EnhancedProductImpressionMessage(string currencyISO)
        {
            initInfo(null, currencyISO);
        }

        public EnhancedProductImpressionMessage(GTMImpression productImpression, string currencyISO)
        {
            var impressions = new List<GTMImpression> { productImpression };
            initInfo(impressions, currencyISO);
        }

        public override string RenderMessage(string dataLayerName)
        {
            return Push(dataLayerName, _commerceConfig);
        }
        public string RenderMsgJson()
        {
            return _commerceConfig.Render();
        }


        private void initInfo(List<GTMImpression> imps, string currencyISO)
        {
         
            Dictionary<string, object> configWrap = new Dictionary<string, object>();
            Dictionary<string, object> ecomWrap = new Dictionary<string, object>();

            ecomWrap["impressions"] = imps.Select(i => i.Info).ToArray();
            ecomWrap["currencyCode"] = currencyISO;

            configWrap["ecommerce"] = ecomWrap;

            _commerceConfig = new ConfigurationObject(configWrap);

        }
        public void Push()
        {
            TagManager.Current.AddMessage(this);

        }

    }
}