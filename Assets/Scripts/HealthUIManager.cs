using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    public static HealthUIManager Instance { get; private set; }

    [SerializeField] private GameObject[] healthIndicators; // Array of health indicators

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Optional, depends on your game design
        }
    }

    private void Start()
    {
        // Initialize health UI
        if (GameManager.Instance != null)
        {
            UpdateHealthUI(GameManager.Instance.lives);
        }
    }

    public void SetHealth(int newHealth)
    {
        UpdateHealthUI(newHealth);
    }

    private void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < healthIndicators.Length; i++)
        {
            healthIndicators[i].SetActive(i < currentHealth);
        }
    }
}
