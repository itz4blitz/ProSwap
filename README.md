# ProSwapApp

What is it?

Pro Swap is an n-tier marketplace for online game transactions. 

What's special about it?

Sellers are protected from fraud and fraudulant-chargebacks. Sellers can get paid immediately.

How can I test this code?

Install Visual Studio 2019. Create a new project and be sure to checkout the latest repository for this project on Github. 
You may have some dependencies (EntityFramework, Owin, etc) that need to be installed. This can be done by doing a test build
(Ctrl + Shift + B) in Visual Studio. Find any errors and within each error, make sure to to install any missing dependency by
highlighting the appropriate sections of code and using Ctrl + . or Alt + Enter and selecting the appropriate using statements
or Framework Installations that are provided. Once left with no errors, you can check backend data by setting the API layer as
the startup project in Visual Studio and debugging the code. Likewise, to work with the frontend UI, you can set the MVC layer as
the startup project.

I want to add my own objects/models. 

Great! Make sure from within Package Manager Console that the Data layer is the default project. Enable migrations for 
EntityFramework with "Enable-Migrations" at the console. Next create a migration with "add-migration MigrationName". Once
you've made changes at your model layer, you'll want to update here with "Update-Database" to reflect your models layer.

I want to add to this code.

Awesome. Feel free to do so and use the code as you wish. If you want to contribute to this project, please let me know.
