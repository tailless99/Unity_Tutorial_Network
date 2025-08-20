using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCamera;

    private void Awake() {
        mainCamera = Camera.main.transform;
    }

    private void LateUpdate() {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
}
