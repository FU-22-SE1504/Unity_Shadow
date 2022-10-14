using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobject : MonoBehaviour
{
    //* Time to alive
    [SerializeField] private float timeAlive = 1f;
    private void Start()
    {
        //* Destroy game object after 1s
        Destroy(gameObject, timeAlive);
    }
}
