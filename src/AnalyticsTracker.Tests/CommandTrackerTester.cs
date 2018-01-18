using System.Collections.Generic;
using NUnit.Framework;
using Paragon.Analytics;
using Paragon.Analytics.Commands;

namespace AnalyticsTracker.Tests
{
	[TestFixture]
	public class CommandTrackerTester
	{
		[Test]
		public void Render_AccountSet_CreatesTracker()
		{
			var subj = new CommandTracker();
			subj.SetAccount("UA-00000000-1");
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('create', 'UA-00000000-1', 'auto');"));
		}

		[Test]
		public void Render_NoConfig_CreatesTrackerWithAuto()
		{
			var subj = new CommandTracker();
			subj.SetAccount("UA-00000000-1");
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('create', 'UA-00000000-1', 'auto');"));
		}

		[Test]
		public void Render_WithConfig_CreatesTrackerWithConfig()
		{
			var subj = new CommandTracker();
			subj.SetAccount("UA-00000000-1");
			subj.SetTrackerConfiguration(new Dictionary<string, object>
			{
				{"name", "myTracker"},
				{"siteSpeedSampleRate", 50},
				{"alwaysSendReferrer", true}
			});
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('create', 'UA-00000000-1', {'name': 'myTracker','siteSpeedSampleRate': 50,'alwaysSendReferrer': true});"));
		}

		[Test]
		public void Render_NoPageSet_DefaultEnabled_SetDefaultPage()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = true;
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('send', {'hitType': 'pageview'});"));
		}

		[Test]
		public void Render_NoPageSet_DefaultDisabled_NoPageSet()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = false;
			string rendered = subj.Render();
			Assert.That(rendered, Is.Not.StringContaining("ga('send', {'hitType': 'pageview'});"));
		}

		[Test]
		public void Render_PageSet_PageTracked()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = true;
			subj.SetPage("/foo");
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('set', {'page': '/foo'})"));
		}

		[Test]
		public void Render_CurrencySet_CurrencyTracked()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = true;
			subj.SetCurrency(AnalyticsCurrency.DKK);
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('set', {'&cu': 'DKK'})"));
		}

		[Test]
		public void Render_WithDisplayFeatures_DisplayFeaturesAdded()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = true;
			subj.Require("displayfeatures");
			string rendered = subj.Render();
			Assert.That(rendered, Is.StringContaining("ga('require', 'displayfeatures');"));
		}

		[Test]
		public void RenderForHeader_NoTracking_NoHeader()
		{
			var subj = new CommandTracker();
			string rendered = subj.RenderForHeader();
			Assert.That(rendered, Is.EqualTo(string.Empty));
		}

		[Test]
		public void RenderForHeader_ExplicitNonPageView_AddsCommand()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = false;
			string rendered = subj.RenderForHeader();
			Assert.That(rendered, Is.EqualTo(string.Empty));
		}

		[Test]
		public void RenderForHeader_ExplicitPageView_AddsCommand()
		{
			var subj = new CommandTracker();
			subj.TrackDefaultPageview = true;
			string rendered = subj.RenderForHeader();
			Assert.That(rendered, Is.StringContaining("ga('send', {'hitType': 'pageview'});"));
		}

		[Test]
		public void RenderForHeader_PageViewCommand_AddsCommand()
		{
			var subj = new CommandTracker();
			subj.Track(new PageViewCommand());
			string rendered = subj.RenderForHeader();
			Assert.That(rendered, Is.StringContaining("ga('send', {'hitType': 'pageview'});"));
		}
	}
}