using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [Header("Shake Defaults")]
    [SerializeField] private float shakeDuration = 0.25f;
    [SerializeField] private float shakeMagnitude = 0.3f;
    [SerializeField] private int vibrato = 20;
    [SerializeField] private float randomness = 90f;

    private Vector3 originalPos;
    private Tween shakeTween;

    void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
    }

    public void TriggerShake() => TriggerShake(shakeDuration, shakeMagnitude);

    public void TriggerShake(float duration, float magnitude)
    {
        shakeTween?.Kill();
        transform.localPosition = originalPos;

        shakeTween = transform
            .DOShakePosition(duration, magnitude, vibrato, randomness, false, true)
            .OnComplete(() => transform.localPosition = originalPos);
    }
}