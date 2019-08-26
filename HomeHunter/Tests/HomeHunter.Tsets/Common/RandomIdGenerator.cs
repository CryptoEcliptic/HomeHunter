using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Tsets.Common
{
    public static class RandomIdGenerator
    {
        public static int GenerateRandomIntId()
        {
            Random rnd = new Random();
            int id = rnd.Next(1, 100000);

            return id;
        }

        public static string GeneradeRandomStringId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
