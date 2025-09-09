using Azure.Identity;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using ModelContextProtocol.Client;
using Spectre.Console;

AnsiConsole.MarkupLine("[bold yellow]Agents With MCP SMS Tool[/]");
AnsiConsole.WriteLine();

#region Environment Variables

var apiKey = Environment.GetEnvironmentVariable("ApiKey");
ArgumentException.ThrowIfNullOrEmpty(apiKey);
AnsiConsole.WriteLine($"Api Key found {apiKey}, proceeding...");
var apiBaseUrl = Environment.GetEnvironmentVariable("ApiBaseUrl");
ArgumentException.ThrowIfNullOrEmpty(apiBaseUrl);
AnsiConsole.WriteLine($"Api base URL found {apiBaseUrl}, proceeding...");
var phoneNumber = Environment.GetEnvironmentVariable("PhoneNumber");
ArgumentException.ThrowIfNullOrEmpty(phoneNumber);
AnsiConsole.WriteLine($"Phone number found {phoneNumber}, proceeding...");
var azureOpenAIBaseUrl = Environment.GetEnvironmentVariable("AzureOpenAIBaseUrl");
ArgumentException.ThrowIfNullOrEmpty(azureOpenAIBaseUrl);
AnsiConsole.WriteLine($"Azure AI Open URL found {azureOpenAIBaseUrl}, proceeding...");
var deploymentName = Environment.GetEnvironmentVariable("DeploymentName") ?? "gpt-4o";
AnsiConsole.WriteLine($"Deployment found {deploymentName}, proceeding...");

#endregion

var smsSendToolName = "SmsSendTool";

#region Semantic Kernel and MCP Client Setup

var transport = new SseClientTransport(new SseClientTransportOptions
{
    Name = smsSendToolName,
    Endpoint = new Uri(apiBaseUrl)
});

var mcpClient = await McpClientFactory.CreateAsync(transport);
var tools = await mcpClient.ListToolsAsync();
var builder = Kernel.CreateBuilder();
var defaultAzureCreds = new DefaultAzureCredential();
builder.AddAzureOpenAIChatCompletion(deploymentName, azureOpenAIBaseUrl, defaultAzureCreds);
var kernel = builder.Build();
var kernelFunctions = tools.Select(tool => tool.AsKernelFunction());
kernel.Plugins.AddFromFunctions(smsSendToolName, kernelFunctions);
var executionSettings = new AzureOpenAIPromptExecutionSettings
{
    Temperature = 0,
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

#endregion

var prompt = AnsiConsole.Ask<string>("Enter message to send via SMS:");
if (string.IsNullOrEmpty(prompt))
{
    AnsiConsole.MarkupLine("[red]No message provided, exiting...[/]");
    return;
}

prompt = "Send to phone number " + phoneNumber + " the following message via SMS: " + prompt + ". " +
         "Only respond if the message was sent successfully or if there has been an error. " +
         "If there is an error, provide the error message with short human readable summary " +
         "and the whole exception as detail.";
var result = await kernel.InvokePromptAsync(prompt, new(executionSettings));
var mcpResponse = result.GetValue<string>();
AnsiConsole.WriteLine("Response from MCP:");
AnsiConsole.MarkupLine($"[green]{mcpResponse}[/]");