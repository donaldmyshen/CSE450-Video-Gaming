using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;

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
            if (other.gameObject.CompareTag("enemy"))
            {
                if (other.GetComponent<Rigidbody2D>() != null)
                {
                    Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
                    if (enemy != null)
                    {
                        enemy.isKinematic = false;
                        Vector2 difference = enemy.transform.position - transform.position;
                        difference = difference.normalized * thrust;
                        enemy.AddForce(difference, ForceMode2D.Impulse);
                        StartCoroutine(KnockCo(other));
                        //other.GetComponent<Enemy>().TakeDamage(1f);
                }
                    
                }
        }

    }

    private IEnumerator KnockCo(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                
                yield return new WaitForSeconds(knockTime);
                if (enemy != null)
                {
                    enemy.velocity = Vector2.zero;
                    enemy.isKinematic = true;
                }
            }
        }
    }
}
