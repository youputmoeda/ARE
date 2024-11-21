using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    InteractEvent interact = new InteractEvent();
    PlayerInteractions player;

    public InteractEvent GetInteractEvent
    {
        get
        {
            if (interact == null) interact = new InteractEvent();
            return interact;
        }
    }

    public PlayerInteractions GetPlayer
    {
        get
        {
            return player;
        }
    }

    public void CallInteract(PlayerInteractions interactedPlayer)
    {
        player = interactedPlayer;
        interact.CallInteractEvent();
    }
}

public class InteractEvent
{
    public delegate void InteractHandler();

    public event InteractHandler HasInteracted;

    public void CallInteractEvent() => HasInteracted?.Invoke();
}
