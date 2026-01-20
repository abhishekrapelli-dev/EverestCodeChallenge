# CourierCodeChallenge

This is a C# Console Application that solves two courier service problems in a step-by-step, interactive manner.

Command line application:
- Accepts all input via command-line
- Validates immediately to make sure inputs are entered correctly.
- Processes data only after valid input is received
- Guides the user clearly through both problem statements

Once all inputs are collected, the application greets you with Thank You message and proceeds to compute results for the selected problem.

**************************************************************
Problem 1: Delivery Cost Estimation with Offers
**************************************************************
This problem calculates the delivery cost of packages, applying offer codes wherever applicable

It takes package info in specific below format seperated by single space, and process it to form collection of packages.
Packages are internally validated against basic checks and then processed further to evaluate Delivery Cost based on applied offer code.

Input Format :
[Package_Name]<space>[Weight]<space>[Distance]<space>[Offer_Code]

Note : The application gracefully handles cases where no offer code is provided.

**************************************************************
Problem 2: Delivery Time Estimation
**************************************************************
This problem estimates the delivery time for packages based on weight, distance, and vehicle constraints.

Input Format is same as Problem 1, though Offer code is ignored for time estimation but accepted for consistency.

Delivery Time Rules : 

- Shipment should contain max packages vehicle can carry in a trip.
- We should prefer heavier packages when there are multiple shipments with the same no. of packages.
- If the weights are also the same, preference should be given to the shipment which can be delivered first.


Application Features : 

- Interactive command-line interface
- Immediate input validation
- Modular and readable code structure
- Handles optional inputs safely
- Designed for extensibility and clarity
- Easily introduce new coupon codes


How to Run the command line application

- Clone the repository
- Open & build the solution in Visual Studio
- Run the console application
- Follow on-screen instructions to provide inputs
