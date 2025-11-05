# Playground Agents with MCP

A demonstration repository showcasing how to build intelligent agent-based solutions using Azure OpenAI, Semantic Kernel, and Model Context Protocol (MCP) in C#. This project provides a practical example of integrating MCP tools with AI agents to enable powerful, extensible functionality like SMS messaging through natural language interactions.

## Technology Stack

### Core Technologies
- **.NET 9.0** - Latest .NET framework for high-performance applications
- **C#** - Primary programming language with nullable reference types enabled
- **Microsoft Semantic Kernel 1.64.0** - AI orchestration framework for building AI agents
- **Azure OpenAI** - GPT-4o model for natural language processing and function calling

### Key Dependencies
- **Azure.Identity 1.15.0** - Azure authentication using DefaultAzureCredential
- **ModelContextProtocolServer.Sse 0.4.0** - SSE-based MCP client for tool integration
- **Spectre.Console 0.51.1** - Rich console UI for enhanced terminal experience

## Project Architecture

This project demonstrates a **Model Context Protocol (MCP) integration pattern** with AI agents:

```
┌─────────────────┐
│   User Input    │
│  (Console UI)   │
└────────┬────────┘
         │
         ▼
┌─────────────────────────┐
│   Semantic Kernel       │
│   (AI Orchestration)    │
│   - Azure OpenAI        │
│   - Function Calling    │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│   MCP Client (SSE)      │
│   - Tool Discovery      │
│   - Function Mapping    │
└────────┬────────────────┘
         │
         ▼
┌─────────────────────────┐
│   SMS Send Tool (MCP)   │
│   - External Service    │
└─────────────────────────┘
```

**Key Architectural Patterns:**
- **MCP Tool Integration**: Dynamically discovers and registers MCP tools as Semantic Kernel functions
- **Azure Authentication**: Uses DefaultAzureCredential for secure Azure OpenAI access
- **Function Calling**: Leverages GPT-4o's function calling capabilities for automated tool invocation
- **SSE Transport**: Server-Sent Events (SSE) protocol for MCP communication

## Getting Started

### Prerequisites

Before you begin, ensure you have the following:

- **Azure Account** with access to:
  - Azure OpenAI Service (with GPT-4o deployment)
  - Azure CLI configured for authentication
- **.NET 9.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Visual Studio 2022** or **VS Code** with C# extension
- **MCP Server** instance with SMS tool configured and running
- **Phone number** registered with your MCP SMS service

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/bovrhovn/playground-agents-with-mcp.git
   cd playground-agents-with-mcp
   ```

2. **Navigate to the solution:**
   ```bash
   cd src/PlaygroundSLN
   ```

3. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

### Configuration

4. **Set up environment variables:**

   Create a `local.env` file in the root directory with the following variables:

   ```env
   ApiKey=your_mcp_api_key
   ApiBaseUrl=https://your-mcp-server-url
   PhoneNumber=+1234567890
   AzureOpenAIBaseUrl=https://your-openai-instance.openai.azure.com
   DeploymentName=gpt-4o
   ```

   **Using PowerShell to set environment variables:**
   ```powershell
   .\scripts\Set-EnvVariables.ps1 -EnvFileToReadFrom "local.env"
   ```

5. **Authenticate with Azure:**
   ```bash
   az login
   ```

### Running the Application

6. **Build and run:**
   ```bash
   cd src/PlaygroundSLN/Playground.AgentsWithMCPSMSTool
   dotnet run
   ```

7. **Using the application:**
   - The console will prompt you to enter a message
   - Type your message (e.g., "Hello, this is a test message")
   - The AI agent will automatically invoke the MCP SMS tool to send the message
   - You'll receive confirmation or error details in the console

## Project Structure

```
playground-agents-with-mcp/
├── .github/
│   └── agents/               # Custom agent definitions
│       └── my-agent.md       # README generator agent
├── scripts/
│   ├── Set-EnvVariables.ps1  # Environment setup script
│   └── Add-DirToSystemEnv.ps1
├── src/
│   └── PlaygroundSLN/
│       ├── PlaygroundSLN.sln # Visual Studio solution
│       └── Playground.AgentsWithMCPSMSTool/
│           ├── Program.cs    # Main application entry point
│           └── *.csproj      # Project file with dependencies
├── .gitignore
└── README.md
```

## Key Features

### 🤖 AI Agent with MCP Integration
- **Natural Language Processing**: Send SMS messages using conversational input
- **Automatic Tool Discovery**: Dynamically discovers available MCP tools at runtime
- **Function Calling**: GPT-4o automatically determines when and how to use the SMS tool

### 🔐 Secure Azure Integration
- **DefaultAzureCredential**: Seamless authentication using Azure CLI credentials
- **Environment-Based Configuration**: Secure credential management via environment variables
- **Azure OpenAI**: Enterprise-grade AI services with compliance and security

### 📱 MCP SMS Tool Integration
- **Server-Sent Events (SSE)**: Real-time communication with MCP server
- **Error Handling**: Comprehensive error messages with human-readable summaries
- **Status Reporting**: Clear feedback on message delivery success or failure

### 🎨 Enhanced Console Experience
- **Spectre.Console**: Rich, colorful terminal UI with markup support
- **Interactive Prompts**: User-friendly input collection
- **Formatted Output**: Clear display of operations and results

## Development Workflow

### Building the Project

```bash
# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run the application
dotnet run --project src/PlaygroundSLN/Playground.AgentsWithMCPSMSTool
```

### Development Best Practices

1. **Environment Variables**: Always use environment variables for sensitive data (API keys, endpoints)
2. **Azure CLI Authentication**: Prefer Azure CLI authentication for local development
3. **MCP Tool Testing**: Test MCP tools independently before integrating with agents
4. **Error Handling**: The application provides detailed error messages for troubleshooting

## Coding Standards

### C# Conventions
- **Nullable Reference Types**: Enabled project-wide for better null safety
- **Implicit Usings**: Enabled to reduce boilerplate
- **Async/Await**: All I/O operations use async patterns
- **Region Organization**: Code organized into logical regions (Environment Variables, Setup, etc.)

### Naming Conventions
- Use descriptive variable names (e.g., `azureOpenAIBaseUrl`, `deploymentName`)
- Follow C# naming conventions (PascalCase for methods, camelCase for local variables)
- Use meaningful region names to organize code sections

### Configuration Management
- All configuration values sourced from environment variables
- Validation with `ArgumentException.ThrowIfNullOrEmpty` for required values
- Default values provided where appropriate (e.g., `deploymentName`)

## Testing

### Manual Testing
Currently, the project includes manual testing through:
- Interactive console-based testing
- Real-time SMS delivery verification
- Error message validation

### Testing Checklist
- [ ] Environment variables properly configured
- [ ] Azure authentication successful
- [ ] MCP server accessible
- [ ] SMS tool discovery working
- [ ] Message sending functional
- [ ] Error handling appropriate

### Future Testing Enhancements
Consider adding:
- Unit tests for Semantic Kernel function integration
- Integration tests for MCP client communication
- Mock MCP servers for isolated testing

## Contributing

Contributions are welcome! This is a playground repository designed for experimentation and learning.

### How to Contribute
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Areas for Contribution
- Additional MCP tool integrations
- Enhanced error handling and logging
- Unit and integration tests
- Documentation improvements
- Additional agent examples

## Additional Resources

### Microsoft Documentation
- [Azure OpenAI Service](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/)
- [Semantic Kernel](https://learn.microsoft.com/en-us/semantic-kernel/)
- [Azure Identity Library](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/identity-readme)
- [.NET 9.0 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)

### MCP and AI Resources
- [Model Context Protocol Specification](https://modelcontextprotocol.io/)
- [Semantic Kernel GitHub](https://github.com/microsoft/semantic-kernel)
- [Spectre.Console Documentation](https://spectreconsole.net/)

### AI and Prompt Engineering
- [Prompt Engineering Guide](https://learn.microsoft.com/en-us/azure/ai-services/openai/concepts/prompt-engineering)
- [Azure OpenAI Best Practices](https://learn.microsoft.com/en-us/azure/ai-services/openai/concepts/best-practices)

## License

This is a playground/demonstration repository. Please check with the repository owner for specific license terms.

---

**Note**: This is an experimental project designed for learning and demonstration purposes. For production use, consider adding comprehensive error handling, logging, monitoring, and security reviews.