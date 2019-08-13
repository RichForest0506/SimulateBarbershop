using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimulateBarbershop
{
    enum BarberState
    {
        Work,
        Sleep
    }

    class Barber
    {
        private BarberState _barberState;

        internal BarberState BarberState { get => _barberState; set => _barberState = value; }

        public Barber()
        {
            _barberState = BarberState.Work;
        }

        public Barber(BarberState barberState)
        {
            _barberState = BarberState.Work;
        }
        
        public void DoActions(Barbershop barbershop)
        {
            while (true)
            {
                if (barbershop.Armchair.CurrentClient != null)
                {
                    DoWork(barbershop);
                }
                else
                {
                    barbershop.WaitingRoom.MutexRoom.WaitOne();
                    if (barbershop.Armchair.CurrentClient == null)
                    {
                        CheckWaitingRoom(barbershop);
                    }
                }
            }            
        }        

        public void DoWork(Barbershop barbershop)
        {            
            Console.WriteLine("B: Start working with a client {0}...\n", barbershop.Armchair.CurrentClient.Name);
            Thread.Sleep(Ping.MediumPing);
            Console.WriteLine("B: Finished working with a client {0}...\n", barbershop.Armchair.CurrentClient?.Name);
            barbershop.Armchair.CurrentClient.LeaveArmChair(barbershop);
        }

        public void CheckWaitingRoom(Barbershop barbershop)
        {
            Thread.Sleep(Ping.ShortPing);
            if (barbershop.WaitingRoom.Chairs.Count > 0)
            {
                barbershop.Armchair.CurrentClient = barbershop.WaitingRoom.Chairs.Dequeue();
                Console.WriteLine("B: The barber called the next client...\n", barbershop.Armchair.CurrentClient?.Name);
                Thread.Sleep(Ping.ShortPing);
                barbershop.WaitingRoom.MutexRoom.ReleaseMutex();
            }
            else
            {
                barbershop.WaitingRoom.MutexRoom.ReleaseMutex();
                Console.WriteLine("B: The barber found no clients in the wating room and goes to the bed...\n");
                _barberState = BarberState.Sleep;
                while (true)
                {
                    if (_barberState == BarberState.Work)
                    {
                        break;
                    }
                }
            }
        }
    }
}
