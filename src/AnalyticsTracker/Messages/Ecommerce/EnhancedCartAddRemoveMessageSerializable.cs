using System;

namespace Paragon.Analytics.Messages
{
    [Serializable]
    public class EnhancedCartAddRemoveMessageSerializable 
    {
        public EnhancedCartAddRemoveMessageSerializable(EnhancedCartAddRemoveMessage SourceCartMessage,string dataLayerName)
        {
            _sourceMessageRenderResult = SourceCartMessage.RenderMessage(dataLayerName);
        }

        private string _sourceMessageRenderResult;

        public string SourceMessageResult
        {
            get
            {
                return _sourceMessageRenderResult;
            }
        }

    }
}