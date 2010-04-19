using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;



namespace FindByCssTest
{
    public class Some
    {
        static private int _lastInt;

        static public int PositiveNonzeroInteger()
        {
            return Math.Abs(Integer()) + 1;
        }

        static public TEnum EnumMember<TEnum>()
        {
            Array enumValues = Enum.GetValues(typeof(TEnum));
            return (TEnum)enumValues.GetValue(Integer() % enumValues.Length);
        }

        public static int Integer()
        {
            return _lastInt++;
        }

        public static string String()
        {
            return "someUniqueString_" + Integer().ToString();
        }

        public static DateTime DateTime()
        {
            return new DateTime(2001 + Integer() % 20, 1 + Integer() % 12, 1 + Integer() % 26);
        }

        public static TInterface Stub<TInterface>() where TInterface : class
        {
            return new Mock<TInterface>(MockBehavior.Loose).Object;
        }
    }
}
