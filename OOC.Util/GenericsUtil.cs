using System;

namespace OOC.Util
{
    public static class GenericsUtil
    {
        public static T CastTo<T>(this object obj)
        {
            try
            {
                return (T) obj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return default(T);
        }
    }
}