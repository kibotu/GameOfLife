using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[Serializable]
public class Cell : MonoBehaviour
{
    public enum CellState
    {
        Empty,
        Alive
    }

    public Material alive;
    public Material empty;

    public int x;
    public int y;

    public CellState State = CellState.Empty;
    public int neighbours;

    private void OnMouseDown()
    {
        ChangeToNextState();
    }

    public void ChangeToNextState()
    {
        if (State == CellState.Empty)
            ChangeStateTo(CellState.Alive);
        else if (State == CellState.Alive)
            ChangeStateTo(CellState.Empty);
    }

    private void ChangeStateTo(CellState state)
    {
        State = state;
        GetComponent<Renderer>().material = GetMaterial(state);
    }

    private Material GetMaterial(CellState state)
    {
        switch (state)
        {
            case CellState.Alive:
                return alive;
            case CellState.Empty:
            default:
                return empty;
        }
    }

    public bool IsAlive { get { return State == CellState.Alive; } }

    public bool IsDead { get { return State == CellState.Empty; } }

    public void Die()
    {
        ChangeStateTo(CellState.Empty);
    }

    public void Birth()
    {
        ChangeStateTo(CellState.Alive);
    }

    /// <summary>
    ///     1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
    ///     2. Any live cell with two or three live neighbours lives on to the next generation.
    ///     3. Any live cell with more than three live neighbours dies, as if by overpopulation.
    ///     4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction
    /// </summary>
    public void Live()
    {
        // underpopulation
        if (IsAlive && neighbours < 2)
            Die();

        // next generation 
        else if (IsAlive && (neighbours == 2 || neighbours == 3))
            Birth();

        // overpopulation
        else if (IsAlive && neighbours > 3)
           Die();

        // reproduction
        else if (IsDead && neighbours == 3)
            Birth();
    }
}