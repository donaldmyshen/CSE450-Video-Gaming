using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Enemy : MonoBehaviour
{

    public float health;
    public float healthMax;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public Image imageHealthBar;
    
    public CameraShake cameraShake;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   /* void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<PlayerHit>()) {
            print("take damage");
            TakeDamage(1f);
        }
    }
    */
    public virtual void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        StartCoroutine(cameraShake.Shake(.05f,.1f));
        if (health <= 0)
        {
            print("Enemy dead");
            Destroy(gameObject);
            MyPlayer.instance.exp += 5;
            MyPlayer.instance.money += 10;
            PlayerPrefs.SetInt("money", MyPlayer.instance.money);
        }
                imageHealthBar.fillAmount = health / healthMax;
    }
}
