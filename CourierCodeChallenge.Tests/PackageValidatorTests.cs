using CourierCodeChallenge.Core.Models;
using CourierCodeChallenge.Core.Validators;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierCodeChallenge.Tests
{
    public class PackageValidatorTests
    {
        public const int maxCarriableLoad = 50;
        public readonly PackageValidator _validator = new PackageValidator(maxCarriableLoad);

        [Fact]
        public void Should_Give_Error_When_Weight_Is_Less_Than_Zero()
        {
            // Arrange
            var package = new Package("PKG1 -50 30 OFR001");

            // Act
            var result = _validator.TestValidate(package);

            // Assert
            result.ShouldNotHaveValidationErrorFor(a => a.Distance);
            result.ShouldHaveValidationErrorFor(a => a.Weight);
            Assert.Equal("Package weight should be greater than 0", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_Give_Error_When_Weight_Is_Greater_Than_MaxLoad()
        {
            // Arrange
            var package = new Package("PKG1 52 30 OFR001");

            // Act
            var result = _validator.TestValidate(package);

            // Assert
            result.ShouldNotHaveValidationErrorFor(a => a.Distance);
            result.ShouldHaveValidationErrorFor(a => a.Weight);
            Assert.Equal("Package weight(52) should not be greater than max load(50)", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_Give_Error_When_Distance_Is_Less_Than_Zero()
        {
            // Arrange
            var package = new Package("PKG1 50 -30 OFR001");

            // Act
            var result = _validator.TestValidate(package);

            // Assert
            result.ShouldNotHaveValidationErrorFor(a => a.Weight);
            result.ShouldHaveValidationErrorFor(a => a.Distance);
            Assert.Equal("Package delivery distance should be greater than 0", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_Give_No_Error_When_Package_Is_Valid()
        {
            // Arrange
            var package = new Package("PKG1 50 30 OFR001");

            // Act
            var result = _validator.TestValidate(package);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
