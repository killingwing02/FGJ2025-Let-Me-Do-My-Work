using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public int totalPieces;
    public int snappedCount;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterPiece() => totalPieces++;
    public void PieceSnapped()
    {
        snappedCount++;
        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        if (snappedCount >= totalPieces)
        {
            WinLose.Instance.Win();
        }
    }
}

