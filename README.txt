Autonomous electric vehicles
PROBLEM STATEMENT taken from TechGig
Design and implement a microservice for an autonomous electric vehicle system with the following features:
Check if the vehicle can reach the destination without charging.
Find charging stations - If the destination can not be reached with current charging level. Appropriate handling to be done if the destination cannot be reached even with charging
The features are described in detail below:
Check if vehicle can reach destination without charging
Create a microservice which exposes one REST endpoint and can receive the request in the given request format

{ "vin": "vehicle identification number eg: W1K2062161F0014", "source": "source name", "destination": "destination name" }

and the response format will be-

{ "transactionId": "043020211 //A unique numerical value", "vin": "W1K2062161F0014 //vehicle identification number", "source": "source name", "destination": "destination name", "distance": "100 //distance between the source and destination in miles", "currentChargeLevel": "1 //current charge level in percentage , 0<=charge<=100", "isChargingRequired": "true/false //whether the vehicle has to stop for charging?.If true populate charging stations", "chargingStations": [ "s1", "s2" ], "errors": [ { "Id": 8888, "description": "Unable to reach the destination with the current charge level" }, { "id": 9999, "description": "Technical Exception" } ] }
Find out whether the vehicle will be able to reach the destination with the current charge level without charging at any charging stations in between the source and the destination. Assume 1% of charge is required for travelling 1 mile.


Prerequisite -

Visual Studio 2019 for build and deploy from local or in any cloud environment, preference in Azure App service for a quick turnout. 
c# and API development knowledge. 

How to Build - 

Open Visual Studio 2019. 
Open the BenzAPI.sln file.
Build the project using ctrl+B.

How to Run - 
Create an App service in Microosft Azure Subscription 
Download the Appservice publish.xml file 
If you are having Visual Studio you can right click on the solution and deploy to App service. 

How to Test -

We can test the application from App service URL or Locatl hosting using the sample test cases.

In case of any concern please feel free to reach out below. 

LinkedIn - https://www.linkedin.com/in/sh-d-200/




