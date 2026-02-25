using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class TicTacToePlayerController : MonoBehaviour
{
    [SerializeField] private GameObject[] squares;
    [SerializeField] private GameObject player1Winner;
    [SerializeField] private GameObject player2Winner;
    private int[][] winConditions = new int[][]
    {
        new int[] {0, 1, 2}, // Row 1
        new int[] {3, 4, 5}, // Row 2
        new int[] {6, 7, 8}, // Row 3
        new int[] {0, 3, 6}, // Column 1
        new int[] {1, 4, 7}, // Column 2
        new int[] {2, 5, 8}, // Column 3
        new int[] {0, 4, 8}, // Diagonal \
        new int[] {2, 4, 6}  // Diagonal /
    };

    private bool winnerDeclared = false; // To prevent multiple win declarations

    private void Awake()
    {
        player1Winner.SetActive(false);
        player2Winner.SetActive(false);
    }

    void Update()
    {
        if (GameManager.GameplayPaused)
        {
            return;
        }
        if (squares == null || squares.Length == 0)
        {
            Debug.LogError("Squares array is not assigned or empty!");
            return;
        }
        if (squares[0].GetComponent<TicTacToeZone>().GetClickCount() > 5)
        {
            CheckWinCondition();
        }
    }

    public void CheckWinCondition()
    {
        // Implement win condition checking logic here
        foreach (var condition in winConditions)
        {
            if (squares[condition[0]].GetComponent<TicTacToeZone>().IsXActive &&
                squares[condition[1]].GetComponent<TicTacToeZone>().IsXActive &&
                squares[condition[2]].GetComponent<TicTacToeZone>().IsXActive)
            {
                if (!winnerDeclared)
                {
                    Debug.Log("Player 2 wins!");
                    player2Winner.SetActive(true);
                    winnerDeclared = true;
                    GameManager.SetGamePaused(true);
                }
                return;
            }

            if (squares[condition[0]].GetComponent<TicTacToeZone>().IsOActive &&
                squares[condition[1]].GetComponent<TicTacToeZone>().IsOActive &&
                squares[condition[2]].GetComponent<TicTacToeZone>().IsOActive)
            {
                if (!winnerDeclared)
                {
                    Debug.Log("Player 1 wins!");
                    player1Winner.SetActive(true);
                    winnerDeclared = true;
                    GameManager.SetGamePaused(true);
                }
                return;
            }
        }
    }

}

