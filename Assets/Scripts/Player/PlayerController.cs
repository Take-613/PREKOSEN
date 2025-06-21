using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private RectTransform rectTransform;
    private Rigidbody2D rigidbody2D;
    
    int jumpCount = 0;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody2D.linearVelocity = new Vector2(2f, rigidbody2D.linearVelocity.y);
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (jumpCount <= 2)
        {
            jumpCount++;
            rigidbody2D.AddForce(new Vector2(0f, 2.5f), ForceMode2D.Impulse);
        }
        return;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
    }

    // Update is called once per frame
    public void UpdateFontSize()
    {
        Vector2 size = rectTransform.sizeDelta;
        boxCollider.size = new Vector2(size.x, size.y);
    }
}
