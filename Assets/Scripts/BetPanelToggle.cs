using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// This script is Toggling the Bet Panel open and closed with a smooth slide animation.
public class BetPanelToggle : MonoBehaviour
{
    [Header("References")]
    public GameObject betPanel; //Bet Panel
    public Button toggleButton; // Button which will toggle the bet panel

    [Header("Animation")]
    public float animDuration = 0.3f;
    public float hiddenYOffset = -200f;

    // Tracking open/closed state
    private bool isPanelOpen = false;
    private bool isAnimating = false;

    // Cached positions
    private Vector2 openPosition;
    private Vector2 closedPosition;

    private RectTransform betPanelRect;

    void Start()
    {
        betPanelRect = betPanel.GetComponent<RectTransform>();

        openPosition = betPanelRect.anchoredPosition;

        closedPosition = new Vector2(openPosition.x, openPosition.y + hiddenYOffset);

        betPanel.SetActive(false); //Initially set as hidden
        isPanelOpen = false;
    }

    // Function will be called by BetToggleButton OnClick.
    public void ToggleBetPanel()
    {
        if (isAnimating) return;

        if (isPanelOpen)
            StartCoroutine(SlidePanel(openPosition, closedPosition, false));
        else
            StartCoroutine(SlidePanel(closedPosition, openPosition, true));
    }

    // Smoothly slides the panel between two positions.
    private IEnumerator SlidePanel(Vector2 from, Vector2 to, bool showPanel)
    {
        isAnimating = true;

        if (showPanel)
        {
            betPanel.SetActive(true);
            betPanelRect.anchoredPosition = from;
        }

        float elapsed = 0f;
        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animDuration;

            float smoothT = t * t * (3f - 2f * t);
            betPanelRect.anchoredPosition = Vector2.Lerp(from, to, smoothT);

            yield return null;
        }

        betPanelRect.anchoredPosition = to;

        if (!showPanel)
            betPanel.SetActive(false);

        isPanelOpen = showPanel;
        isAnimating = false;
    }
}