using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paragon.Analytics;
using Paragon.Analytics.Models;

namespace Paragon.Analytics.Messages
{

    public class EnhancedCartAddRemoveMessage : MessageBase
    {


        private string _eventString;
        private string _actionString;

        private ConfigurationObject _commerceConfig;
        private CartUpdateAction _cartAction;
        private CartUpdateAction CartAction

        {
            get
            {
                return _cartAction;
            }
            set
            {
                _cartAction = value;

                if (_cartAction == CartUpdateAction.Add)
                {
                    _eventString = "addToCart";
                    _actionString = "add";
                }
                else
                {
                    _eventString = "removeFromCart";
                    _actionString = "remove";
                }

            }
        }

        public enum CartUpdateAction
        {
            Add = 1,
            Remove = 2
        }

        public EnhancedCartAddRemoveMessage(CartUpdateAction cartAction, List<GTMMinimalProduct> affectedLineItems, string currencyISO = "USD")
        {
            _renderResult = string.Empty;
            CartAction = cartAction;
            initInfo(affectedLineItems, currencyISO);
        }
        public EnhancedCartAddRemoveMessage(EnhancedCartAddRemoveMessageSerializable storedMessage)
        {
            _renderResult = storedMessage.SourceMessageResult;
        }
        public override string RenderMessage(string dataLayerName)
        {
            if (!string.IsNullOrEmpty(_renderResult))
            {
                return _renderResult;
            }
            return Push(dataLayerName, _commerceConfig);
        }

        private string _renderResult;

        private void initInfo(List<GTMMinimalProduct> li, string currencyISO = "USD")
        {
            Dictionary<string, object> configWrap = new Dictionary<string, object>();
            Dictionary<string, object> ecomWrap = new Dictionary<string, object>();
            Dictionary<string, object> addremoveWrap = new Dictionary<string, object>();

            addremoveWrap["products"] = li.Select(i => i.Info).ToArray();

            ecomWrap[_actionString] = addremoveWrap;
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