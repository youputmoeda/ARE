using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HighlightableItem : MonoBehaviour
{
    private Material originalMaterial;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Vector3 interactionPromptOffset = new Vector3(0, 2, 0);
    private Renderer objectRenderer;
    private Coroutine highlightCoroutine;

    public Vector3 InteractionPromptOffset => interactionPromptOffset;


    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    public void Highlight()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material = highlightMaterial; // Aplica o material de highlight
        }
    }

    public void Unhighlight()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material = originalMaterial; // Restaura o material original
        }
    }

    private IEnumerator TransitionMaterial(Material targetMaterial, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolando entre os materiais
            objectRenderer.material.Lerp(objectRenderer.material, targetMaterial, elapsedTime / duration);

            yield return null;
        }

        objectRenderer.material = targetMaterial; // Garante que o material final é aplicado
    }
}
