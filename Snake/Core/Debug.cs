namespace Snake.Core;

public static class Debug
{
    public static void PrintBuildConfiguration()
    {
#if DEBUG
        System.Console.WriteLine("Build type: Debug.");
#else
        System.Console.WriteLine("Build type: Release.");
#endif
    }
}
