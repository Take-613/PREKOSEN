using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class PlayerDefaulltPositionController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private float ObjectSpeed = 2;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.linearVelocity = new Vector2(ObjectSpeed, rigidbody2D.linearVelocity.y);
    }
}
