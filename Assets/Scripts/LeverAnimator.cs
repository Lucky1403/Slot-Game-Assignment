using System.Collections;
using UnityEngine;

// This script handles smooth lever pull animation using two GameObjects.
public class LeverAnimator : MonoBehaviour
{
    [Header("Lever GameObjects")]
    public GameObject leverUpImage; //Lever UP Image
    public GameObject leverDownImage; //Lever Down Image

    [Header("Animation Settings")]
    public float pullDownDuration = 0.15f;
    public float holdDuration = 0.4f;
    public float springUpDuration = 0.25f;
    private bool isAnimating = false; //Checking state is lever currently animating ? 

    void Start()
    {
        SetLeverUp(); //Initially lever will be up
    }

    // This method will trigger the full lever pull animation and returns after animation.
    public IEnumerator PlayLeverPull()
    {
        if (isAnimating) yield break;
        isAnimating = true;

        // Move lever down
        leverDownImage.transform.localScale = new Vector3(1f, 0f, 1f);
        leverUpImage.SetActive(false);
        leverDownImage.SetActive(true);

        float elapsed = 0f;
        while (elapsed < pullDownDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / pullDownDuration;
            float scale = Mathf.Lerp(0f, 1f, t * t);
            leverDownImage.transform.localScale = new Vector3(1f, scale, 1f);
            yield return null;
        }

        // Snap to full down position
        leverDownImage.transform.localScale = Vector3.one;

        // Hold Down
        yield return new WaitForSeconds(holdDuration);

        // Spring back up (smooth bounce)
        elapsed = 0f;
        while (elapsed < springUpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / springUpDuration;

            float bounce = Mathf.Sin(t * Mathf.PI);
            float scale = Mathf.Lerp(1f, 0f, t) + (bounce * 0.15f);
            scale = Mathf.Max(0f, scale); // clamp so it doesn't go more upside
            leverDownImage.transform.localScale = new Vector3(1f, scale, 1f);
            yield return null;
        }

        // Switch back to lever up
        SetLeverUp();
        isAnimating = false;
    }

    // Function to Immediately show lever in default state

    private void SetLeverUp()
    {
        leverUpImage.SetActive(true);
        leverDownImage.SetActive(false);
        leverDownImage.transform.localScale = Vector3.one;
    }
}