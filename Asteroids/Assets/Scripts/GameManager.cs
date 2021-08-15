using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int score;
    public bool respawning;
    public bool poweredUp;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    
    private AsteroidSpawner _asteroidSpawner;
    private GameObject _player;
    private Coroutine _powerUpCor;

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
    
    public IEnumerator ShipDestroyed()
    {
        _player.GetComponent<SpaceshipController>().enabled = false;
        ModifyHealth(-1);
        
        yield return new WaitForSeconds(1);
        
        _player.gameObject.SetActive(false);

        if (CurrentHealth == 0) 
            RestartLevel();
        
        _player = ObjectPooler.Instance.Spawn("Player", transform.position, quaternion.identity);
        _player.GetComponent<SpaceshipController>().enabled = true;
    }

    private void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void ModifyHealth(int amount)
    {
        CurrentHealth += amount;
        UIManager.Instance.UpdateHealthVisual();
    }
    
    public void PowerUpBuff()
    {
        if(_powerUpCor != null)
            StopCoroutine(_powerUpCor);
        _powerUpCor = StartCoroutine(PowerUpBuffCor());
    }
    
    private IEnumerator PowerUpBuffCor()
    {
        poweredUp = true;
        yield return new WaitForSeconds(3);
        poweredUp = false;
    }
    
    public void IncreaseScore(int amount)
    {
        score += amount;
        UIManager.Instance.ModifyScoreVisual(score);
    }

    //Called as Play button event
    public void StartGame()
    {
        _player = ObjectPooler.Instance.Spawn("Player", transform.position, quaternion.identity);
        UIManager.Instance.DeactivateButton();
        UIManager.Instance.UpdateHealthVisual();
        
        _asteroidSpawner = new AsteroidSpawner();
        _asteroidSpawner.SpawnRound(5);
    }
}
