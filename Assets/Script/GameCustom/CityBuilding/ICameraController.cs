using UnityEngine;

public interface ICameraController
{
    void SetTarget(GameObject target);
    GameObject GetCamera();
}