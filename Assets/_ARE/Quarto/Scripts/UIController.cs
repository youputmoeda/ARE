using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private RawImage interactionImagePrompt;
    [SerializeField] private Camera dummyCamera; // Para posicionar o texto corretamente
    private Transform currentTarget; // Objeto interag?vel atualmente destacado
    private Vector3 currentOffset; // Offset atual do objeto interag?vel

    // Refer?ncia para o material do objeto com o outline (caso precise de customiza??o de cor/espessura)
    private Material targetMaterial;
    private Color originalOutlineColor;
    private float originalOutlineWidth;

    private void Awake()
    {
        if (interactionImagePrompt == null)
        {
            Debug.LogError("Interaction Prompt is not assigned in the UIController!");
        }

        interactionImagePrompt.gameObject.SetActive(false); // Come?a desativado
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            Vector3 screenPosition = dummyCamera.WorldToScreenPoint(currentTarget.position + currentOffset);

            if (screenPosition.z > 0 && screenPosition.x >= 0 && screenPosition.x <= Screen.width && screenPosition.y >= 0 && screenPosition.y <= Screen.height)
            {
                interactionImagePrompt.transform.position = screenPosition;
                interactionImagePrompt.gameObject.SetActive(true);
            }
            else
            {
                interactionImagePrompt.gameObject.SetActive(false);
            }
        }
    }

    // Chamado pelo Player para ativar o texto
    public void ShowInteractionPrompt(Transform target, string message)
    {
        currentTarget = target; // Define o alvo atual
        interactionImagePrompt.gameObject.SetActive(true);

        // Obt?m o offset do objeto interag?vel, se existir
        HighlightableItem item = target.GetComponent<HighlightableItem>();
        currentOffset = item != null ? item.InteractionPromptOffset : Vector3.zero; // Usa o offset do objeto ou 0
    }

    // Chamado pelo Player para esconder o texto
    public void HideInteractionPrompt()
    {
        interactionImagePrompt.gameObject.SetActive(false);
        currentTarget = null;

        // Remover o highlight
        if (targetMaterial != null)
        {
            ResetOutline();
        }
    }

    // Fun??o para adicionar o highlight no objeto
    private void HighlightObject(Transform target)
    {
        // Verificar se o objeto tem um componente Renderer
        Renderer targetRenderer = target.GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            targetMaterial = targetRenderer.material; // Pega o material do objeto

            // Salvar as cores e configura??es originais do outline
            originalOutlineColor = targetMaterial.GetColor("_OutlineColor");
            originalOutlineWidth = targetMaterial.GetFloat("_OutlineWidth");

            // Alterar a cor e a espessura do outline
            targetMaterial.SetColor("_OutlineColor", Color.green); // Ou qualquer cor desejada
            targetMaterial.SetFloat("_OutlineWidth", 0.1f); // Controlar o tamanho do contorno
        }
    }

    // Fun??o para restaurar o outline ao normal
    private void ResetOutline()
    {
        if (targetMaterial != null)
        {
            // Restaurar as configura??es originais
            targetMaterial.SetColor("_OutlineColor", originalOutlineColor);
            targetMaterial.SetFloat("_OutlineWidth", originalOutlineWidth);
        }
    }
}
