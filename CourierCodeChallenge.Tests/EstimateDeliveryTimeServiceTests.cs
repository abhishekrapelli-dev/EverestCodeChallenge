using CourierCodeChallenge.Core.Models;
using CourierCodeChallenge.Core.Services;

namespace CourierCodeChallenge.Tests
{
    public class EstimateDeliveryTimeServiceTests
    {
        [Fact]
        public void Should_Calculate_Valid_Delivery_Time()
        {
            var vehicleCount = 2;
            var maxSpeed = 50;
            var maxLoad = 175;
            var basePrice = 50;
            var packageDetails = new List<string> { "PKG1 50 30 OFR001", "PKG4 110 60 OFR002", "PKG5 155 95 NA" };

            // Arrange
            var packages = packageDetails.Select(x => new Package(x)).ToList();

            var service = new EstimateDeliveryTimeService(vehicleCount, maxSpeed, maxLoad, basePrice);

            // Act
            service.Calculate(packages);

            // Assert
            Assert.True(packages.First().DeliveryTime > 0);
        }

        [Fact]
        public void Should_Calculate_Delivery_Time_Correctly()
        {
            var vehicleCount = 2;
            var maxSpeed = 50;
            var maxLoad = 200;
            var basePrice = 50;
            var packageDetails = new List<string> { "PKG1 50 30 OFR001", "PKG4 110 60 OFR002", "PKG5 155 95 NA" };

            // Arrange
            var packages = packageDetails.Select(x => new Package(x)).ToList();
            var service = new EstimateDeliveryTimeService(vehicleCount, maxSpeed, maxLoad, basePrice);
            var outputDeliveryCostService = new ConsoleOutputDeliveryCostService();

            // Act
            service.Calculate(packages);
            outputDeliveryCostService.WriteLine(packages);

            // Assert
            var package1 = packages.First(x => x.Id == "PKG1");
            Assert.Equal("0.60", package1.DeliveryTime.ToString("F2"));

            var package4 = packages.First(x => x.Id == "PKG4");
            Assert.Equal("1.20", package4.DeliveryTime.ToString("F2"));

            var package5 = packages.First(x => x.Id == "PKG5");
            Assert.Equal("1.90", package5.DeliveryTime.ToString("F2"));
        }


        [Fact]
        public void Should_Throw_Format_Exception_For_Invalid_Package_Details()
        {
            var vehicleCount = 2;
            var maxSpeed = 50;
            var maxLoad = 200;
            var basePrice = 50;
            var packageDetails = new List<string> { "PKG1 50 30 OFR001", "PKG4 110 60 OFR002", "PKG5 155 95 NA" };

            // Arrange
            var packages = packageDetails.Select(x => new Package(x)).ToList();
            var service = new EstimateDeliveryTimeService(vehicleCount, maxSpeed, maxLoad, basePrice);
            var outputDeliveryCostService = new ConsoleOutputDeliveryCostService();

            // Act
            service.Calculate(packages);
            outputDeliveryCostService.WriteLine(packages);

            // Assert
            var package1 = packages.First(x => x.Id == "PKG1");
            Assert.Equal("0.60", package1.DeliveryTime.ToString("F2"));

            var package4 = packages.First(x => x.Id == "PKG4");
            Assert.Equal("1.20", package4.DeliveryTime.ToString("F2"));

            var package5 = packages.First(x => x.Id == "PKG5");
            Assert.Equal("1.90", package5.DeliveryTime.ToString("F2")); 
        }
    }
}