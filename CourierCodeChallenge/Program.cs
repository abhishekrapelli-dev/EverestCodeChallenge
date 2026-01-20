// See https://aka.ms/new-console-template for more information
using CourierCodeChallenge.Core.Models;
using CourierCodeChallenge.Core.Services;
using CourierCodeChallenge.Core.Services.Abstraction;
using CourierCodeChallenge.Core.Validators;
using FluentValidation;
using FluentValidation.Results;

bool keepRunning = true;

while (keepRunning)
{
    Console.Clear();

    Console.WriteLine("Welcome, to Courier Management System!");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("Kindly enter base delivery cost and number of packages in number format only, as '[BaseDeliveryCost]<space>[No. of Packages]'");

    int baseDeliveryCost = 0;
    int numberOfPackages = 0;

// Read base delivery cost & number of packages
ReadDeliveryCostAndPackagesCount:
    var readDeliveryCostAndNoOfPackages = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

    if (readDeliveryCostAndNoOfPackages.Length == 2)
    {
        if (!int.TryParse(readDeliveryCostAndNoOfPackages[0], out baseDeliveryCost)
            || !int.TryParse(readDeliveryCostAndNoOfPackages[1], out numberOfPackages))
        {
            Console.WriteLine("Error => Base Delivery Cost or Number of Packages is not entered in correct format");
            Console.WriteLine("Kindly enter base delivery cost and number of packages in number format only, as '[DeliveryCost]<space>[No. of Packages]'");
            goto ReadDeliveryCostAndPackagesCount;
        }
    }
    else
    {
        Console.WriteLine("Error => Incomplete details entered, Please enter as '[BaseDeliveryCost]<space>[No. of Packages]'");
        goto ReadDeliveryCostAndPackagesCount;
    }

    Console.WriteLine("Base Delivery Cost => " + baseDeliveryCost);
    Console.WriteLine("Number of Packages => " + numberOfPackages);

    Console.WriteLine("");

    Console.WriteLine("Kindly enter availble vehicle count, max speed and max load of the vehicle in below format.");
    Console.WriteLine("[No. Of Vehicle]<space>[Max Speed]<space>[Max Load]");

// Read vehicle info
ReadVehicleMetadata:

    var vehicleInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var vehicleCount = 0;
    var maxSpeed = 0;
    var maxLoad = 0;

    if (vehicleInfo.Length == 3)
    {
        if (!int.TryParse(vehicleInfo[0], out vehicleCount)
            || !int.TryParse(vehicleInfo[1], out maxSpeed)
            || !int.TryParse(vehicleInfo[2], out maxLoad))
        {
            Console.WriteLine("Error => Vehicle Count/Max Speed/Max Load is not a valid number.");
            Console.WriteLine("Kindly re-enter in correct format.");
            goto ReadVehicleMetadata;
        }
    }
    else
    {
        Console.WriteLine("Error => Incomplete details entered, Please enter as '[No. Of Vehicle]<space>[Max Speed]<space>[Max Load]'");
        goto ReadVehicleMetadata;
    }

    Console.WriteLine("Total Vehicle Available => " + vehicleCount);
    Console.WriteLine("Max Speed of Vehicle => " + maxSpeed);
    Console.WriteLine("Max Load => " + maxLoad);

    Console.WriteLine("");
    Console.WriteLine($"To calculate delivery cost & estimated delivery time, please enter package details in below format {numberOfPackages} times, \n[Package_Name]<space>[Weight]<space>[Distance]<space>[Offer_Code]");

    var packages = new List<Package>();

    // Read package lines
    for (int i = 0; i < numberOfPackages; i++)
    {
    ReadPackageDetails:
        Console.WriteLine("");
        Console.WriteLine($"Enter package {i + 1} details.");
        var packageDetails = Console.ReadLine();

        try
        {
            var package = new Package(packageDetails);
            var validator = new PackageValidator(maxLoad);

            ValidationResult results = validator.Validate(package);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine($"Error=> {failure.ErrorMessage}");
                }

                goto ReadPackageDetails;
            }

            if (packages.FirstOrDefault(x => x.Id == package.Id) == null)
                packages.Add(package);
            else
            {
                Console.WriteLine($"Package details for Id, '{package.Id}' already exists.");
                Console.WriteLine("Do you wish to override? (Y/N)");

                var isOverride = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(isOverride) || isOverride.Equals("N", StringComparison.OrdinalIgnoreCase))
                    goto ReadPackageDetails;
                else
                {
                    packages.Remove(packages.First(x => x.Id == package.Id));
                    packages.Add(package);
                    Console.WriteLine($"Package details for Id, '{package.Id}' is updated.");
                    goto ReadPackageDetails;
                }
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine(ex.Message);
            goto ReadPackageDetails;
        }
    }

    Console.WriteLine("");
    Console.WriteLine($"Thank You for entering all {numberOfPackages} package details");

    Console.WriteLine("");
    Console.WriteLine("Great!, All information is captured successfully.");
    Console.WriteLine("");


ExecuteProblem:
    Console.WriteLine("Please enter either problem number 1 or 2 to check the results.");

    int problemNumber = 0;

    if (!int.TryParse(Console.ReadLine(), out problemNumber))
    {
        Console.WriteLine("Error => Problem number should be either 1 or 2 only.");
        Console.WriteLine("");
        goto ExecuteProblem;
    }

    Console.WriteLine($"Ok, executing problem {problemNumber} solution.");

    IEstimate estimate;
    IConsoleOutput consoleOutput;

    if (problemNumber == 1)
    {
        estimate = new EstimateDeliveryCostService(baseDeliveryCost);
        consoleOutput = new ConsoleOutputDeliveryCostService();
    }
    else if (problemNumber == 2)
    {
        estimate = new EstimateDeliveryTimeService(vehicleCount, maxSpeed, maxLoad, baseDeliveryCost);
        consoleOutput = new ConsoleOutputDeliveryTimeService();
    }
    else
        goto ExecuteProblem;

    estimate.Calculate(packages);

    Console.WriteLine("");
    Console.WriteLine("*****************************************************************");
    Console.WriteLine($"Displaying... problem {problemNumber} result,");
    Console.WriteLine("*****************************************************************");
    consoleOutput.WriteLine(packages);
    Console.WriteLine("*****************************************************************");

    Console.WriteLine("");
    Console.WriteLine("Do you want to execute another problem? (Y/N)");
    var isExecuteAnotherProblem = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(isExecuteAnotherProblem))
        goto ExecuteProblem;

    if (isExecuteAnotherProblem.Equals("Y", StringComparison.OrdinalIgnoreCase))
    {
        keepRunning = true;
        goto ExecuteProblem;
    }
    else
        keepRunning = false;
}

