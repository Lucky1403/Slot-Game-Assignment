using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// This script controls a single reel's symbols and spinning animation and it uses frame-based timing for smooth symbol flickering.
public class ReelController : MonoBehaviour
{
    [Header("Symbol Image Slots")]
    public Image symbolTop;
    public Image symbolMid;
    public Image symbolBot;

    [Header("All Possible Symbols")]
    public Sprite[] symbolSprites;

    [Header("Spin Settings")]
    public float symbolSwapInterval = 0.08f;
    private int finalSymbolIndex; //final symbol index this reel lands on (middle row)

    // Spins the reel for the given duration then snaps to result. Then Gradually slows down near the end for a realistic feel.
    public IEnumerator Spin(float duration)
    {
        float elapsed = 0f;
        float timeSinceLastSwap = 0f;
        float slowDownStart = duration - 0.6f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            timeSinceLastSwap += Time.deltaTime;

            float currentInterval = symbolSwapInterval;
            if (elapsed > slowDownStart)
            {
                float t = (elapsed - slowDownStart) / 0.6f;
                currentInterval = Mathf.Lerp(symbolSwapInterval, 0.2f, t);
            }

            if (timeSinceLastSwap >= currentInterval)
            {
                timeSinceLastSwap = 0f;
                symbolTop.sprite = GetRandomSprite();
                symbolMid.sprite = GetRandomSprite();
                symbolBot.sprite = GetRandomSprite();
            }

            yield return null;
        }

        //Random symbols using RNG
        finalSymbolIndex = Random.Range(0, symbolSprites.Length);

        symbolMid.sprite = symbolSprites[finalSymbolIndex];
        symbolTop.sprite = symbolSprites[(finalSymbolIndex + 1) % symbolSprites.Length];
        symbolBot.sprite = symbolSprites[(finalSymbolIndex + symbolSprites.Length - 1) % symbolSprites.Length];
    }

    public int GetResult() => finalSymbolIndex;

    private Sprite GetRandomSprite()
    {
        return symbolSprites[Random.Range(0, symbolSprites.Length)];
    }
}