/// <summary>
/// Game.cs - Handle the low-level game logic.
/// </summary>
namespace Snake.Modules;

/// <summary>
/// Class that manages the low-level game logic.
/// </summary>
public class Game
{
    /// <summary>
    /// Construct a new Game object.
    /// </summary>
    public Game(int width, int height)
    {
        this._width = width;
        this._height = height;
        this._player = new Core.Player(new Core.Position(width / 2, height / 2));
        this._rand = new System.Random();
        this.GenerateFood();
    }

    /// <summary>
    /// Reset the game state.
    /// </summary>
    public void Reset()
    {
        this._player.Body.Clear();
        this._player.Body.Add(new Core.Position(this._width / 2, this._height / 2));
        this._player.Direction = Core.Position.Up;
        this._gameOver = false;
        this._score = 0;
        this.GenerateFood();
    }

    /// <summary>
    /// Render the game objects.
    /// </summary>
    public void Draw(bool gameOver = false)
    {
        // Clear the console
        Spectre.Console.AnsiConsole.Clear();

        // Create a canvas for the game area
        Spectre.Console.Canvas canvas = new(this._width + 2, this._height + 2);

        // Draw components
        this.DrawBorder(canvas, gameOver);
        this.DrawPlayer(canvas);
        this.DrawFood(canvas);

        // Render the canvas
        Spectre.Console.AnsiConsole.Cursor.SetPosition(0, 1); // Game area starts below the top border
        Spectre.Console.AnsiConsole.Write(canvas);

        // Display the score or the restart prompt below the game area
        this.DisplayScoreOrPrompt(gameOver);
    }

    /// <summary>
    /// Handle user input.
    /// </summary>
    public void HandleInput()
    {
        while (System.Console.KeyAvailable)
        {
            System.ConsoleKey key = System.Console.ReadKey(true).Key;
            Core.Position currentDirection = this._player.Direction;
            Core.Position newDirection = currentDirection;

            switch (key)
            {
                case System.ConsoleKey.UpArrow when currentDirection != Core.Position.Down:
                    newDirection = Core.Position.Up;
                    break;
                case System.ConsoleKey.DownArrow when currentDirection != Core.Position.Up:
                    newDirection = Core.Position.Down;
                    break;
                case System.ConsoleKey.LeftArrow when currentDirection != Core.Position.Right:
                    newDirection = Core.Position.Left;
                    break;
                case System.ConsoleKey.RightArrow when currentDirection != Core.Position.Left:
                    newDirection = Core.Position.Right;
                    break;
            }

            this._player.Direction = newDirection;
        }
    }

    /// <summary>
    /// Update the game state.
    /// </summary>
    public void Update()
    {
        Core.Position nextHead = this._player.Head + this._player.Direction;

        bool collision =
            nextHead.X < 0
            || nextHead.X >= this._width
            || nextHead.Y < 0
            || nextHead.Y >= this._height
            || this.CheckSelfCollision(nextHead);

        if (collision)
        {
            this._gameOver = true;
            return;
        }

        if (nextHead == this._food)
        {
            this._score++;
            this._player.Move(grow: true);
            this.GenerateFood();
        }
        else
        {
            this._player.Move();
        }
    }

    /// <summary>
    /// Display the game over screen.
    /// </summary>
    public void DisplayGameOver()
    {
        this.Draw(gameOver: true);
    }

    /// <summary>
    /// Wait for a short period to control game speed.
    /// </summary>
    public void Wait()
    {
        System.Threading.Thread.Sleep(Game.GameSpeed);
    }

    /// <summary>
    /// Clear the console screen.
    /// </summary>
    public void ClearConsole()
    {
        System.Console.Clear();
    }

    /// <summary>
    /// Get a value indicating whether the game is over.
    /// </summary>
    public bool IsGameOver => this._gameOver;

    /// <summary>
    /// Get the current score.
    /// </summary>
    public int Score => this._score;

    /// <summary>
    /// Draw the game border.
    /// </summary>
    private void DrawBorder(Spectre.Console.Canvas canvas, bool gameOver)
    {
        Spectre.Console.Color borderColor = gameOver
            ? Spectre.Console.Color.Red
            : Spectre.Console.Color.Grey;

        for (int x = 0; x < this._width + 2; x++)
        {
            canvas.SetPixel(x, 0, borderColor);
            canvas.SetPixel(x, this._height + 1, borderColor);
        }
        for (int y = 0; y < this._height + 2; y++)
        {
            canvas.SetPixel(0, y, borderColor);
            canvas.SetPixel(this._width + 1, y, borderColor);
        }
    }

    /// <summary>
    /// Draw the player on the canvas.
    /// </summary>
    private void DrawPlayer(Spectre.Console.Canvas canvas)
    {
        Spectre.Console.Color bodyColor = Spectre.Console.Color.Green;
        for (int i = 1; i < this._player.Body.Count; i++)
        {
            Core.Position segment = this._player.Body[i];
            canvas.SetPixel(segment.X + 1, segment.Y + 1, bodyColor);
        }

        Spectre.Console.Color headColor = Spectre.Console.Color.Green;
        Core.Position head = this._player.Head;
        canvas.SetPixel(head.X + 1, head.Y + 1, headColor);
    }

    /// <summary>
    /// Draw the food on the canvas.
    /// </summary>
    private void DrawFood(Spectre.Console.Canvas canvas)
    {
        Spectre.Console.Color foodColor = Spectre.Console.Color.Yellow;
        canvas.SetPixel(this._food.X + 1, this._food.Y + 1, foodColor);
    }

    /// <summary>
    /// Display the score or restart prompt.
    /// </summary>
    private void DisplayScoreOrPrompt(bool gameOver)
    {
        Spectre.Console.AnsiConsole.Cursor.SetPosition(0, this._height + 3); // Below the game walls

        string message = gameOver
            ? $"[bold grey]Score: {this._score}[/] | [bold yellow]Any key to restart, Escape to exit[/]"
            : $"[bold grey]Score: {this._score}[/] | Arrow keys to move";

        Spectre.Console.Panel panel =
            new(message)
            {
                Border = Spectre.Console.BoxBorder.None,
                Padding = new Spectre.Console.Padding(0),
                Expand = true,
            };

        Spectre.Console.AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Check if the player collides with itself.
    /// </summary>
    private bool CheckSelfCollision(Core.Position position)
    {
        for (int i = 1; i < this._player.Body.Count; i++)
        {
            if (this._player.Body[i] == position)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Generate a new food item at a random location.
    /// </summary>
    private void GenerateFood()
    {
        do
        {
            this._food = new Core.Position(
                this._rand.Next(this._width),
                this._rand.Next(this._height)
            );
        } while (this._player.Contains(this._food));
    }

    private const int GameSpeed = 100; // Game speed in milliseconds
    private readonly int _width;
    private readonly int _height;
    private readonly Core.Player _player;
    private readonly System.Random _rand;
    private Core.Position _food;
    private bool _gameOver;
    private int _score;
}
