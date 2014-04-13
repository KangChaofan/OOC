namespace OOC.Util
{
    public static class GenericsUtil
    {
        public static T CastTo<T>(this object obj)
        {
            return (T) obj;
        }
    }
}