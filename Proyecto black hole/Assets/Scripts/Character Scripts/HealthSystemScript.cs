using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemScript : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public bool invensible = false;

    private float invulnerabilityTime = 1f;
    private float brakeTime = 0.2f;
    [SerializeField] private HealthBarrUiScript healthBar;

    public GameObject dieTextAsset; 

    public void Start()
    {
        dieTextAsset.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(true);
        health = maxHealth;
        healthBar.InicializeHealthBar(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!invensible && health > 0)
        {
            health -= damage;
            healthBar.SetHealth(health);
            Debug.Log("Health: " + health);
            StartCoroutine(Invunerability());
            StartCoroutine(brakeVelocity());
            if (health ==0)
            {
                Debug.Log("You are dead");
                Time.timeScale = 0;
                dieTextAsset.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator Invunerability()
    {
        invensible = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        invensible = false;
    }

    IEnumerator brakeVelocity()
    {
        var actualVelocity = GetComponent<PlayerController>().walkSpeed;
        GetComponent<PlayerController>().walkSpeed = 0;
        yield return new WaitForSeconds(brakeTime);
        GetComponent<PlayerController>().walkSpeed = actualVelocity;
    }
}
