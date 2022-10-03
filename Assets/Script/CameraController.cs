using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform userTransform;
    public float cameraSpeed;
    Vector3 offset;
    float playerPositionY;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - userTransform.position;
        playerPositionY = transform.position.y;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 cameraPosition = userTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, cameraPosition, cameraSpeed * Time.deltaTime);

        if (transform.position.y < playerPositionY)
        {
            transform.position = new Vector3(transform.position.x, playerPositionY, transform.position.z);
        }
        if (transform.position.y > playerPositionY)
        {
            transform.position = new Vector3(transform.position.x, playerPositionY, transform.position.z);
        }
    }

}
