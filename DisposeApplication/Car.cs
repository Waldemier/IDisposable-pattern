using System;

namespace DisposeApplication
{
    public class Car
    {
        public string Mark { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return $"Mark: {this.Mark}, Color: {this.Color}";
        }
        
        ~Car()
        {
            Console.WriteLine($"The {nameof(Car)} destructor is executing.");
        }
    }
}