using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulateBarbershop
{
    class WaitingRoom
    {
        private Queue<Client> _chairs;
        private Mutex _mutexRoom;

        public WaitingRoom()
        {
            _chairs = new Queue<Client>();
            _mutexRoom = new Mutex();
        }

        public WaitingRoom(Queue<Client> chairs)
        {
            _chairs = chairs;
            _mutexRoom = new Mutex();
        }

        public Mutex MutexRoom { get => _mutexRoom; set => _mutexRoom = value; }
        internal Queue<Client> Chairs { get => _chairs; set => _chairs = value; }
    }
}
