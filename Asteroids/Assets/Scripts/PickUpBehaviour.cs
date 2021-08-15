using UnityEngine;

public abstract class PickUpBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Update() => transform.position += transform.up * (Time.deltaTime * speed);
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            OnPlayerCollision();
            gameObject.SetActive(false);
        }
    }

    protected abstract void OnPlayerCollision();
}
