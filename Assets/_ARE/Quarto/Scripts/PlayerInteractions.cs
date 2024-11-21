using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private UIController uiController; // Refer?ncia ao controlador de UI
    Camera dummyCamera;
    PlayerController _player;
    PlayerActionsInput _playerActionsInput;
    private HighlightableItem currentlyHighlightedItem;
    private float interactionRadius = 3f; // Raio para detectar objetos interag?veis

    void Start()
    {
        _player = GetComponent<PlayerController>();
        _playerActionsInput = _player?.GetComponent<PlayerActionsInput>();
        dummyCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        DetectInteractableItems();
        if (_playerActionsInput != null && _playerActionsInput.InteractPressed)
        {
            PlayerInteract();
            _playerActionsInput.InteractPressed = false;
        }
    }
    private void DetectInteractableItems()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRadius);
        HighlightableItem closestHighlightable = null;
        float closestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            HighlightableItem highlightable = hit.GetComponent<HighlightableItem>();
            if (highlightable != null)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestHighlightable = highlightable;
                    closestDistance = distance;
                }
            }
        }

        // Atualiza o highlight e o UI para o objeto mais pr?ximo
        if (currentlyHighlightedItem != closestHighlightable)
        {
            // Remover o highlight e esconder o texto do item anterior
            if (currentlyHighlightedItem != null)
            {
                currentlyHighlightedItem.Unhighlight();
                uiController.HideInteractionPrompt();
            }

            // Adicionar o highlight e mostrar o texto para o novo item
            if (closestHighlightable != null)
            {
                currentlyHighlightedItem = closestHighlightable;
                closestHighlightable.Highlight();
                uiController.ShowInteractionPrompt(closestHighlightable.transform, "E");
            }

            currentlyHighlightedItem = closestHighlightable;
        }
        else if (currentlyHighlightedItem != null)
        {
            // Se o item destacado estiver ainda no alcance da c?mera, mantenha o texto vis?vel
            Vector3 screenPos = dummyCamera.WorldToScreenPoint(currentlyHighlightedItem.transform.position);
            if (screenPos.z > 0) // Garante que o objeto est? vis?vel
            {
                uiController.ShowInteractionPrompt(currentlyHighlightedItem.transform, "E");
            }
            else
            {
                uiController.HideInteractionPrompt();
            }
        }
    }

    /*private void OnDrawGizmosSelected()
    {
        // Visualiza??o do raio de intera??o no editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }*/

    public void PlayerInteract()
    {
        var layermask0 = 1 << 0;
        var layermask10 = 1 << 11;
        var finalmask = layermask0 | layermask10;

        RaycastHit hit;
        Ray ray = dummyCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Debug.DrawRay(ray.origin, ray.direction * 15, Color.red, 1f);
        if (Physics.Raycast(ray, out hit, 15, finalmask))
        {
            Debug.Log($"Raycast hit object: {hit.transform.name} at distance {hit.distance}");

            // Check if the object has an Interact script
            Interact interactScript = hit.transform.GetComponent<Interact>();
            HighlightableItem highlightable = hit.transform.GetComponent<HighlightableItem>();

            if (highlightable != null)
            {
                Debug.Log(highlightable);
                if (currentlyHighlightedItem != highlightable)
                {
                    if (currentlyHighlightedItem != null)
                        currentlyHighlightedItem.Unhighlight();

                    highlightable.Highlight();
                    currentlyHighlightedItem = highlightable;

                    // Mostrar o texto de intera??o
                    uiController.ShowInteractionPrompt(currentlyHighlightedItem.transform, "E");
                }

                // L?gica de intera??o
                if (interactScript) // Substituir pela l?gica do Input System se necess?rio
                {
                    interactScript?.CallInteract(this);
                }
            }
        }
        else
        {
            ResetHighlightAndUI();
        }
    }
    // Fun??o auxiliar para resetar o highlight e esconder o texto
    private void ResetHighlightAndUI()
    {
        if (currentlyHighlightedItem != null)
        {
            currentlyHighlightedItem.Unhighlight();
            currentlyHighlightedItem = null;
        }

        uiController.HideInteractionPrompt();
    }
}

 
