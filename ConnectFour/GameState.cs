namespace ConnectFour;

/// <summary>
/// Holds the state of a single game of Connect Four and all of the rules
/// for playing a piece and detecting a win.
///
/// The board is a flat array of 42 cells, laid out left-to-right, top-to-bottom:
///
///    0  1  2  3  4  5  6
///    7  8  9 10 11 12 13
///   14 15 16 17 18 19 20
///   21 22 23 24 25 26 27
///   28 29 30 31 32 33 34
///   35 36 37 38 39 40 41
///
/// A value of 0 means empty, 1 means player 1, and 2 means player 2.
/// </summary>
public class GameState
{
    public enum WinState
    {
        No_Winner = 0,
        Player1_Wins = 1,
        Player2_Wins = 2,
        Tie = 3
    }

    private const int Columns = 7;
    private const int Rows = 6;
    private const int Cells = Columns * Rows;

    /// <summary>
    /// Every group of four cells that wins the game, precomputed once.
    /// </summary>
    private static readonly int[][] WinningPlaces = BuildWinningPlaces();

    public int[] TheBoard { get; private set; } = new int[Cells];

    /// <summary>
    /// The number of pieces already played, which doubles as the index of the
    /// next piece to be placed.
    /// </summary>
    public int CurrentTurn => TheBoard.Count(cell => cell != 0);

    /// <summary>
    /// The player (1 or 2) whose turn it is.
    /// </summary>
    public int PlayerTurn => CurrentTurn % 2 + 1;

    public void ResetBoard() => TheBoard = new int[Cells];

    /// <summary>
    /// Drops a piece for the current player into <paramref name="column"/>.
    /// </summary>
    /// <returns>The row the piece landed in, 1 (top) through 6 (bottom).</returns>
    public byte PlayPiece(byte column)
    {
        if (column >= Columns)
        {
            throw new ArgumentException("Invalid column");
        }

        if (CheckForWin() != WinState.No_Winner)
        {
            throw new ArgumentException("Game is over");
        }

        if (TheBoard[column] != 0)
        {
            throw new ArgumentException("Column is full");
        }

        // Walk down the column until the cell below is occupied or off the board.
        var landingSpot = column;
        while (landingSpot + Columns < Cells && TheBoard[landingSpot + Columns] == 0)
        {
            landingSpot += Columns;
        }

        TheBoard[landingSpot] = PlayerTurn;

        return Convert.ToByte(landingSpot / Columns + 1);
    }

    public WinState CheckForWin()
    {
        foreach (var place in WinningPlaces)
        {
            var first = TheBoard[place[0]];
            if (first == 0)
            {
                continue;
            }

            if (TheBoard[place[1]] == first && TheBoard[place[2]] == first && TheBoard[place[3]] == first)
            {
                return first == 1 ? WinState.Player1_Wins : WinState.Player2_Wins;
            }
        }

        return TheBoard.Any(cell => cell == 0) ? WinState.No_Winner : WinState.Tie;
    }

    private static int[][] BuildWinningPlaces()
    {
        var places = new List<int[]>();

        for (var row = 0; row < Rows; row++)
        {
            for (var col = 0; col < Columns; col++)
            {
                var start = row * Columns + col;

                // Horizontal
                if (col + 3 < Columns)
                {
                    places.Add(new[] { start, start + 1, start + 2, start + 3 });
                }

                // Vertical
                if (row + 3 < Rows)
                {
                    places.Add(new[] { start, start + 7, start + 14, start + 21 });
                }

                // Diagonal down-right
                if (col + 3 < Columns && row + 3 < Rows)
                {
                    places.Add(new[] { start, start + 8, start + 16, start + 24 });
                }

                // Diagonal down-left
                if (col - 3 >= 0 && row + 3 < Rows)
                {
                    places.Add(new[] { start, start + 6, start + 12, start + 18 });
                }
            }
        }

        return places.ToArray();
    }
}
