using congestion.calculator;
using CongestionCalculator;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CongestionTaxCalculator calculator = new CongestionTaxCalculator();
            //DateTime[] dates = {
            //    new DateTime(2013, 6, 4, 6, 0, 0),
            //    new DateTime(2013, 6, 4, 7, 1, 0),
            //    new DateTime(2013, 6, 4, 8, 2, 0),
            //    new DateTime(2013, 6, 4, 9, 3, 0),
            //    new DateTime(2013, 6, 4, 10, 4, 0),
            //    new DateTime(2013, 6, 4, 11, 5, 0),
            //    new DateTime(2013, 6, 4, 12, 6, 0),
            //    new DateTime(2013, 6, 4, 13, 7, 0),
            //    new DateTime(2013, 6, 4, 14, 8, 0),
            //    new DateTime(2013, 6, 4, 15, 9, 0)
            //}; // Dates to exceed the maximum fee

            Vehicle vehicle = new Motorbike();
            DateTime[] dates = {
                new DateTime(2013, 6, 4, 6, 0, 0),
                new DateTime(2013, 6, 4, 7, 0, 0)
            }; // Dates within toll hours


            // Act
            int result = calculator.GetTax(vehicle, dates);
            Console.WriteLine(result);
            Console.Read();
        }
    }
}
