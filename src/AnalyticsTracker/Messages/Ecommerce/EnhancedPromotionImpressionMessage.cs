using Paragon.Analytics.Models;
using System.Collections.Generic;
using System.Linq;

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
        public EnhancedPromotionImpressionMessage(string currencyISO)
        {
            initInfo(null, currencyISO);
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

            if (TagManager.Current.HasPromoImpressionsWaiting)
            {
              imps =  imps.Concat(TagManager.Current.PromoImpressions).ToList();
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