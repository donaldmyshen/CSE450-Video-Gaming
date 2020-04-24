using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (MyPlayer.instance.healthMax - MyPlayer.instance.health >= 10)
            {
                MyPlayer.instance.health += 10;
                Destroy(gameObject);
            }
        }
    }
}
