using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Models
{
    public static class RandomString
    {
        public static string GetNew(int length)
        {
            int minValue = (int)Math.Pow(10, length - 1);
            int maxValue = (int)Math.Pow(10, length) - 1;
            Random random = new Random();
            return random.Next(minValue, maxValue).ToString();
        }
    }
}
