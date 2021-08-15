using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private float speed;
    
    private void Update()
    {
        transform.position += transform.up * (Time.deltaTime * speed);
    }

    public void onSpawn()
    {
        Invoke(nameof(DisableBullet), 1.5f);
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}
