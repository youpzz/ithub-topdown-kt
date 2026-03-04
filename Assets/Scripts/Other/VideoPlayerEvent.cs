using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoPlayerEvent : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    [Space]
    public UnityEvent onVideoFinished;
    public UnityEvent onVideoStarted;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.started += OnVideoStarted;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        onVideoFinished?.Invoke();
    }

    private void OnVideoStarted(VideoPlayer vp)
    {
        onVideoStarted?.Invoke();
    }

    void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
        videoPlayer.started -= OnVideoStarted;
    }
}
