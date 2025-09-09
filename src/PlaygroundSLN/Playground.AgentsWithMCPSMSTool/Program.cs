using Spectre.Console;

AnsiConsole.MarkupLine("[bold yellow]Agents With MCP SMS Tool[/]");
AnsiConsole.WriteLine();
var apiKey = Environment.GetEnvironmentVariable("ApiKey");
ArgumentException.ThrowIfNullOrEmpty(apiKey);
AnsiConsole.WriteLine("Api Key found, proceeding...");
