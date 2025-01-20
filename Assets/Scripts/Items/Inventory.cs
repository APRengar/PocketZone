using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] float inventorySwitchDuration = 0.5f;

    [Header("ReadOnly")]
    [SerializeField] bool isVisible = false;
    
    private CanvasGroup canvasGroup;

    private void Start() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ToggleVisibility()
    {
        StopAllCoroutines(); // Stops any ongoing coroutine to prevent conflicts
        StartCoroutine(OnVisibilityChanged());
    }
    
    private IEnumerator OnVisibilityChanged()
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = isVisible ? 0f : 1f;

        while (elapsedTime < inventorySwitchDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / inventorySwitchDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = !isVisible;
        canvasGroup.blocksRaycasts = !isVisible;

        isVisible = !isVisible;
    }
}
