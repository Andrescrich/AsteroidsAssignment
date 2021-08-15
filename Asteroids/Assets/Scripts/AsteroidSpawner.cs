using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    private void Start() => SpawnRound(6);

    private void SpawnRound(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float randomXPos = Random.Range(-8.5f, 8.5f);
            float randomYPos = Random.Range(-5.5f, 5.5f);
            Vector3 pos = new Vector3(randomXPos, randomYPos, 0);
            ObjectPooler.Instance.Spawn("Asteroid", pos, quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }
}
