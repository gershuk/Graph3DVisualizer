namespace SupportFuctions
{
    public static partial class ObjectFunctions
    {
        public static void Swap<T> (ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}

