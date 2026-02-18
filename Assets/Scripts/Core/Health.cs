using System;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public event Action<float, float> OnHealthChanged;

    [SerializeField] private float health;


    private float maxHealth;


    void Awake()
    {
        maxHealth = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage; 
        if (health <= 0)
        {
            if (gameObject.tag == "Player") SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            Destroy(gameObject);
        }
        
        OnHealthChanged?.Invoke(health, maxHealth);
    }

    

}
