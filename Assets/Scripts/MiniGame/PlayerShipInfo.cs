using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerShipInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    public float score = 3000f;

    public float damageDealt = 0f;
    public float damageTaken = 0f;
    public float dangersDestroyed = 0f; 
    public float timesDead = 0f;

    float currentHealth = 100;
    float maxHealth = 100;

    public float damage = 20f;

    [SerializeField] private HealthBar healthBar;

    [SerializeField] private GameObject shield;
    
    public bool isShieldActive = true;

    [SerializeField] private GameObject playerShip;

    [SerializeField] private Transform playerShipSpawnPoint;

    [SerializeField] private GameObject explosionEffect;

    public GameData gameData {
        get {
            return gameData;
        }
        set {
            if (StaticValues.USE_SKILL_SYSTEM)
            {
                damage *= Mathf.Lerp(1f, 3f, gameData.destruction / 100f);
            }
        }
    }

    public void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }

    void Update()
    {
        // Show score as int
        scoreText.text = "Score: " + ((int)score).ToString();
    }

    public void SpawnPlayerShip()
    {
        currentHealth = maxHealth;
        playerShip.transform.position = playerShipSpawnPoint.position;
        playerShip.SetActive(true);
        PlayerShipMovement playerShipMovement = playerShip.GetComponent<PlayerShipMovement>();
        playerShipMovement.canMove = true;
        StartCoroutine(ActivateShield());
    }

    public void DestroyPlayerShip()
    {
        // Deduct score for dying
        AddScore(-500f);
        timesDead += 500f;

        playerShip.SetActive(false);
        Instantiate(explosionEffect, playerShip.transform.position, Quaternion.identity);
        // Respawn the player ship aftter 2 seconds
        Invoke(nameof(SpawnPlayerShip), 2f);
    }

    private IEnumerator ActivateShield()
    {
        shield.SetActive(true);
        isShieldActive = true;
        yield return new WaitForSeconds(3f);
        shield.SetActive(false);
        isShieldActive = false;
    }

    public void TakeDamage(float damageAmount)
    {
        // Apply skill system
        if (StaticValues.USE_SKILL_SYSTEM)
{
            damageAmount *= Mathf.Lerp(1f, 1f / 3f, gameData.mechanics / 100f);
        }

        if (damageAmount >= currentHealth)
        {
            damageAmount = currentHealth;
        }
        // Deduct score for taking damage
        AddScore(-damageAmount);
        damageTaken += damageAmount;

        currentHealth = Mathf.Max(0f, currentHealth - damageAmount);
        healthBar.SetHealth(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            DestroyPlayerShip();
        }
    }
}
