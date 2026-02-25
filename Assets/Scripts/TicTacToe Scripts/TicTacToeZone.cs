using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class TicTacToeZone : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject X;
    [SerializeField] private GameObject O;
    [SerializeField] private TextMeshProUGUI playerTurnText;
    [HideInInspector] public bool IsXActive => X.activeSelf;
    [HideInInspector] public bool IsOActive => O.activeSelf;
    private static int clickCount = 1; // To track the number of clicks for toggling
    public int GetClickCount()
    {
        return clickCount;
    }

    private void Awake()
    {
        X.SetActive(false);
        O.SetActive(false);

        clickCount = 1; // Reset click count when the game starts

        GameManager.SetGamePaused(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.GameplayPaused)
        {
            return;
        }
        OnZoneClicked();

    }

    private void OnZoneClicked()
    {
        if (IsXActive || IsOActive)
        {
            return;
        }
        else if (clickCount % 2 == 0)
        {
            X.SetActive(true);
            UpdatePlayerTurnText();
        }
        else
        {
            O.SetActive(true);
            UpdatePlayerTurnText();
        }
    }

    private void UpdatePlayerTurnText()
    {
        if (playerTurnText != null)
        {
            if (clickCount % 2 == 0)
            {
                playerTurnText.text = "Turn: \nPlayer 1";
            }
            else
            {
                playerTurnText.text = "Turn: \nPlayer 2";
            }
            clickCount++;
        }
    }
}
