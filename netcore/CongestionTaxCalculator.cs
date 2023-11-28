using congestion.calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using Tax_Calculator.DataLayer;
using Tax_Calculator.DataLayer.Tax_Calculator.DataLayer;

namespace CongestionCalculator
{
    public class CongestionTaxCalculator
    {

        Tax_Calculator_DBContext dbContext = new Tax_Calculator_DBContext();
        List<CongestionTaxRule> congestionTaxRules = new List<CongestionTaxRule>();
        List<TollFreeVehicle> TollFreeVehicle = new List<TollFreeVehicle>();
        public CongestionTaxCalculator()
        {
            congestionTaxRules = dbContext.CongestionTaxRules.ToList();
            TollFreeVehicle = dbContext.TollFreeVehicles.ToList();
        }

        public int GetTax(Vehicle vehicle, DateTime[] dates)
        {
            if (dates == null || dates.Length == 0)
                return 0;

            int totalFee = 0;
            DateTime intervalStart = dates[0];

            foreach (DateTime date in dates)
            {
                int nextFee = GetTollFee(date, vehicle);
                int tempFee = GetTollFee(intervalStart, vehicle);

                TimeSpan diff = date - intervalStart;
                double minutes = diff.TotalMinutes;

                if (minutes <= 60 && dates[0] != date)
                {
                    if (nextFee >= tempFee)
                    {
                        totalFee += nextFee;
                        totalFee -= tempFee;
                    }
                }
                else
                {
                    totalFee += nextFee;
                }

                intervalStart = date;
            }

            return Math.Min(totalFee, 60); // Cap the total fee at 60
        }

        private bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return false;
            string vehicleType = vehicle.GetVehicleType();

            if (TollFreeVehicle.Any(v => v.VehicleType.Trim().ToLower() == vehicleType.Trim().ToLower()))
                return true;
            return false;
        }

        public int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            TimeSpan time = date.TimeOfDay;


            foreach (var rule in congestionTaxRules.OrderBy(r => r.StartTime))
            {
                TimeSpan startTime = rule.StartTime;
                TimeSpan endTime = rule.EndTime;

                if (time >= startTime && time < endTime)
                {
                    return rule.Amount;
                }
            }

            return 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true; // Check if it's a weekend
            }

            // Fetch toll-free date data from the database for the year 2013
            var tollFreeDates = dbContext.TollFreeDates
                .ToList();

            // Check if the provided date falls within any of the toll-free date ranges
            foreach (var tollFreeDate in tollFreeDates)
            {
                if (date >= tollFreeDate.StartDate && date <= tollFreeDate.EndDate)
                {
                    return true; // Return true if the date falls within a toll-free range
                }
            }

            return false; // Return false if not a toll-free date
        }

    }
}
