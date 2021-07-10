using System;
using System.Diagnostics;
using System.Linq;

namespace DisposeApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicles vehicles = new Vehicles();
            vehicles.Cars.ToList().ForEach(Console.WriteLine);
            Console.WriteLine(vehicles.Cars.Count);
            Console.WriteLine(GC.GetTotalMemory(false));
            vehicles.Dispose();
            Console.WriteLine(vehicles.Cars.Count);
            Console.WriteLine(GC.GetTotalMemory(false));
            Console.WriteLine(GC.GetGeneration(vehicles));
        
            // Не вдалося викликати збір сміття вручну та викликати деструктори.
            VehiclesWrapper vw = new VehiclesWrapper();
            Console.WriteLine(vw.Vehicle[0].Cars.Count);
            vw.Vehicle.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            if (Debugger.IsAttached) // Прапор, який вказує на те, чи включений режим відладки.
            {
                Console.ReadLine();
            }
        }
    }
}