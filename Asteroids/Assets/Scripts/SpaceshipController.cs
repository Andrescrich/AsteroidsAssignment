using System.Collections;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public static SpaceshipController Instance;
    public float smoothTime = 0.3f;
    public bool poweredUp;


    [SerializeField] private float shootingSpeed;
    [SerializeField] private GameObject[] cannons;
    private Vector2 _velocity = Vector2.zero;
    private bool _shooting;
    private Coroutine powerUpCor;

    private void Awake() => Instance = this;

    private void Update()
    {
        Vector3 worldPosition = CameraManager.Instance.cameraComp.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPosition = new Vector2(worldPosition.x, worldPosition.y);
        
        Move(targetPosition);
        Rotate(targetPosition);

        if (Input.GetMouseButton(0) && !_shooting)
            StartCoroutine(Shoot());
        
    }

    private void Move(Vector2 targetPosition)
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
    }

    private void Rotate(Vector2 targetPosition)
    {
        transform.up = targetPosition - (Vector2)transform.position;    
    }

    private IEnumerator Shoot()
    {
        _shooting = true;
        if(!poweredUp)
            ObjectPooler.Instance.Spawn("Bullet", cannons[0].transform.position, cannons[0].transform.rotation);
        else
        {
            foreach (var cannon in cannons)
            {
                ObjectPooler.Instance.Spawn("Bullet", cannon.transform.position, cannon.transform.rotation);
            }
        }
        yield return new WaitForSeconds(shootingSpeed);
        _shooting = false;
    }

    public void PowerUpBuff()
    {
        if(powerUpCor != null)
            StopCoroutine(powerUpCor);
        powerUpCor = StartCoroutine(PowerUpBuffCor());
    }
    
    private IEnumerator PowerUpBuffCor()
    {
        poweredUp = true;
        yield return new WaitForSeconds(3);
        poweredUp = false;
    }
}
