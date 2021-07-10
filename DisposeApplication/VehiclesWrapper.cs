using System;
using System.Collections.Generic;

namespace DisposeApplication
{
    public class VehiclesWrapper
    {
        public List<Vehicles> Vehicle
        {
            get;
            set;
        }

        public VehiclesWrapper()
        {
            this.Vehicle = new List<Vehicles>();
            var vehicle = new Vehicles();
            this.Vehicle.Add(vehicle);
        }
        
        ~VehiclesWrapper()
        {
            Console.WriteLine("VehiclesWrapper destructor is execution.");
        }
    }
}