using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    [Header("Video Player Settings")]
    [SerializeField] private GameObject _screen; // Tela onde o vídeo será exibido
    [SerializeField] private VideoClip defaultClip; // Vídeo padrão inicial


    [SerializeField] private GameObject _sound = null;

    private VideoPlayer _videoPlayer;
    public event Action OnVideoEnd;

    private void Awake()
    {
        // Obtém o componente VideoPlayer e configura o evento
        _videoPlayer = GetComponent<VideoPlayer>();

        if (_videoPlayer == null)
        {
            Debug.LogError("Nenhum componente VideoPlayer foi encontrado neste GameObject.");
            return;
        }

        _videoPlayer.loopPointReached += OnVideoEndHandler;

        // Configura o primeiro vídeo, caso exista
        if (defaultClip != null)
        {
            PlayVideo(defaultClip);
        }
    }

    private void OnDestroy()
    {
        if (_videoPlayer != null)
        {
            _videoPlayer.loopPointReached -= OnVideoEndHandler;
        }
    }

    /// <summary>
    /// Evento disparado quando o vídeo termina.
    /// </summary>
    private void OnVideoEndHandler(VideoPlayer source)
    {
        if (_screen != null) _screen?.SetActive(false);
        if (_sound != null) _sound?.SetActive(true);
        OnVideoEnd?.Invoke(); // Dispara o evento para ouvintes externos
    }

    /// <summary>
    /// Reproduz um novo vídeo no VideoPlayer.
    /// </summary>
    public void PlayVideo(VideoClip clip)
    {
        if (_videoPlayer == null || clip == null)
        {
            Debug.LogWarning("VideoPlayer ou VideoClip está nulo.");
            return;
        }

        // Ativa a tela, carrega e reproduz o vídeo
        if (_sound != null) _sound?.SetActive(false);
        if (_screen != null) _screen?.SetActive(true);
        _videoPlayer.clip = clip;
        _videoPlayer.Play();

    }

    /// <summary>
    /// Pausa a reprodução do vídeo atual.
    /// </summary>
    public void PauseVideo()
    {
        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
        }
    }

    /// <summary>
    /// Retoma a reprodução do vídeo atual.
    /// </summary>
    public void ResumeVideo()
    {
        if (!_videoPlayer.isPlaying)
        {
            _videoPlayer.Play();
            Debug.Log("Vídeo retomado.");
        }
    }

    /// <summary>
    /// Para o vídeo e reseta o progresso.
    /// </summary>
    public void StopVideo()
    {
        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Stop();
            _screen.SetActive(false);
        }
    }

    /// <summary>
    /// Verifica se o vídeo está a ser reproduzido.
    /// </summary>
    /// <returns>Verdadeiro se o vídeo estiver a ser reproduzido.</returns>
    public bool IsPlaying()
    {
        return _videoPlayer.isPlaying;
    }

    /// <summary>
    /// Reproduz um vídeo quando um objeto é carregado.
    /// </summary>
    public void PlayVideoOnObjectLoad(GameObject obj, VideoClip clip)
    {
        if (obj != null)
        {
            obj.SetActive(true); // Ativa o objeto no jogo
            PlayVideo(clip); // Reproduz o vídeo
        }
    }
}
