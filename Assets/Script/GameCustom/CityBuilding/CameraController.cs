using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController
{
    [SerializeField] private CinemachineVirtualCamera camera;

    public void SetTarget(GameObject target)
    {
        camera.Follow = target.transform;
        camera.LookAt = target.transform;
    }
}