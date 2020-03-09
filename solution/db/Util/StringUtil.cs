using System;
using System.Collections.Generic;
using System.Text;

namespace pecacompativel.db.Util
{
    public static class StringUtil
    {
        /// <summary>
        /// Return friendly name to be used with URL paths
        /// </summary>
        /// <param name="name">name or string content</param>
        /// <returns></returns>
        public static string GetFriendlyName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "";

            Dictionary<string, char> charToReplace = new Dictionary<string, char>()
            {
                {"áàâã", 'a' }, {"éèê", 'e'}, {"íìî",'i'},
                {"óòõô", 'o' }, {"úùû", 'u'}, {"!@#$%¨&*()_=[]{}^~´`'\"?;:<>,. /", '-'}
            };

            name = name.ToLower().Trim();

            foreach(var item in charToReplace)
            {
                foreach(char character in item.Key)
                {
                   name = name.Replace(character, item.Value);
                }
            }

            return name;
        }
    }
}
