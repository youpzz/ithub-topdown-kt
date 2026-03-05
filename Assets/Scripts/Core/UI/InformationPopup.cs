using System.Collections;
using TMPro;
using UnityEngine;

public class InformationPopup : MonoBehaviour
{
    public static InformationPopup Instance;
    [SerializeField] private UIPanel titlePanel;
    [SerializeField] private UIPanel commentaryPanel;
    [SerializeField] private UIPanel mainPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text commentaryText;
    [SerializeField] private TMP_Text mainText;

    private Coroutine titleHideCoroutine;
    private Coroutine commentaryCoroutine;
    private Coroutine mainCoroutine;

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

    public void ShowBigPopup(string message, float time = 10f)
    {
        mainText.text = message;

        mainPanel.Show();

        if (mainCoroutine != null) StopCoroutine(mainCoroutine);

        mainCoroutine = StartCoroutine(HideAfterDelay(time, mainPanel));
    }


    private IEnumerator HideAfterDelay(float time, UIPanel panel)
    {
        yield return new WaitForSeconds(time);
        panel.Hide();
    }
}
