using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //* Variable
    [SerializeField] private float cameraSpeed = 10f;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, cameraSpeed * Time.deltaTime);
    }

    // public void MoveToNewRoom()
}
