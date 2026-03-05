using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    [SerializeField] private string url;

    public void OpenLink()
    {
        Application.OpenURL(url);
    }
}
