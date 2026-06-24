using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// This script handles visual feedback animations for wins and losses.
// 1) Win: flashes the 3 middle symbols with a gold glow
// 2) Jackpot: rapidly flashes all 9 symbols
// 3) Loss: briefly shakes the reel area
public class WinAnimator : MonoBehaviour
{
    [Header("Middle Row Symbol Images (Winning Line)")]
    public Image reel1Mid;
    public Image reel2Mid;
    public Image reel3Mid;

    [Header("All 9 Symbol Images ")]
    public Image[] allSymbols;

    [Header("Win Line Highlight")]
    public Image winLine; // slot_machine_Middle_box image

    [Header("Animation Settings")]
    public float flashInterval = 0.12f;   // how fast symbols flash
    public int flashCount = 6;            // how many times they flash on win
    public Color winFlashColor = new Color(1f, 0.9f, 0f); // gold color
    public Color normalColor = Color.white;

    // function will be called after spin completes.
    public IEnumerator PlayWinAnimation(int winAmount)
    {
        if (winAmount >= 500)
        {
            yield return StartCoroutine(JackpotFlash()); // JACKPOT — flash all 9 symbols rapidly
        }
        else if (winAmount > 0)
        {
            yield return StartCoroutine(WinFlash()); // NORMAL WIN — flash middle row symbols
        }
        else
        {
            yield return StartCoroutine(LossShake()); // LOSS — shake effect
        }
    }

    // Flashes the 3 middle symbols gold for a normal win.
    private IEnumerator WinFlash()
    {
        // Show win line highlight
        if (winLine != null)
        {
            Color c = winLine.color;
            c.a = 0.8f;
            winLine.color = c;
        }

        for (int i = 0; i < flashCount; i++)
        {
            SetMiddleRowColor(winFlashColor); //flash gold
            yield return new WaitForSeconds(flashInterval);

            SetMiddleRowColor(normalColor); //Back to normal
            yield return new WaitForSeconds(flashInterval);
        }

        if (winLine != null)
        {
            yield return StartCoroutine(FadeWinLine());
        }
    }

    // During jackpot all 9 symbols will be rapidly flashed
    private IEnumerator JackpotFlash()
    {
        if (winLine != null)
        {
            Color c = winLine.color;
            c.a = 1f;
            winLine.color = c;
        }

        for (int i = 0; i < flashCount * 2; i++)
        {
            SetAllSymbolsColor(winFlashColor);
            yield return new WaitForSeconds(flashInterval * 0.6f);

            SetAllSymbolsColor(normalColor);
            yield return new WaitForSeconds(flashInterval * 0.6f);
        }

        if (winLine != null)
        {
            yield return StartCoroutine(FadeWinLine());
        }
    }

    // Shakes the middle symbols slightly on a loss.
    private IEnumerator LossShake()
    {
        Vector3 originalPos1 = reel1Mid.rectTransform.anchoredPosition;
        Vector3 originalPos2 = reel2Mid.rectTransform.anchoredPosition;
        Vector3 originalPos3 = reel3Mid.rectTransform.anchoredPosition;

        float shakeDuration = 0.4f;
        float shakeMagnitude = 8f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;

            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude * 0.5f, shakeMagnitude * 0.5f);

            reel1Mid.rectTransform.anchoredPosition = originalPos1 + new Vector3(offsetX, offsetY, 0);
            reel2Mid.rectTransform.anchoredPosition = originalPos2 + new Vector3(-offsetX, offsetY, 0);
            reel3Mid.rectTransform.anchoredPosition = originalPos3 + new Vector3(offsetX, -offsetY, 0);

            yield return null;
        }

        reel1Mid.rectTransform.anchoredPosition = originalPos1;
        reel2Mid.rectTransform.anchoredPosition = originalPos2;
        reel3Mid.rectTransform.anchoredPosition = originalPos3;
    }

    // Smoothly fades out the win line highlight.
    private IEnumerator FadeWinLine()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Color c = winLine.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(0.8f, 0.2f, elapsed / duration);
            winLine.color = c;
            yield return null;
        }

        c.a = 1f;
        winLine.color = c;
    }

    private void SetMiddleRowColor(Color color)
    {
        reel1Mid.color = color;
        reel2Mid.color = color;
        reel3Mid.color = color;
    }

    private void SetAllSymbolsColor(Color color)
    {
        foreach (Image img in allSymbols)
            img.color = color;
    }
}