using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulateBarbershop
{
    class Client
    {
        private String _name;

        public Client()
        {
            _name = Guid.NewGuid().ToString();
        }

        public Client(string name)
        {
            _name = name;
        }

        public Client(string name, Barbershop barbershop)
        {
            _name = name;
            DoActions(barbershop);
        }

        public string Name { get => _name; set => _name = value; }

        public void DoActions(Barbershop barbershop)
        {            
            CheckBarber(barbershop);            
        }

        private void CheckBarber(Barbershop barbershop)
        {
            barbershop.WaitingRoom.MutexRoom.WaitOne();
            Console.WriteLine("C{0}: Enters the barber shop...\n", _name);
            Thread.Sleep(Ping.ShortPing);
            if (barbershop.Armchair.CurrentClient == null && barbershop.WaitingRoom.Chairs.Count == 0 && barbershop.Barber.BarberState == BarberState.Sleep)
            {
                WakeUpBarber(barbershop);
                barbershop.WaitingRoom.MutexRoom.ReleaseMutex();
            }            
            else
            {
                DoActionIfBarberIsWorking(barbershop);
                barbershop.WaitingRoom.MutexRoom.ReleaseMutex();
            }
        }

        private void WakeUpBarber(Barbershop barbershop)
        {
            Console.WriteLine("C{0}: Wakes up the barber and takes a chair...\n", _name);
            Thread.Sleep(Ping.ShortPing);
            barbershop.Armchair.CurrentClient = this;
            barbershop.Barber.BarberState = BarberState.Work;
        }

        private void DoActionIfBarberIsWorking(Barbershop barbershop)
        {
            Console.WriteLine("C{0}: Go to the waiting room because the barber is busy...\n", _name);
            Thread.Sleep(Ping.ShortPing);

            Console.WriteLine("C{0}: Checks if there is an empty chair in the waiting room...\n", _name);            
            if (barbershop.WaitingRoom.Chairs.Count < 3)
            {
                WaitInTheWaitingRoom(barbershop);
            }
            else
            {
                LeaveBarberShop();
            }
            Thread.Sleep(Ping.ShortPing);
        }

        private void WaitInTheWaitingRoom(Barbershop barbershop)
        {
            Console.WriteLine("C{0}: Found an empty chair and takes it...\n", _name);
            barbershop.WaitingRoom.Chairs.Enqueue(this);
            Thread.Sleep(Ping.ShortPing);
        }

        private void LeaveBarberShop()
        {
            Console.WriteLine("C{0}: Did not find an empty chairs and leaves...\n", _name);
            Thread.Sleep(Ping.ShortPing);
        }

        public void LeaveArmChair(Barbershop barbershop)
        {
            Console.WriteLine("C{0}: Trimmed and going home...\n", _name);            
            barbershop.Armchair.CurrentClient = null;
            Thread.Sleep(Ping.ShortPing);
        }

        public override string ToString()
        {
            return "№" + _name;
        }
    }
}
