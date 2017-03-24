using System.Collections.Generic;
using System.Linq;
using Paragon.Analytics;
using Paragon.Analytics.Models;

namespace Paragon.Analytics.Messages
{
    public class EnhancedPromotionClickMessage : MessageBase
    {
        private string _eventString = "promotionClick";
        private string _actionString = "promoClick";

        private ConfigurationObject _commerceConfig;
       
       
        public EnhancedPromotionClickMessage(GTMPromotion promoClicked, string currencyISO)
        {
            initInfo(promoClicked,currencyISO);
           

        }

        public override string RenderMessage(string dataLayerName)
        {
            return Push(dataLayerName, _commerceConfig);
        }

        private void initInfo(GTMPromotion p, string currencyISO)
        {
            List<GTMPromotion> promos = new List<GTMPromotion>{p};

            Dictionary<string, object> configWrap = new Dictionary<string, object>();
            Dictionary<string, object> ecomWrap = new Dictionary<string, object>();
            Dictionary<string, object> actionWrap = new Dictionary<string, object>();
          
            actionWrap["promotions"] = promos.Select(i => i.Info).ToArray();

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