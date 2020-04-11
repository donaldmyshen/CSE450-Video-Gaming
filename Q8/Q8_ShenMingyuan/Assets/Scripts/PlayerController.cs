using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Outlets
    Rigidbody2D _rigidbody;

    // Configurations
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyUp))
        {
            _rigidbody.AddForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(keyDown))
        {
            _rigidbody.AddForce(Vector2.down * moveSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(keyLeft))
        {
            _rigidbody.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(keyRight))
        {
            _rigidbody.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
        }
    }
}
