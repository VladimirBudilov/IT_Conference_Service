# IT conference service
## Description

This project is an application management system that allows users to create, edit, delete, and submit applications for consideration. Each application contains the following data:

- User ID/Author ID (Guid, required)
- Activity type (one of the following: Lecture, Masterclass, Discussion, required)
- Title (string, up to 100 characters, required)
- Brief description for the website (string, up to 300 characters, optional)
- Plan (string, up to 1000 characters, required)

The system provides several operations, including creating an application, editing an application, deleting an application, submitting an application for consideration, retrieving applications submitted after a certain date, retrieving unsubmitted applications older than a certain date, retrieving the current unsubmitted application for a specific user, retrieving an application by ID, and retrieving a list of possible activity types.

The system ensures that a user can only have one unsubmitted application at a time, and it enforces various rules to ensure the integrity of the data. The system stores its state in a database, and the data is not lost after a restart. The database schema is described in migrations and is automatically deployed when the service starts.


## Prerequisites

- .NET 8
- Entity Framework Core
- PostgreSQL

## Getting Started

1. **Clone the Repository**

        git clone https://github.com/VladimirBudilov/IT_Conference_Service.git
2. Update NuGet Packages

3. Configure Connection String

      Update the connection string in the appsettings.json file located in the project root:

## Build and Run the Application


    dotnet build
    dotnet run --project IT_Conference_Service.csproj
Your application should now be running. Open a web browser and navigate to 
http://localhost:5000
to access it.
