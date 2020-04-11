using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.GetComponent<Projectile>()){
            PlayerController.instance.score++;
            PlayerPrefs.SetInt("Score", PlayerController.instance.score);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
