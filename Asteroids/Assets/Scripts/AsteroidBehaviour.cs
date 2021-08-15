using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidBehaviour : MonoBehaviour, IPooledObject
{
    public enum AsteroidType
    {
        Asteroid,
        Asteroid2,
        Asteroid3
    }
    
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private AsteroidType type;
    
    private Collider2D _coll;
    
    private void Awake() => _coll = GetComponent<Collider2D>();
    private void Update() => transform.position += transform.up * (Time.deltaTime * Random.Range(minSpeed, maxSpeed));

    public void onSpawn() => StartCoroutine(SpawnInvuln());

    private IEnumerator SpawnInvuln()
    {
        _coll.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _coll.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7) 
            GameManager.Instance.ModifyHealth(-1);
        
        if (other.gameObject.layer == 8)
        {
            if (type == AsteroidType.Asteroid)
            {
                ObjectPooler.Instance.Spawn("Asteroid2", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                ObjectPooler.Instance.Spawn("Asteroid2", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                GameManager.Instance.IncreaseScore(20);
            }
            else if (type == AsteroidType.Asteroid2)
            {
                ObjectPooler.Instance.Spawn("Asteroid3", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                ObjectPooler.Instance.Spawn("Asteroid3", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                GameManager.Instance.IncreaseScore(50);
            }
            else if (type == AsteroidType.Asteroid3)
            {
                GameManager.Instance.IncreaseScore(100);
            }

            int randomDrop = Random.Range(0, 15);
            switch (randomDrop)
            {
                case 1:
                    ObjectPooler.Instance.Spawn("HealthUp", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    break;
                case 2:
                    ObjectPooler.Instance.Spawn("PowerUp", transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                    break;
            }
            
            gameObject.SetActive(false);
        }
    }
}
