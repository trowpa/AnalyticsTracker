using Paragon.Analytics.Models;
using System.Collections.Generic;
using System.Linq;

namespace Paragon.Analytics.Messages
{
    public class EnhancedTransactionMessage:MessageBase
    {
        private ConfigurationObject _commerceConfig;

        public EnhancedTransactionMessage(string id, string affiliation, string revenue, string tax, string shipping, List<GTMProduct> lineItems, string currencyISO = "USD",string coupon = "")
        {
            initInfo(new GTMActionField(id,affiliation,revenue,tax,shipping), lineItems, currencyISO);
        }
        public EnhancedTransactionMessage(Models.GTMPurchase purchase, string currencyISO = "USD")
        {
            var pi = purchase.OrderInfo;
            var li = purchase.LineItems;

            initInfo(pi, li,currencyISO);
            
        }

        public override string RenderMessage(string dataLayerName)
        {
            return Push(dataLayerName, _commerceConfig);
        }

        private void initInfo(GTMActionField pi, List<GTMProduct> li, string currencyISO = "USD")
        {
            Dictionary<string, object> configWrap = new Dictionary<string, object>();
            Dictionary<string, object> ecomWrap = new Dictionary<string, object>();
            Dictionary<string, object> purchaseWrap = new Dictionary<string, object>();

           
            purchaseWrap["actionField"] = pi.Info;
            purchaseWrap["products"] = li.Select(i => i.Info).ToArray();
            
            ecomWrap["purchase"] = purchaseWrap;
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