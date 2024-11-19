/// <summary>
/// Objects.cs - Core game objects.
/// </summary>
namespace Snake.Core;

/// <summary>
/// Struct that represents a position in 2D space.
/// </summary>
public readonly struct Position : System.IEquatable<Position>
{
    /// <summary>
    /// Construct a new Position object.
    /// </summary>
    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// Add another position to this position.
    /// </summary>
    public Position Add(Position other) => new(this.X + other.X, this.Y + other.Y);

    /// <summary>
    /// Override the addition operator.
    /// </summary>
    public static Position operator +(Position left, Position right) =>
        new(left.X + right.X, left.Y + right.Y);

    // Equality members
    public bool Equals(Position other) => this.X == other.X && this.Y == other.Y;

    public override bool Equals(object? obj) => obj is Position other && this.Equals(other);

    public override int GetHashCode() => System.HashCode.Combine(this.X, this.Y);

    public static bool operator ==(Position left, Position right) => left.Equals(right);

    public static bool operator !=(Position left, Position right) => !left.Equals(right);

    // Directional constants
    public static readonly Position Up = new(0, -1);
    public static readonly Position Down = new(0, 1);
    public static readonly Position Left = new(-1, 0);
    public static readonly Position Right = new(1, 0);

    public int X { get; }
    public int Y { get; }
}

/// <summary>
/// Class that represents the player.
/// </summary>
public class Player
{
    /// <summary>
    /// Construct a new Player object.
    /// </summary>
    public Player(Position initialPosition)
    {
        this.Body = [initialPosition];
        this.Direction = Position.Up;
    }

    /// <summary>
    /// Move the player in its current direction.
    /// </summary>
    public void Move(bool grow = false)
    {
        Position newHead = this.Head + this.Direction;
        this.Body.Insert(0, newHead);
        if (!grow)
        {
            this.Body.RemoveAt(this.Body.Count - 1);
        }
    }

    /// <summary>
    /// Check if player occupies a specific position.
    /// </summary>
    public bool Contains(Position position) => this.Body.Contains(position);

    /// <summary>
    /// Get or set the current direction of the player.
    /// </summary>
    public Position Direction { get; set; }

    /// <summary>
    /// Get the head position of the player.
    /// </summary>
    public Position Head => this.Body[0];
    public System.Collections.Generic.List<Position> Body { get; }
}
