using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneReloader : MonoBehaviour
{
    public static SceneReloader Instance;

    [SerializeField] private Image blackScreen;

    void Awake()
    {
        Instance = this;
        blackScreen.color = new Color(0, 0, 0, 0);
    }

    public void Reload(float timeToFade = 1)
    {
        blackScreen.DOFade(1f, timeToFade)
            .OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}