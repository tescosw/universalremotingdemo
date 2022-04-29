# Introduction 
There are several examples in the project that show how Universal Remoting can be used.

# Project description
The examples in the project are written for .NET 6.
The ideal development environment for running the project is Visual Studio 2022. 
The Universal Remoting packages themselves are in .NET Standard 2.0, so you can use them in almost any custom project.

In the Clients folder there are several examples of using Universal Remoting.

####TestBlazor
A simple sample client in WebAssembly Blazor.

####TestWebAPI
A sample of a simple WebAPI server that uses Universal Remoting to get data from a data server and forward it in a custom format.

####TestWPFApp
A slightly more extensive example that shows how Universal Remoting can be used in a client environment. The sample shows how you can use binding properties of object instances on the server to edit fields in XAML.
For example: `<TextBox x:Name="invoiceNumber" Text="{Binding Cislo_faktury}"></TextBox>`.
Various examples of reading data from the server are displayed on different windows. By directly referencing properties in server classes, or by creating a local DTO object and reading the data via LINQ.


# Build and Test

####TestBlazor
To run the TestBlazor example, you must select Startup Project URDemo.Server and profile URDemo.TestBlazor

####TestWebAPI and TestWPFApp
Select URDemo.Server and profile OnlyConsole as the Startup Project.

In the Solution Explorer, right-click Solution and select Properties from the pop-up menu. In the Startup Project section, select Multiple startup projects. In the Action column, select Start for the URDemo.Server project and then TestWebAPI or TestWPFApp, whichever you want to run.