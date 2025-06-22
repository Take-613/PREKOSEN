using UnityEngine;

public class MainCameraManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField]
    private float ObjectSpeed = 9;
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(ObjectSpeed, GetComponent<Rigidbody2D>().linearVelocity.y);
    }
}
