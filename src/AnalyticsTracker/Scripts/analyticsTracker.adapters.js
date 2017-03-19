﻿(function(window, jQuery, angular, undefined) {
	var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

	function decode64(input) {
		var output = "";
		var chr1, chr2, chr3 = "";
		var enc1, enc2, enc3, enc4 = "";
		var i = 0;

		// remove all characters that are not A-Z, a-z, 0-9, +, /, or =
		var base64test = /[^A-Za-z0-9\+\/\=]/g;
		if (base64test.exec(input)) {
			alert("There were invalid base64 characters in the input text.\n" +
				"Valid base64 characters are A-Z, a-z, 0-9, '+', '/',and '='\n" +
				"Expect errors in decoding.");
		}
		input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

		do {
			enc1 = keyStr.indexOf(input.charAt(i++));
			enc2 = keyStr.indexOf(input.charAt(i++));
			enc3 = keyStr.indexOf(input.charAt(i++));
			enc4 = keyStr.indexOf(input.charAt(i++));

			chr1 = (enc1 << 2) | (enc2 >> 4);
			chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
			chr3 = ((enc3 & 3) << 6) | enc4;

			output = output + String.fromCharCode(chr1);

			if (enc3 != 64) {
				output = output + String.fromCharCode(chr2);
			}
			if (enc4 != 64) {
				output = output + String.fromCharCode(chr3);
			}

			chr1 = chr2 = chr3 = "";
			enc1 = enc2 = enc3 = enc4 = "";

		} while (i < input.length);

		return unescape(output);
	}

	if (jQuery) {
		$(document).ajaxComplete(function(evt, xhr, options) {
			var i = 0;
			var value = null;
			var encodedHeader = "";
			do {
				value = xhr.getResponseHeader("AnalyticsTracker-" + i);

				if (value) {
					encodedHeader += value;
				}

				i++;
			} while (value);

			if (encodedHeader) {
				var decoded = decode64(encodedHeader);
				eval(decoded);
			}
		});

		// http://api.jquery.com/jQuery.ajaxSetup/
		// Set default values for future Ajax requests. Its use is not recommended.
		$.ajaxSetup(
		{
			headers: { 'AnalyticsTracker-Enabled': 'true' }
		});
	}

	if (angular) {
		var module = angular.module("analytics", ["ngResource"]);

		module.factory('AnalyticsInterceptor', [
			function() {
				var myInterceptor = {
					response: function(response) {
						var i = 0;
						var value;
						var headerEncoded = "";
						do {
							value = response.headers("AnalyticsTracker-" + i);
							if (value) {
								headerEncoded += value;
							}
							i++;
						} while (value)

						if (headerEncoded) {
							var decoded = decode64(headerEncoded);
							eval(decoded);
						}
						return response;
					}
				};

				return myInterceptor;
			}
		]);

		module.config([
			'$httpProvider', function($httpProvider) {
				$httpProvider.interceptors.push('AnalyticsInterceptor');
				$httpProvider.defaults.headers.common['AnalyticsTracker-Enabled'] = true;
			}
		]);
	}
})(window, window.jQuery, window.angular);