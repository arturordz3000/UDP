using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Core.Managers
{
    class RawDataManager
    {
        public static string ConvertBytesToString(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }

        public static string[] GetParametersFromMessage(byte[] bytes, char[] splitter)
        {
            string rawMessage = RawDataManager.ConvertBytesToString(bytes);
            return rawMessage.Split(splitter);
        }

        public static Dictionary<string, string> ConvertToDictionary(string[] keys, string[] values)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            if (keys.Count() == values.Count() || keys.Count() > values.Count())
            {
                for (int i = 0; i < values.Count(); i++)
                {
                    dictionary.Add(keys[i], values[i]);
                }
            }
            else
            {
                for (int i = 0; i < keys.Count(); i++)
                {
                    dictionary.Add(keys[i], values[i]);
                }
            }

            return dictionary;
        }
    }
}
