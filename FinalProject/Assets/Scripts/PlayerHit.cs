using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    float attacktaked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
       
        if(other.CompareTag("breakable")){
            print("breakable");
            other.GetComponent<Torch>().Smash();
        }
        else if (other.gameObject.CompareTag("enemy"))
        {
            print("hit something");
            attacktaked = MyPlayer.instance.attack;
            other.GetComponent<Enemy>().TakeDamage(attacktaked);
        }
    }

}
