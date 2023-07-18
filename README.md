# ElectronicMediaProject

# Important note: you can delete files in folder Images, but ***MUSTN'T DELETE FOLDER IMAGES***

# How to work

## 1. Install tool and environment

#### Install [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) 

#### Install [SQL Server 2022](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## 2. Follow the steps below to run the project

#### Step 1: Open ElectronicMediaProject.sln solution
#### Step 2: Open ***appsettings.json*** file in ***ElectronicMediaAPI*** to update your environment
#### Step 3: Open ***Package Manager Console*** and choose ***Default project*** is ***ElectronicMedia.Core***
#### Step 4: Run command ***add-migration initDb***
#### Step 5: Run command ***update-database*** (you can open SSMS to check database has been created or not)
#### Step 6: Right-click to Solution and choose Property
#### Step 7: Choose multiple start-up projects: Electronic.API and ElectronicWeb
#### Step 8: Run two projects at the same time
#### Note important: the database will be empty, so you can use Swagger when running project ElectronicMediaAPI or SSMS to create data first.

## Enjoy the project

#### Thanks so much!
