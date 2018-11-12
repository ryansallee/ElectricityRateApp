#Electricity Rate App

##Purpose
The Electricity Rate App is a simple console app that allows a user to find the name
of an electric utility provider, make a calculation of usage-based charges, or compare 
electric utility rates between cities. In addition, the app persists these results to a database
via EF so that a history of these requests can be obtained. The source data is from [https://catalog.data.gov/dataset/u-s-electric-utility-companies-and-rates-look-up-by-zipcode-2016/resource/3770c037-618d-4510-b798-10fb553b55f1]
(U.S. Electric Utility Companies and Rates: Look-up by Zipcode (2016)), and this CSV is
deserialized and persisted to a database via EF.