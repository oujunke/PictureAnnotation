using System;
using System.Collections.Generic;
using System.Text;

namespace PictureAnnotation
{
    public static  class Extension
    {
        public static int StringToInt(this string str)
        {
            int.TryParse(str, out int result);
            return result;
        }
    }
}
