using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : MonoBehaviour
{
    
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.y - movementDistance;
        rightEdge = transform.position.y + movementDistance;
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    public void Update()
    {
        if (movingLeft)
        {
            if (transform.position.y > leftEdge)
            {
               transform.position = new Vector3(transform.position.x , transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.y < rightEdge)
            {
                transform.position = new Vector3(transform.position.x , transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }
}
