using CourierCodeChallenge.Core.Models;
using CourierCodeChallenge.Core.Services;

namespace CourierCodeChallenge.Tests
{
    public class EstimateDeliveryCostServiceTests
    {
        [Fact]
        public void Should_Throw_InvalidData_Exception_For_Invalid_Package_Details()
        {
            // Arrange
            var packageDetails = new List<string> { "PKG1 fwe as OFR001" };

            // Act & Assert
            var result = Assert.Throws<InvalidDataException>(() => packageDetails.Select(x => new Package(x)).ToList());
            Assert.Equal("Distance & Weight should be a number, please enter package details in above format", result.Message);
        }

        [Fact]
        public void Should_Throw_Format_Exception_For_Invalid_Package_Details()
        {
            // Arrange
            var packageDetails = new List<string> { "PKG1 fwe as OFR001 asas" };

            // Act & Assert
            var result = Assert.Throws<FormatException>(() => packageDetails.Select(x => new Package(x)).ToList());
            Assert.Equal("Entered input is not in correct format, please enter package details in this format, \n[Package_Name]<space>[Weight]<space>[Distance]<space>[Offer_Code]", result.Message);
        }        

        [Fact]
        public void Should_Calculate_Valid_DeliveryCost()
        {
            var basePrice = 50;
            var packageDetails = new List<string> { "PKG1 50 30 OFR001", "PKG3 175 100 OFR003", "PKG5 155 95 NA" };

            // Arrange
            var packages = packageDetails.Select(x => new Package(x)).ToList();

            var service = new EstimateDeliveryCostService(basePrice);

            // Act
            service.Calculate(packages);

            // Assert
            Assert.True(packages.All(p => p.DeliveryCost > 0));
        }

        [Fact]
        public void Should_Calculate_DeliveryCost_And_DiscountPrice_Correctly()
        {
            var basePrice = 50;
            var packageDetails = new List<string> { "PKG1 50 30 OFR001", "PKG4 110 60 OFR002", "PKG5 155 95 NA" };

            // Arrange
            var packages = packageDetails.Select(x => new Package(x)).ToList();

            var service = new EstimateDeliveryCostService(basePrice);

            // Act
            service.Calculate(packages);

            // Assert
            var package1 = packages.First(x => x.Id == "PKG1");
            Assert.Equal("700.00", package1.DeliveryCost.ToString("F2"));
            Assert.Equal("0.00", package1.DiscountPrice.ToString("F2"));

            var package4 = packages.First(x => x.Id == "PKG4");
            Assert.Equal("1348.50", package4.DeliveryCost.ToString("F2"));
            Assert.Equal("101.50", package4.DiscountPrice.ToString("F2"));

            var package5 = packages.First(x => x.Id == "PKG5");
            Assert.Equal("2075.00", package5.DeliveryCost.ToString("F2"));
            Assert.Equal("0.00", package5.DiscountPrice.ToString("F2"));
        }
    }
}