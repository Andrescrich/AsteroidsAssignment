using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public int CurrentHealth
    {
        get => currentHealth; 
        set
        {
            if (value <= 0)
            {
                currentHealth = 0;
                return;
            }

            if (value >= 3)
            {
                currentHealth = 3;
                return;
            }

            currentHealth = value;
        }
    }
    
    private void Awake() => Instance = this;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void ModifyHealth(int amount)
    {
        CurrentHealth += amount;
        UIManager.Instance.ModifyHealthVisual(CurrentHealth);
    }
    
    public void IncreaseScore(int amount)
    {
        score += amount;
        UIManager.Instance.ModifyScoreVisual(score);
    }
}
