namespace AoC.Common;

public record MapCoordinate(int X, int Y)
{
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    public bool IsAdjacentTo(MapCoordinate coordinate)
    {
        if (this.X == coordinate.X && this.Y == coordinate.Y - 1) return true; 
        if (this.X == coordinate.X && this.Y == coordinate.Y + 1) return true;
        if (this.X == coordinate.X - 1 && this.Y == coordinate.Y) return true;
        if (this.X == coordinate.X + 1 && this.Y == coordinate.Y) return true;

        return false;
    }
}
