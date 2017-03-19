using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.Analytics;
using Paragon.Analytics.Commands.EnhancedEcommerce.FieldObjects;
using Paragon.Analytics.Models;

namespace Paragon.Analytics.Messages
{
    public class EnhancedPromotionImpressionMessage : MessageBase
    {
        private string _eventString = "promotionImpression";
        private string _actionString = "promoView";

        private ConfigurationObject _commerceConfig;

        public EnhancedPromotionImpressionMessage(List<GTMPromotion> promoImpressions,  string currencyISO)
        {
            initInfo(promoImpressions, currencyISO);
        }
      
        public override string RenderMessage(string dataLayerName)
        {
            return Push(dataLayerName, _commerceConfig);
        }
        public string RenderMsgJson()
        {
            return _commerceConfig.Render();
        }



        private void initInfo(List<GTMPromotion> imps, string currencyISO)
        {
           
            if (imps == null)
            {
                imps = new List<GTMPromotion>();
            }


            Dictionary<string, object> configWrap = new Dictionary<string, object>();
            Dictionary<string, object> ecomWrap = new Dictionary<string, object>();
            Dictionary<string, object> actionWrap = new Dictionary<string, object>();

            actionWrap["promotions"] = imps.Select(i => i.Info).ToArray();
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