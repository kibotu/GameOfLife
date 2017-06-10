using UnityEngine;

public class Board : MonoBehaviour
{
    private Cell[,] _cells;

    public void CreateBoard(Cell prefab, int rows, int colums)
    {
        _cells = new Cell[rows, colums];

        for (var y = 0; y < _cells.GetLength(1); ++y)
            for (var x = 0; x < _cells.GetLength(0); ++x)
            {
                var cell = Instantiate(prefab, new Vector3(x - rows/2, y - colums/2, 0), Quaternion.identity);
                cell.x = x;
                cell.y = y;
                _cells[x, y] = cell;
            }
    }

    public void NextTurn()
    {
        // figure out next turn state
        for (var y = 0; y < _cells.GetLength(1); ++y)
            for (var x = 0; x < _cells.GetLength(0); ++x)
                _cells[x, y].neighbours = GetAmountNeighbours(_cells[x, y]);

        // apply state
        for (var y = 0; y < _cells.GetLength(1); ++y)
            for (var x = 0; x < _cells.GetLength(0); ++x)
                _cells[x, y].Live();
    }

    /// <summary>
    ///     [x-1, y-1]  [x, y-1]  [x+1, y-1]
    ///     [x-1, y]    [x, y]    [x+1, y]
    ///     [x-1, y+1]  [x, y+1]  [x+1, y+1]
    /// </summary>
    private int GetAmountNeighbours(Cell cell)
    {
        var rows = _cells.GetLength(0);
        var columns = _cells.GetLength(1);

        var amount = 0;

        var x = cell.x;
        var y = cell.y;

        // Debug.Log("[" + x + ", " + y + "] rows="+rows + ", columns="+columns);

        // top left
        if (x - 1 >= 0 && y - 1 >= 0 && _cells[x - 1, y - 1].IsAlive)
            ++amount;
        // top center
        if (y - 1 >= 0 && _cells[x, y - 1].IsAlive)
            ++amount;
        // top right
        if (x + 1 < rows && y - 1 >= 0 && _cells[x + 1, y - 1].IsAlive)
            ++amount;


        // center left
        if (x - 1 >= 0 && _cells[x - 1, y].IsAlive)
            ++amount;

        // center center, identity

        // center right
        if (x + 1 < rows && _cells[x + 1, y].IsAlive)
            ++amount;


        // bottom left
        if (x - 1 >= 0 && y + 1 < columns && _cells[x - 1, y + 1].IsAlive)
            ++amount;
        // bottom center
        if (y + 1 < columns && _cells[x, y + 1].IsAlive)
            ++amount;
        // bottom right
        if (x + 1 < rows && y + 1 < columns && _cells[x + 1, y + 1].IsAlive)
            ++amount;

        return amount;
    }
}