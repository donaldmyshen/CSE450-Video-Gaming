using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other){
    	// Reload scene only when colliding with player
        if(other.gameObject.GetComponent<PlayerController>()){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
