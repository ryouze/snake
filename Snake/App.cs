/// <summary>
/// App.cs - Main application logic.
/// </summary>
namespace Snake;

/// <summary>
/// Class that handles the main application logic.
/// </summary>
public class App
{
    /// <summary>
    /// Construct a new App object.
    /// </summary>
    public App(int width, int height)
    {
        this._engine = new Modules.Game(width, height);
    }

    /// <summary>
    /// Run the main application loop.
    /// </summary>
    public void Run()
    {
        System.Console.CursorVisible = false;

        do
        {
            this._engine.Reset();

            while (!this._engine.IsGameOver)
            {
                this._engine.Draw();
                this._engine.HandleInput();
                this._engine.Update();
                this._engine.Wait();
            }

            this._engine.DisplayGameOver();
        } while (System.Console.ReadKey(true).Key != System.ConsoleKey.Escape);

        this._engine.ClearConsole();
        System.Console.WriteLine($"Your final score was {this._engine.Score}.");
    }

    private readonly Modules.Game _engine;
}
