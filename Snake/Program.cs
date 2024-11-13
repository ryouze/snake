namespace Snake;

using Spectre.Console;

public static class Program
{
    public static void Main()
    {
        // Force invariant culture for consistent behavior
        // CultureInfo.DefaultThreadCurrentUICulture = Thread.CurrentThread.CurrentUICulture =
        //     CultureInfo.InvariantCulture;

        // Print build type if in Debug mode
        Core.Debug.PrintBuildConfiguration();

        // Sample list of items
        var items = new[]
        {
            "Item 1",
            "Item 2",
            "Item 3",
            "Item 4",
            "Item 5",
            "Item 6",
            "Item 7",
            "Item 8",
            "Item 9",
            "Item 10",
        };

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an item from the list")
                .PageSize(5) // Number of items visible at once
                .AddChoices(items)
        );

        AnsiConsole.MarkupLine($"You selected: [green]{selected}[/]");
    }
}
