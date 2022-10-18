using System;
using Cinemachine;
using SystemOfExtras;
using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController
{
    [SerializeField] private CinemachineVirtualCamera camera;

    private void OnEnable()
    {
        ServiceLocator.Instance.RegisterService<ICameraController>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Instance.RemoveService<ICameraController>(this);
    }

    public void SetTarget(GameObject target)
    {
        camera.Follow = target.transform;
        camera.LookAt = target.transform;
    }
}