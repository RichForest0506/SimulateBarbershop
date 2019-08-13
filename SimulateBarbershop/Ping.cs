using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateBarbershop
{
    class Ping
    {
        public static int ShortPing { get; set; }
        public static int MediumPing { get; set; }
        public static int LongPing { get; set; }

        static Ping()
        {
            ShortPing = 1000;
            MediumPing = 5000;
            LongPing = 10000;
        }        
    }
}
