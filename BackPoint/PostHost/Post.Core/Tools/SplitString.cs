using System;
using System.Collections.Generic;
using System.Text;

namespace Post.Core.Tools
{
    public static class SplitString
    {
        public static string SplitStringWithStart(string originString)
        {
            int startIndex = originString.IndexOf("=") + 1;
            try
            {
                string stringResult = originString.Substring(startIndex);
                return stringResult;
            }
            catch(Exception ex) {
                var re = ex.Message;
                return re;
            }

        }
    }
}
