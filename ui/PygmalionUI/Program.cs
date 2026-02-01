using PygmalionUI;

// Parse command line arguments
for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "-h" || args[i] == "--help")
    {
        Console.WriteLine("Pygmalion UI");
        Console.WriteLine("A terminal-based UI for managing contacts and files");
        Console.WriteLine();
        Console.WriteLine("Usage: PygmalionUI [options]");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  -h, --help    Show this help message");
        return;
    }
}

var app = new App();
app.Run();
