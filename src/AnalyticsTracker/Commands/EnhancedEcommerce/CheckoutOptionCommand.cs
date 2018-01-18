﻿using System.Text;
using Paragon.Analytics.Commands.EnhancedEcommerce.FieldObjects;
using Paragon.Analytics.Commands.Events;

namespace Paragon.Analytics.Commands.EnhancedEcommerce
{
	public class CheckoutOptionCommand : EnhancedEcommerceCommandBase
	{
		private readonly CheckoutActionFieldObject _checkoutAction;
		private readonly EventCommand _trackingEvent;

		public CheckoutOptionCommand(CheckoutActionFieldObject checkoutAction, EventCommand trackingEvent)
		{
			_checkoutAction = checkoutAction;
			_trackingEvent = trackingEvent;
		}

		public override CommandOrder Order
		{
			get { return CommandOrder.AfterPageView; }
		}

		public override string RenderCommand()
		{
			var sb = new StringBuilder();

			var ccfg = new ConfigurationObject(_checkoutAction.Info);
			sb.AppendFormat("ga('ec:setAction', 'checkout_option', {0});", ccfg.Render());
			sb.AppendLine();

			if (_trackingEvent != null)
				sb.Append(_trackingEvent.RenderCommand());

			return sb.ToString();
		}
	}
}