using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Door : MonoBehaviour
{
    // Item variables

    // Door open animations
    [SerializeField] private VideoPlayerScript _videoPlayerScript;
    [SerializeField] private VideoClip _clip;
    [SerializeField] private GameObject _endUI;


    public Interact openFromInteraction;

    private void OnEnable()
    {
        Interact check = GetComponent<Interact>();
        if (check)
        {
            openFromInteraction = check;
            openFromInteraction.GetInteractEvent.HasInteracted += OpenDoor;
        }
        else
        {
            Interact addComp = gameObject.AddComponent<Interact>();
            openFromInteraction = addComp;
            openFromInteraction.GetInteractEvent.HasInteracted += OpenDoor;
        }

        if (_videoPlayerScript != null)
        {
            _videoPlayerScript.OnVideoEnd += OpenEndUI;
        }
    }

    private void OnDisable()
    {
        if (openFromInteraction)
        {
            openFromInteraction.GetInteractEvent.HasInteracted -= OpenDoor;
        }

        if (_videoPlayerScript != null)
        {
            _videoPlayerScript.OnVideoEnd -= OpenEndUI;
        }
    }

    public void OpenDoor()
    {
        if (_videoPlayerScript != null && _clip != null)
        {
            _videoPlayerScript.PlayVideo(_clip);
        }

        var player = openFromInteraction.GetPlayer;
        player.GetComponent<PlayerController>().enabled = false;
    }

    private void OpenEndUI()
    {
        SetCursorStateScript.SetCursorState(true);

        _endUI.SetActive(true);
    }
}


