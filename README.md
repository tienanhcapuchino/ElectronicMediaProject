# ElectronicMediaProject

# Important note: you can delete file in folder Images, but ***MUSTN'T DELETE FOLDER IMAGES***

# How to work

## 1. Install tool and environment

#### Install [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) 

#### Install [Node JS v.17](https://nodejs.org/en/blog/release/v17.7.1)

#### Install [SQL Server 2022](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## 2. Follow the steps bellow to run project

#### Step 1: Open ElectronicMediaProject.sln solution
#### Step 2: Open ***appsettings.json*** file in ***ElectronicMediaAPI*** to update your environment
#### Step 3: Open ***Package Manager Console*** and choose ***Default project*** is ***ElectronicMedia.Core***
#### Step 4: Run command ***add-migration initDb***
#### Step 5: Run command ***update-database*** (you can open SSMS to check database have been created or not)
#### Step 6: Run project ***ElectronicMediaAPI*** 
#### Step 7: open command prompt (cmd) of folder ElectronicUI
#### Step 8: run command ***npm install***
#### Step 9: run command ***npm run start***
#### Note important: the database will be empty, so you can use swagger when run project ElectronicMediaAPI or use SSMS to create data first.

## Enjoy the project

#### Thanks so much!
