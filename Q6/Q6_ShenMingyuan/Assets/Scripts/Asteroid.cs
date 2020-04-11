using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Outlets
    Rigidbody2D rigidbody;

    // State tracking
    float randomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        randomSpeed = Random.Range(0.5f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // Always move left
        rigidbody.velocity = Vector2.left * randomSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
