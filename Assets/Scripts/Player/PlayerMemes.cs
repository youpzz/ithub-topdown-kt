using UnityEngine;

public class PlayerMemes : MonoBehaviour
{
    [Header("Custom Death")]
    [SerializeField] private AudioClip[] deathSfx;
    [SerializeField] private GameObject deathParticle;
    
    private Health health;


    void Awake()
    {
        health = GetComponent<Health>();

        
    }

    void Start()
    {
        health.OnDeath += DeathTroll;
    }


    void DeathTroll()
    {
        health.OnDeath -= DeathTroll;
        int sfxIndex = Random.Range(0, deathSfx.Length);
        AudioManager.Instance.PlaySound(deathSfx[sfxIndex]);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        SceneReloader.Instance.Reload(deathSfx[sfxIndex].length + 2.5f);

        string deathMessage = new string[] { "Помер", "Скончался", "Не повезло", "Сдох" } [Random.Range(0, 4)];

        InformationPopup.Instance.ShowBigPopup(deathMessage);

        Destroy(transform.gameObject);

    }


}
