# Úvod 
Jedná se o vzorový projekt, jehož cílem je poskytnout příklady použití Universal Remoting knihoven.

# Popis projektu
Všechny příklady v projektu jsou napsány pro Microsoft .NET 6. Ideálním vývojovým prostředím pro spuštění projektu je Microsoft Visual Studio 2022. 
Samotné [nuget balíčky pro Universal Remoting](https://www.nuget.org/packages/TescoSW.OW.Remoting.Universal/) jsou ve verzi Microsoft .NET Standard 2.0, takže je můžete použít téměř v jakémkoli vlastním projektu.
* Adresář **Clients** - obsahuje příklady použití Universal Remoting.
* Adresář **Server** - obsahuje demo aplikační server.

# Příklady použití (adresář Clients)

##TestBlazor
Jednoduchý, ukázkový klient vyhotovený za využití [Microsoft ASP.NET Core Blazor](https://docs.microsoft.com/cs-cz/aspnet/core/blazor/?view=aspnetcore-6.0) a [WebAssembly](https://webassembly.org/).

##TestWebAPI
Ukázka jednoduchého [Microsoft ASP.NET Core Web API](https://docs.microsoft.com/cs-cz/aspnet/core/web-api/?view=aspnetcore-6.0) serveru, který pomocí Universal Remoting knihoven získává data z aplikačního serveru a přeposílá je ve vlastním formátu.

##TestWPFApp
Rozsáhlejší příklad, který demonstruje, jak je možné Universal Remoting knihovny použít v klientské aplikace založené na [Microsoft WPF](https://docs.microsoft.com/cs-cz/dotnet/desktop/wpf/?view=netdesktop-6.0). V ukázce lze vidět, jak je možné 
použít binding vlastností objektu na serveru do editačních polí v rámci XAML, například: `<TextBox x:Name="invoiceNumber" Text="{Vazba Cislo_faktury}"></TextBox>`. V dílčích oknech aplikace jsou pak také demonstrovány různé způsoby čtení dat z 
aplikačního serveru, například přímým odkazováním se na vlastnosti v serverových třídách nebo vytvoření lokálního DTO objektu a čtení dat přes [LINQ](https://docs.microsoft.com/cs-cz/dotnet/csharp/programming-guide/concepts/linq/).

# Sestavení a testování

##TestBlazor
Pro spuštění příkladu TestBlazor je nutné vybrat jako Startup Project *URDemo.Server* a profil *URDemo.TestBlazor*.

##TestWebAPI a TestWPFApp
U projektu *URDemo.Server* nastavte aktivní profil *OnlyConsole*. V panelu Solution Explorer klikněte pravím tlačítkem na Solution a v pop-up menu vyberte položku Properties. V sekci Startup Project vyberte Multiple startup projects. 
Ve sloupci Action vyberte *Start* u projektu *URDemo.Server* a pak u *TestWebAPI*, nebo *TestWPFApp*, podle toho, který chcete spustit.