using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulateBarbershop
{
    class Barbershop
    {
        private WaitingRoom _waitingRoom;
        private Barber _barber;
        private Armchair _armchair;        

        public Barbershop()
        {
            _waitingRoom = new WaitingRoom();
            _barber = new Barber();
            _armchair = new Armchair();            
        }

        public Barbershop(WaitingRoom waitingRoom, Barber barber, Armchair armchair)
        {
            _waitingRoom = waitingRoom;
            _barber = barber;
            _armchair = armchair;            
        }

        public WaitingRoom WaitingRoom { get => _waitingRoom; set => _waitingRoom = value; }
        public Barber Barber { get => _barber; set => _barber = value; }
        public Armchair Armchair { get => _armchair; set => _armchair = value; }        

        public void NewClient(Client client)
        {            
            Task.Factory.StartNew(() => client.DoActions(this));
        }

        public void Working()
        {
            Task.Factory.StartNew(() => Barber.DoActions(this));
            while (true)
            {
                NewClient(ClientFactory.CreateNewClient());                
                Thread.Sleep(Ping.MediumPing);
            }
        }
    }
}
