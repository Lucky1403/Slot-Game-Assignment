using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This script is managing the betting system and controls the bet amound, updates UI
public class BetManager : MonoBehaviour
{
    [Header("Bet Settings")]
    public int minBet = 10;
    public int maxBet = 50;
    public int betStep = 10;

    [Header("UI References")]
    public TextMeshProUGUI betText;        // shows "Bet: 10"
    public TextMeshProUGUI lastWinText;    // shows "Last Win" amount
    public Button betDownButton;           // left arrow
    public Button betUpButton;             // right arrow

    private int currentBet;

    void Start()
    {
        currentBet = minBet;
        UpdateBetUI();
        SetLastWin(0);
    }

    // Returns current bet — called by SlotManager before each spin.
    public int GetCurrentBet()
    {
        return currentBet;
    }

    // Left arrow — decreases bet by one step.
    public void OnBetDown()
    {
        if (currentBet <= minBet) return;
        currentBet -= betStep;
        UpdateBetUI();
    }

    // Right arrow — increases bet by one step.
    public void OnBetUp()
    {
        if (currentBet >= maxBet) return;
        currentBet += betStep;
        UpdateBetUI();
    }

    // Called by SlotManager after each spin with the win amount.
    public void SetLastWin(int amount)
    {
        if (lastWinText == null) return;
        lastWinText.text = amount > 0 ? "Last Win: +" + amount : "Last Win: --";
    }

    // Lock/unlock bet buttons during spin.
    public void SetBetButtonsInteractable(bool interactable)
    {
        // When locked during spin, disable all
        if (!interactable)
        {
            if (betDownButton != null) betDownButton.interactable = false;
            if (betUpButton != null) betUpButton.interactable = false;
        }
        else
        {
            UpdateBetUI();
        }
    }

    // Updates bet text and button interactable states.
    private void UpdateBetUI()
    {
        if (betText != null)
            betText.text = "Bet: " + currentBet;

        // At minimum bet → down button shows disabled sprite
        if (betDownButton != null)
            betDownButton.interactable = currentBet > minBet;

        // At maximum bet → up and max buttons show disabled sprite
        if (betUpButton != null)
            betUpButton.interactable = currentBet < maxBet;
    }
}