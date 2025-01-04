using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Book : MonoBehaviour
{
    // Item variables

    // Picture open animations

    public Interact openFromInteraction;

    [SerializeField] private VideoPlayerScript _videoPlayerScript;
    [SerializeField] private VideoClip _clip;
    [SerializeField] private GameObject _bookUI;

    bool _alreadySawTheClip = false;

    private void OnEnable()
    {
        Interact check = GetComponent<Interact>();
        if (check)
        {
            openFromInteraction = check;
            openFromInteraction.GetInteractEvent.HasInteracted += OpenBook;
        }
        else
        {
            Interact addComp = gameObject.AddComponent<Interact>();
            openFromInteraction = addComp;
            openFromInteraction.GetInteractEvent.HasInteracted += OpenBook;
        }

        // Adiciona o evento para o fim do vídeo
        if (_videoPlayerScript != null)
        {
            _videoPlayerScript.OnVideoEnd += OpenBookUI;
        }
    }

    private void OnDisable()
    {
        if (openFromInteraction)
        {
            openFromInteraction.GetInteractEvent.HasInteracted -= OpenBook;
        }

        if (_videoPlayerScript != null)
        {
            _videoPlayerScript.OnVideoEnd -= OpenBookUI;
        }
    }

    public void OpenBook()
    {
        if (_videoPlayerScript != null && _clip != null && !_alreadySawTheClip)
        {
            _alreadySawTheClip = true;
            _videoPlayerScript.PlayVideo(_clip);
        }
        else
        {
            OpenBookUI();
        }
    }

    private void OpenBookUI()
    {
        var player = openFromInteraction.GetPlayer;
        player.GetComponent<PlayerController>().enabled = false;

        SetCursorStateScript.SetCursorState(true);
        _bookUI.SetActive(true);
        _bookUI.GetComponent<Animator>().SetTrigger("Open");
    }
}

