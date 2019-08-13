using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateBarbershop
{
    class ClientFactory
    {
        private static int _count_s = 0;

        public static int Count_s { get => _count_s; set => _count_s = value; }

        public static Client CreateNewClient()
        {
            var newClient = new Client(_count_s.ToString());
            _count_s++;
            return newClient;
        }
    }
}
