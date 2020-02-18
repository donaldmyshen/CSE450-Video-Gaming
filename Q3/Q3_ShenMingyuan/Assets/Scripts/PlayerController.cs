using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Outlet
	Rigidbody2D rigidbody;
    public Transform aimPivot;
    public GameObject projectilePrefab;

    // State Tracking
    public int jumpsLeft;

	// Methods
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            rigidbody.AddForce(Vector2.left*12f);
        }
        if(Input.GetKey(KeyCode.D)){
            rigidbody.AddForce(Vector2.right*12f);
        }
        
        // Aim Toward Mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * 180f / Mathf.PI;

        aimPivot.transform.rotation = Quaternion.Euler(0,0,angleToMouse);

        // Shoot
        if(Input.GetMouseButtonDown(0)){
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        // Make player jump
        if(Input.GetKeyDown(KeyCode.Space)){
            if (jumpsLeft>0){
                jumpsLeft--;
                rigidbody.AddForce(Vector2.up*10f, ForceMode2D.Impulse);
            }       
        }
    }
    
    void OnCollisionEnter2D(Collision2D other){
        // Check that we collided with the ground
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){

            // Check what is directly below our character's feet
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 0.7f);

            // We might have multiple things beneath our character's feet at once
            for(int i=0; i < hits.Length; i++){
                RaycastHit2D hit = hits[i];
                //check that we collided with the ground right below our feet
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
                    // Reset jump count
                    jumpsLeft = 2;
                }
            }
        }
    }
}
