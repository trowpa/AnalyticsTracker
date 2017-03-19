using System.Text;
using Paragon.Analytics.Commands.EnhancedEcommerce.FieldObjects;
using Paragon.Analytics.Commands.Events;

namespace Paragon.Analytics.Commands.EnhancedEcommerce
{
	public class AddToBasketCommand : EnhancedEcommerceCommandBase
	{
		private readonly ProductFieldObject _product;
		private readonly EventCommand _trackingEvent;

		public AddToBasketCommand(ProductFieldObject product, EventCommand trackingEvent)
		{
			_product = product;
			_trackingEvent = trackingEvent;
		}

		public override CommandOrder Order
		{
			get { return CommandOrder.AfterPageView; }
		}

		public override string RenderCommand()
		{
			var sb = new StringBuilder();

			var lcfg = new ConfigurationObject(_product.Info);
			sb.AppendFormat("ga('ec:addProduct', {0});", lcfg.Render());
			sb.AppendLine();
			sb.AppendLine("ga('ec:setAction', 'add');");

			if (_trackingEvent != null)
				sb.Append(_trackingEvent.RenderCommand());

			return sb.ToString();
		}
	}
}