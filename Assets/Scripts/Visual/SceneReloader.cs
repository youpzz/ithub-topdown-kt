using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneReloader : MonoBehaviour
{
    public static SceneReloader Instance;

    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private float fadeDuration = 1.5f;

    void Awake()
    {
        Instance = this;
        blackScreen.alpha = 0;
        blackScreen.gameObject.SetActive(false);
    }

    public void Reload(float delayBeforeFade = 1f)
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.alpha = 0;

        DOVirtual.DelayedCall(delayBeforeFade, () =>
        {
            blackScreen
                .DOFade(1f, fadeDuration)
                .SetEase(Ease.InQuad)
                .OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        });
    }
}