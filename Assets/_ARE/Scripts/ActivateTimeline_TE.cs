using UnityEngine;
using System.Collections;

public class ActivateTimeline_TE : MonoBehaviour
{
    [SerializeField] private TimelineController _timelineController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _timelineController.PlayTimeline();
        }
    }
}
