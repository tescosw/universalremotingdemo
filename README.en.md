#Introduction 
This project contains examples showing possible usage of Universal Remoting libraries.

#Project description
All of the examples in the project are written for Microsoft .NET 6.
The ideal development environment for running the project is Microsoft Visual Studio 2022. 
The [Universal Remoting nuget packages](https://www.nuget.org/packages/TescoSW.OW.Remoting.Universal/) are in Microsoft .NET Standard 2.0, so you can use them in almost any of your custom projects.
* **Clients** folder - contains examples of usage of the Universal Remoting libraries.
* **Server** folder - contains demo application server.

#Examples (Clients folder)

##TestBlazor
A simple example of client based on [Microsoft ASP.NET Core Blazor](https://docs.microsoft.com/cs-cz/aspnet/core/blazor/?view=aspnetcore-6.0) and [WebAssembly](https://webassembly.org/).

##TestWebAPI
A simple example of [Microsoft ASP.NET Core Web API](https://docs.microsoft.com/cs-cz/aspnet/core/web-api/?view=aspnetcore-6.0) server that uses Universal Remoting libraries to get data from a application server and forward it in a custom format.

##TestWPFApp
A slightly more extensive example that shows how Universal Remoting can be used in a client application based on [Microsoft WPF](https://docs.microsoft.com/cs-cz/dotnet/desktop/wpf/?view=netdesktop-6.0). The sample shows how you can use binding 
properties of object on the server to edit fields in XAML, for example: `<TextBox x:Name="invoiceNumber" Text="{Binding Cislo_faktury}"></TextBox>`.
Various examples of reading data from the application server are shown on different application windows, for example by directly referencing properties of server classes or by creating a local DTO object and reading 
the data via [LINQ](https://docs.microsoft.com/cs-cz/dotnet/csharp/programming-guide/concepts/linq/).

#Build and Test

##TestBlazor
To run the TestBlazor example, you must select Startup Project *URDemo.Server* and profile *URDemo.TestBlazor*.

##TestWebAPI and TestWPFApp
For the *URDemo.Server* project, set the *OnlyConsole* profile as active. In the Solution Explorer, right-click Solution and select Properties from the pop-up menu. In the Startup Project section, select 
Multiple startup projects. In the Action column, select *Start* for the *URDemo.Server* project and then *TestWebAPI* or *TestWPFApp*, whichever you want to run.