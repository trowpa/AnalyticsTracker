
using System.Collections.Generic;
using Paragon.Analytics;
using Paragon.Analytics.Messages;

namespace Paragon.Analytics
{
    public static class GeneralDataPusher
    {
        public static void Push(string name, string value)  { doPush(new Variable(name, value)); }
        public static void Push(string name, string[] values) { doPush(new Variable(name, values)); }
        public static void Push(string name, int value) { doPush(new Variable(name, value)); }
        public static void Push(string name, int[] values) { doPush(new Variable(name, values)); }
        public static void Push(string name, uint value) { doPush(new Variable(name, value)); }
        public static void Push(string name, uint[] values) { doPush(new Variable(name, values)); }
        public static void Push(string name, decimal value) { doPush(new Variable(name, value)); }
        public static void Push(string name, decimal[] values) { doPush(new Variable(name, values)); }
        public static void Push(string name, Dictionary<string, object> value)  { }
        public static void Push(string name, Dictionary<string, object>[] values)  { }

        private static void doPush(Variable msg)
        {
          

            TagManager.Current.AddMessage(msg);

        }

    }
}
