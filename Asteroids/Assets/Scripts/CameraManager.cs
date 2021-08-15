using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public Camera cameraComp;

    private void Awake()
    {
        Instance = this;
        cameraComp = GetComponent<Camera>();
    }
}
