namespace Paragon.Analytics
{
	public abstract class MessageBase
	{
		public abstract string RenderMessage(string dataLayerName);
    public abstract string RenderMessage();

		protected string Push(string dataLayerName, ConfigurationObject obj)
		{
			return string.Format("{0}.push({1});",dataLayerName, obj.Render());
		}
    	
		protected string Push(ConfigurationObject obj)
		{
			return $"window.tagManagerPush({obj.Render()});";
		}
	}
}