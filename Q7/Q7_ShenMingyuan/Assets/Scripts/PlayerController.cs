using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    // Outlet
    Rigidbody2D rigidbody;
    public Transform aimPivot;
    public GameObject projectilePrefab;
    SpriteRenderer sprite;
    Animator animator;
    public Text scoreUI;

    // State Tracking
    public int jumpsLeft;
    public int score;
    public bool isPaused;

    // Methods
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        score = PlayerPrefs.GetInt("Score");
    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
        if (rigidbody.velocity.magnitude > 0)
        {
            animator.speed = rigidbody.velocity.magnitude / 3f;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // We stop the Update loop execution right away if the game is paused.
        if (isPaused)
        {
            return;
        }
        // Update UI
        scoreUI.text = score.ToString();

        // Move player left
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(Vector2.left * 12f);
            sprite.flipX = true;
        }
        // Move player right
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(Vector2.right * 12f);
            sprite.flipX = false;
        }
        // Make player jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            jumpsLeft--;
            rigidbody.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
        }
        animator.SetInteger("JumpsLeft", jumpsLeft);

        // Aim Toward Mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * 180f / Mathf.PI;

        aimPivot.transform.rotation = Quaternion.Euler(0, 0, angleToMouse);

        // Shoot
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        // the Escape key will trigger showing the menu.
        if (Input.GetKey(KeyCode.Escape))
        {
            MenuController.instance.Show();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 1f);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = 2;
                }
            }
        }
    }
}