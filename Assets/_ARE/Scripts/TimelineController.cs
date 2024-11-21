using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    [SerializeField] private PlayableDirector timelineDirector;

    private void Start()
    {
        // Certifica-te de que a Timeline começa parada
        timelineDirector.Stop();
    }

    // Este métofo pode ser chamado no momento em que quiseres iniciar a Timeline
    public void PlayTimeline()
    {
        timelineDirector.Play();
    }

    // Exemplo de como parar a Timeline, se necessário
    public void StopTimeline()
    {
        timelineDirector.Stop();
    }

    // Podes ativar a Timeline em resposta a um evento específico
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayTimeline();
        }
    }
}
