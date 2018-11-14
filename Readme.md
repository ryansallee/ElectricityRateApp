# Electricity Rate App

## Purpose
The Electricity Rate App is a simple console app that allows a user to find the name
of an electric utility provider, make a calculation of usage-based charges, or compare 
electric utility rates between cities. In addition, the app persists these results to a database
via EF so that a history of these requests can be obtained. The source data is from [https://catalog.data.gov/dataset/u-s-electric-utility-companies-and-rates-look-up-by-zipcode-2016/resource/3770c037-618d-4510-b798-10fb553b55f1]
(U.S. Electric Utility Companies and Rates: Look-up by Zipcode (2016)), and this CSV is
deserialized and persisted to a database via EF. As well, this app also contains examples of the four principles
of Object-Oriented Programming: Abstraction, Encapsulation, Inheritance, and Polymorphism.

## Instructions
1. Clone the project from GitHub and run via Visual Studio (Open in Visual Studio link when cloning project).
2. Use NuGet Package Manager Console (Tools>NuGet Package Manager>Package Manager Console) and seed the Rates database with
test data by using the command update-database.
3. Run the the project from Debug. On the first time project is opened, AddPowerRates() will execute
and add residential rate information to the PowerRate table. Since there are 50,000 + lines in this CSV
this code will take anywhere from 10-20 minutes depending on your system.
4. Once AddPowerRates() has excuted, the main menu will be available to use.