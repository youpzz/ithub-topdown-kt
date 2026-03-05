using UnityEngine;
using UnityEngine.Events;

public class InformationTrigger : MonoBehaviour
{
    public enum ShowType {Title, Commentary}

    [SerializeField] private ShowType showType = ShowType.Title;
    [SerializeField] private string messageToSend = "Сообщение";
    [SerializeField] private bool showsAgain = false;
    [SerializeField] private AudioClip soundToPlay;
    [SerializeField] private UnityEvent unityEvent;

    private bool wasShown = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (!showsAgain && wasShown) return;

        if (soundToPlay != null) AudioManager.Instance.PlaySound(soundToPlay);

        if (showType == ShowType.Title) InformationPopup.Instance.ShowTitlePopup(messageToSend);
        else InformationPopup.Instance.ShowCommentaryPopup(messageToSend);

        unityEvent?.Invoke();
        wasShown = true;
    }

}
