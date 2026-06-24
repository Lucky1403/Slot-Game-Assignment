using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This script manages all UI: credits display, popups, and spin button state.
public class UIManager : MonoBehaviour
{
    [Header("Credits Display")]
    public TextMeshProUGUI creditsText; //Total credits text

    [Header("Win Popup (shown on wins)")]
    public GameObject winPopup;
    public TextMeshProUGUI winPopupText;

    [Header("Normal Popup (shown on loss)")]
    public GameObject normalPopup;
    public TextMeshProUGUI normalPopupText;

    [Header("Play Again Popup (shown when out of credits)")]
    public GameObject playAgainPopup;

    [Header("Spin Button")]
    public Button spinButton;

    [Header("Game Settings")]
    public int refillCredits = 100; // credits given when player clicks YES
    private SlotManager slotManager;

    void Start()
    {
        slotManager = GetComponent<SlotManager>();
    }

    // Updates the credits display
    public void UpdateCredits(int amount)
    {
        creditsText.text = "Credits: " + amount;
    }

    // Shows the correct popup based on win amount.
    public void ShowWinPopup(int amount, string symbolName)
    {
        HidePopup();

        SetSpinButtonInteractable(false);

        if (amount >= 500)
        {
            winPopup.SetActive(true);
            winPopupText.text = "JACKPOT!\n" + symbolName + " x3\n+" + amount + " Credits!";
        }
        else if (amount > 0)
        {
            winPopup.SetActive(true);
            winPopupText.text = "YOU WIN!\n+" + amount + " Credits!";
        }
        else if (symbolName == "No Credits!")
        {
            if (playAgainPopup != null)
                playAgainPopup.SetActive(true);
            else
            {
                normalPopup.SetActive(true);
                normalPopupText.text = "Out of Credits!\nGame Over!";
            }
        }
        else
        {
            normalPopup.SetActive(true);
            normalPopupText.text = "No Win!\nTry Again!";
        }
    }

    // Called when player clicks X on ANY popup.
    public void OnClosePopupClicked()
    {
        HidePopup();
        SetSpinButtonInteractable(true); // spin is re-enabled
    }

    // Called when player clicks YES on Play Again popup.
    public void OnPlayAgainYes()
    {
        HidePopup();
        slotManager.RefillCredits(refillCredits);
        SetSpinButtonInteractable(true);
    }

    // Called when player clicks NO on Play Again popup.
    public void OnPlayAgainNo()
    {
        HidePopup();
        normalPopup.SetActive(true);
        normalPopupText.text = "Thanks for playing!\nRefresh to restart.";
    }

    // All popups will be hidden
    public void HidePopup()
    {
        if (winPopup != null) winPopup.SetActive(false);
        if (normalPopup != null) normalPopup.SetActive(false);
        if (playAgainPopup != null) playAgainPopup.SetActive(false);
    }

    // Enable or disable the spin button
    public void SetSpinButtonInteractable(bool interactable)
    {
        if (spinButton != null)
            spinButton.interactable = interactable;
    }
}