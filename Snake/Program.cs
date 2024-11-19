/// <summary>
/// Program.cs - Main entry point.
/// </summary>
namespace Snake;

static class Program
{
    static void Main()
    {
        App app = new(30, 20);
        app.Run();
    }
}
