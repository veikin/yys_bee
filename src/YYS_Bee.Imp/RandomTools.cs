using System;

namespace YYS_Bee.Imp
{
    public class RandomTools
    {
        public static int Get(int minValue,int maxValue)
        {
            int temp = minValue;
            if (minValue > maxValue)
            {
                minValue = maxValue;
                maxValue = temp;
            }
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }
    }
}
