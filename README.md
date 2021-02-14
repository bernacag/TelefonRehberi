# Telefon Rehberi API
This project was made to keep contacts in the phone book.
It lists by creating a contact, deleting a contact, contact location, contact information about the person and the location of the people.
Telephone, e-mail, location information were entered as Information Type.
Enum type; 0=telephone, 1=e-mail, 2=location.

# Getting Started


Prerequisites
You will need the following tools:
Visual Studio 2019
.Net Core 3.1
EF Core 3.1

Installing
Follow these steps to get your development environment set up:

Clone the repository
At the root directory, restore required packages by running:
dotnet restore
Next, build the solution by running:
dotnet build
Next, within the TelefonRehberi.API directory, launch the back end by running:
dotnet run
Launch http://localhost:44375/ in your browser to view the Web UI.
If you have Visual Studio after cloning Open solution with your IDE, TelefonRehberi.API should be the start-up project. Directly run this project on Visual Studio with F5 or Ctrl+F5. You will see index page of project, you can navigate product and category pages and you can perform crud operations on your browser.
