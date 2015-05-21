using System;

namespace AffinityUI
{
    public static class Extensions
    {
        public static float SafeToFloat(this string value)
        {
            float result;
            if (float.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        public static float SafeToFloat<TOwner>(this BindableProperty<TOwner, string> value)
        {
            float result;
            if (float.TryParse(value.Value, out result))
            {
                return result;
            }
            return 0;
        }

        public static float SafeToInt(this string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        public static int SafeToInt<TOwner>(this BindableProperty<TOwner, string> value)
        {
            int result;
            if (int.TryParse(value.Value, out result))
            {
                return result;
            }
            return 0;
        }

        public static double SafeToDouble(this string value)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        public static double SafeToDouble<TOwner>(this BindableProperty<TOwner, string> value)
        {
            double result;
            if (double.TryParse(value.Value, out result))
            {
                return result;
            }
            return 0;
        }

        public static long SafeToLong(this string value)
        {
            long result;
            if (long.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        public static long SafeToLong<TOwner>(this BindableProperty<TOwner, string> value)
        {
            long result;
            if (long.TryParse(value.Value, out result))
            {
                return result;
            }
            return 0;
        }

        public static byte SafeToByte(this string value)
        {
            byte result;
            if (byte.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        public static byte SafeToByte<TOwner>(this BindableProperty<TOwner, string> value)
        {
            byte result;
            if (byte.TryParse(value.Value, out result))
            {
                return result;
            }
            return 0;
        }
    }
}