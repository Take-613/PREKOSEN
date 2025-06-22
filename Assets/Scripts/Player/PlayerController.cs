using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private RectTransform rectTransform;
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private float playerSpeed = 2;
    [SerializeField]
    private float jumpForce = 2;

    private float positionSub = 0;

    [SerializeField] private GameObject defaultPosition;
    
    int jumpCount = 0;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        //defaultPosition = GameObject.Find("PlayerDefaultPosition");
    }

    void FixedUpdate()
    {
        positionSub = defaultPosition.transform.position.x - transform.position.x;
        rigidbody2D.linearVelocity = new Vector2(playerSpeed+positionSub*2, rigidbody2D.linearVelocity.y);
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (jumpCount <= 1 && rigidbody2D.linearVelocity.y < 0.01)
        {
            jumpCount++;
            rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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
