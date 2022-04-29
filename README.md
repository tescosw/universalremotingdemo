# Úvod 
V projektu je několik příkladů, které ukazují, jak lze Universal Remoting použít.

# Popis projektu
Příklady v projektu jsou napsány pro .NET 6.
Ideálním vývojovým prostředím pro spuštění projektu je Visual Studio 2022. 
Samotné balíčky Universal Remoting jsou ve verzi .NET Standard 2.0, takže je můžete použít téměř v jakémkoli vlastním projektu.

Ve složce Clients je několik příkladů použití Universal Remoting.

####TestBlazor
Jednoduchý ukázkový klient ve WebAssembly Blazoru.

####TestWebAPI
Ukázka jednoduchého WebAPI serveru, který pomocí Universal Remotingu získává data z datového serveru a přeposílá je ve vlastním formátu.

####TestWPFApp
Trochu rozsáhlejší příklad, který ukazuje, jak je možné Universal Remoting použít v klientském prostředí. V ukázce je vidět, jak je možné použít binding vlastností instancí objektů na serveru do editačních polí v XAML.
Například: `<TextBox x:Name="invoiceNumber" Text="{Vazba Cislo_faktury}"></TextBox>`.
V různých oknech jsou ukázány různé příklady k čtení dat ze serveru. Přímím odkazováním se na vlastnosti v serverových třídách, nebo vytvoření lokálního DTO objektu a čtení dat přes LINQ.

# Sestavení a testování

####TestBlazor
Pro spuštění příkladu TestBlazor je nutné vybrat Startup Project URDemo.Server a profil URDemo.TestBlazor.

####TestWebAPI a TestWPFApp
Jako Startup Project vyberte URDemo.Server a profil OnlyConsole.

V panelu Solution Explorer klikněte pravím tlačítkem na Solution a v pop-up menu vyberte položku Properties. V sekci Startup Project vyberte Multiple startup projects. Ve sloupci Action vyberte Start u projektu URDemo.Server a pak u TestWebAPI, nebo TestWPFApp, podle toho, který chcete spustit.
