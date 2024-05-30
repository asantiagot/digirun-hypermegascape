using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public float maxHealth = 100f;
    public float currentHealth;
    public float healthRegenerationDelay = 3f;
    public float healthRegenerationRate = 3f;
    private float lastHitTime;

    public Text healthText;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
       currentHealth = maxHealth; 
       UpdateUI();
    }

    void Update()
    {
        RegenerateHealth();    
    }

    void UpdateUI()
    {
        healthText.text = "Health: " + Mathf.CeilToInt(currentHealth).ToString() + "%";
    }

    void RegenerateHealth()
    {
        if (Time.time - lastHitTime > healthRegenerationDelay && currentHealth < maxHealth)
        {
            currentHealth += healthRegenerationRate * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            UpdateUI();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        lastHitTime = Time.time;
        UpdateUI();
    }
}
