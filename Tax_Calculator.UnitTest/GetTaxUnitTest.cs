using congestion.calculator;
using System;
using Xunit;

namespace CongestionCalculator.Tests
{
    public class CongestionTaxCalculatorTests
    {
        [Fact]
        public void GetTax_WhenNoDatesProvided_ShouldReturnZero()
        {
            // Arrange
            CongestionTaxCalculator calculator = new CongestionTaxCalculator();
            Vehicle vehicle = new Car();
            DateTime[] dates = Array.Empty<DateTime>();

            // Act
            int result = calculator.GetTax(vehicle, dates);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetTax_WhenOneDateWithinTollHours_ShouldReturnCorrectFee()
        {
            // Arrange
            CongestionTaxCalculator calculator = new CongestionTaxCalculator();
            Vehicle vehicle = new Car();
            DateTime[] dates = { new DateTime(2013, 6, 4, 6, 15, 0) }; // A date within toll hours

            // Act
            int result = calculator.GetTax(vehicle, dates);

            // Assert
            Assert.Equal(8, result); // Assuming the toll fee for this time slot is 8
        }

        [Fact]
        public void GetTax_WhenMultipleDatesWithin60Minutes_ShouldReturnHighestFee()
        {
            // Arrange
            CongestionTaxCalculator calculator = new CongestionTaxCalculator();
            Vehicle vehicle = new Car();
            DateTime[] dates = {
                new DateTime(2013, 6, 4, 6, 0, 0),
                new DateTime(2013, 6, 4, 6, 30, 0),
                new DateTime(2013, 6, 4, 7, 0, 0)
            }; // Dates within 60 minutes

            // Act
            int result = calculator.GetTax(vehicle, dates);

            // Assert
            Assert.Equal(18, result); // Assuming the highest fee among these dates is 18
        }

        [Fact]
        public void GetTax_WhenTollFreeVehicleProvided_ShouldReturnZero()
        {
            // Arrange
            CongestionTaxCalculator calculator = new CongestionTaxCalculator();
            Vehicle vehicle = new Motorbike();
            DateTime[] dates = {
                new DateTime(2013, 6, 4, 6, 0, 0),
                new DateTime(2013, 6, 4, 7, 0, 0)
            }; // Dates within toll hours

            // Act
            int result = calculator.GetTax(vehicle, dates);

            // Assert
            Assert.Equal(0, result); // Tax should be zero for toll-free vehicles
        }

        [Fact]
        public void GetTax_WhenDateIsTollFreeDate_ShouldReturnZero()
        {
            // Arrange
            CongestionTaxCalculator calculator = new CongestionTaxCalculator();
            Vehicle vehicle = new Car();
            DateTime tollFreeDate = new DateTime(2013, 7, 1, 8, 0, 0); // A toll-free date

            // Act
            int result = calculator.GetTax(vehicle, new[] { tollFreeDate });

            // Assert
            Assert.Equal(0, result); // Tax should be zero for toll-free dates
        }

        [Fact]
        public void GetTax_WhenMultipleDatesExceedMaxFee_ShouldReturnMaxFee()
        {
            // Arrange
            CongestionTaxCalculator calculator = new CongestionTaxCalculator();
            Vehicle vehicle = new Car();
            DateTime[] dates = {
                new DateTime(2013, 6, 4, 6, 0, 0),
                new DateTime(2013, 6, 4, 7, 1, 0),
                new DateTime(2013, 6, 4, 8, 2, 0),
                new DateTime(2013, 6, 4, 9, 3, 0),
                new DateTime(2013, 6, 4, 10, 4, 0),
                new DateTime(2013, 6, 4, 11, 5, 0),
                new DateTime(2013, 6, 4, 12, 6, 0),               
                new DateTime(2013, 6, 4, 13, 7, 0),
                new DateTime(2013, 6, 4, 14, 8, 0),
                new DateTime(2013, 6, 4, 15, 9, 0)
            }; // Dates to exceed the maximum fee

            // Act
            int result = calculator.GetTax(vehicle, dates);

            // Assert
            Assert.Equal(60, result); // Tax should be capped at the maximum fee of 60
        }


        [Fact]
        public void GetTax_WhenSingleDateOutsideTaxablePeriod_ShouldReturnZero()
        {
            // Arrange
            CongestionTaxCalculator calculator = new CongestionTaxCalculator();
            Vehicle vehicle = new Car();
            DateTime nonTaxableDate = new DateTime(2013, 6, 22, 8, 0, 0); // A non-taxable date (Saturday)

            // Act
            int result = calculator.GetTax(vehicle, new[] { nonTaxableDate });

            // Assert
            Assert.Equal(0, result); // Tax should be zero for dates outside the taxable period
        }

        // Add more test cases for different scenarios as needed
    }
}

