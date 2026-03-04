using System.Collections;
using TMPro;
using UnityEngine;

public class InformationPopup : MonoBehaviour
{
    public static InformationPopup Instance;
    [SerializeField] private UIPanel titlePanel;
    [SerializeField] private UIPanel commentaryPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text commentaryText;

    private Coroutine titleHideCoroutine;
    private Coroutine commentaryCoroutine;

    void Awake()
    {
        Instance = this;
    }
    
    public void ShowTitlePopup(string message, float time = 5f)
    {
        titleText.text = message;

        titlePanel.Show();

        if (titleHideCoroutine != null) StopCoroutine(titleHideCoroutine);

        titleHideCoroutine = StartCoroutine(HideAfterDelay(time, titlePanel));
    }

    public void ShowCommentaryPopup(string message, float time = 10f)
    {
        commentaryText.text = message;

        commentaryPanel.Show();

        if (commentaryCoroutine != null) StopCoroutine(commentaryCoroutine);

        commentaryCoroutine = StartCoroutine(HideAfterDelay(time, commentaryPanel));
    }

    private IEnumerator HideAfterDelay(float time, UIPanel panel)
    {
        yield return new WaitForSeconds(time);
        panel.Hide();
    }
}
