using PersonTablesCLI;

try
{
    DataManipulation.PerformAction(Environment.GetCommandLineArgs());
}
catch (ArgumentException exc)
{
    Console.WriteLine($"Error:\n{exc.Message}");
}
catch (Exception exc)
{
    Console.WriteLine($"Unknown error:\n{exc.Message}");
}
