using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulateBarbershop
{
    class Armchair
    {
        private Client _currentClient;
        private Mutex _mutexSit;

        public Armchair()
        {
            _currentClient = null;
            _mutexSit = new Mutex();
        }

        public Armchair(Client currentClient)
        {
            _currentClient = currentClient;
            _mutexSit = new Mutex();
        }

        public Mutex MutexSit { get => _mutexSit; set => _mutexSit = value; }
        internal Client CurrentClient { get => _currentClient; set => _currentClient = value; }
    }
}
