using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{

    public static PlayerController instance;
    //Outlet
    Rigidbody2D rigidbody;
    public Transform aimPivot;
    public GameObject projectilePrefab;
    SpriteRenderer sprite;
    Animator animator;

    public Text scoreUI;

    //State Tracking
    public int jumpsLeft;
    public int score;

    // Methods
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
        if (rigidbody.velocity.magnitude > 0){
            animator.speed = rigidbody.velocity.magnitude / 3f;
        }
        else {
            animator.speed = 1f;
        }
    }

    // Update is called once per frame
    void Update(){

        // Update UI
        scoreUI.text = score.ToString();

        //Move Player Left
        if (Input.GetKey(KeyCode.A)){
            rigidbody.AddForce(Vector2.left * 12f);
            sprite.flipX = true;
        }

        //Move Player Right
        if (Input.GetKey(KeyCode.D)){
            rigidbody.AddForce(Vector2.right * 12f);
            sprite.flipX = false;
        }

        //Aim Towards Mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * 180f / Mathf.PI;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        //Shoot
        if (Input.GetMouseButtonDown(0)){
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;

        }
        //Player Jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0){
            jumpsLeft--;
            rigidbody.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
        }
        animator.SetInteger("JumpsLeft", jumpsLeft);
    }

    private void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 1f);

            for (int i = 0; i < hits.Length; i++){
                RaycastHit2D hit = hits[i];
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
                    jumpsLeft = 2;
                }
            }
        }        
    }
}
