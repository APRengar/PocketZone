using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGroupAlphaOnDeath : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float alpha = 1f; // Centralized alpha controller

    // Update alpha for all child SpriteRenderers
    private void SetAlpha(float value)
    {
        foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, value);
        }
    }

    public void StartFade(float targetAlpha, float duration)
    {
        StartCoroutine(FadeTo(targetAlpha, duration));
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = alpha; // Current alpha value
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            SetAlpha(alpha);
            yield return null;
        }

        alpha = targetAlpha; // Ensure it ends at the exact target value

    }
}