using System.Collections;
using UnityEngine;

// This script is Main game controller, Coordinates reels, lever, sounds, animations, UI, and betting.
public class SlotManager : MonoBehaviour
{
    [Header("Reel Controllers")]
    public ReelController reel1;
    public ReelController reel2;
    public ReelController reel3;

    [Header("Lever")]
    public LeverAnimator leverAnimator;

    [Header("Managers")]
    public WinAnimator winAnimator;
    public SoundManager soundManager;
    public UIManager uiManager;
    public BetManager betManager;

    [Header("Game Settings")]
    public int startingCredits = 100; //Initial credits set to 100
    private int currentCredits;
    private bool isSpinning = false;

    void Start()
    {
        currentCredits = startingCredits;
        uiManager.UpdateCredits(currentCredits);
        uiManager.HidePopup();
    }

    public void OnSpinPressed()
    {
        if (isSpinning) return;

        int currentBet = betManager.GetCurrentBet();

        if (currentCredits < currentBet)
        {
            uiManager.ShowWinPopup(0, "No Credits!");
            return;
        }

        currentCredits -= currentBet;
        uiManager.UpdateCredits(currentCredits);
        StartCoroutine(SpinSequence());
    }

    // Called by UIManager when player clicks YES on Play Again popup.
    public void RefillCredits(int amount)
    {
        currentCredits += amount;
        uiManager.UpdateCredits(currentCredits);
    }

    private IEnumerator SpinSequence()
    {
        isSpinning = true;
        uiManager.SetSpinButtonInteractable(false);
        betManager.SetBetButtonsInteractable(false);
        uiManager.HidePopup();

        soundManager.PlayLever();
        yield return StartCoroutine(leverAnimator.PlayLeverPull());

        yield return new WaitForSeconds(0.1f);

        soundManager.PlaySpinLoop();
        StartCoroutine(reel1.Spin(1.8f));
        StartCoroutine(reel2.Spin(2.5f));
        StartCoroutine(reel3.Spin(3.2f));

        yield return new WaitForSeconds(1.9f);
        soundManager.PlayReelStop();

        yield return new WaitForSeconds(0.7f);
        soundManager.PlayReelStop();

        yield return new WaitForSeconds(0.7f);
        soundManager.PlayReelStop();
        soundManager.StopSpinLoop();

        yield return new WaitForSeconds(0.2f);

        int result1 = reel1.GetResult();
        int result2 = reel2.GetResult();
        int result3 = reel3.GetResult();

        int baseWin = WinChecker.CheckWin(result1, result2, result3);

        int betMultiplier = betManager.GetCurrentBet() / 10;
        int totalWin = baseWin * betMultiplier;

        if (totalWin > 0)
        {
            currentCredits += totalWin;
            uiManager.UpdateCredits(currentCredits);
        }

        betManager.SetLastWin(totalWin);

        if (totalWin >= 500)
            soundManager.PlayJackpot();
        else if (totalWin > 0)
            soundManager.PlayWin();
        else
            soundManager.PlayLoss();

        yield return StartCoroutine(winAnimator.PlayWinAnimation(totalWin));

        string symbolName = WinChecker.GetSymbolName(result1);
        uiManager.ShowWinPopup(totalWin, symbolName);

        isSpinning = false;
        betManager.SetBetButtonsInteractable(true);
    }
}