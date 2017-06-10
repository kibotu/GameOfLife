using UnityEngine;

/// <summary>
///     https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
///     Rules
///     The universe of the Game of Life is an infinite two-dimensional orthogonal grid of square cells,
///     each of which is in one of two possible states, alive or dead, or "populated" or "unpopulated".
///     Every cell interacts with its eight neighbours, which are the cells that are horizontally, vertically,
///     or diagonally adjacent.At each step in time, the following transitions occur:
/// 
///     1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
///     2. Any live cell with two or three live neighbours lives on to the next generation.
///     3. Any live cell with more than three live neighbours dies, as if by overpopulation.
///     4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction
/// 
///     The initial pattern constitutes the seed of the system.The first generation is created by
///     applying the above rules simultaneously to every cell in the seed—births and deaths occur simultaneously,
///     and the discrete moment at which this happens is sometimes called a tick (in other words, each generation is a
///     pure function of the preceding one). The rules continue to be applied repeatedly to create further generations.
/// </summary>
public class Game : MonoBehaviour
{
    [SerializeField]
    private int rows;

    [SerializeField]
    private int columns;

    private Board _board;

    public bool IsRunning = false;

    [SerializeField]
    private Cell prefab;

    private float elapsedTime;

    public float turnDuration = 0.3f;

    private void Start()
    {
        _board = GetComponent<Board>();
        _board.CreateBoard(prefab, rows, columns);
    }

    private void Update()
    {
        if (!IsRunning) return;

        if (elapsedTime > turnDuration)
        {
            elapsedTime = 0;
            _board.NextTurn();
        }

        elapsedTime += Time.deltaTime;
    }

    public void StartGame()
    {
        elapsedTime = 0;
        IsRunning = true;
    }

    public void StopGame()
    {
        elapsedTime = 0;
        IsRunning = false;
    }
}