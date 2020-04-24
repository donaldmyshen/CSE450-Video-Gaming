using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EE : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;


    float lastPositionX = 0.0f;
    float lastPositionY = 0.0f;
    private Animator animator;
    private Rigidbody2D myRigidbody;
    float time = 0;


    // Start is called before the first frame update
    void Start()
    {
        lastPositionX = transform.position.x;
        lastPositionY = transform.position.y;
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float velocityX = 0;
        float velocityY = 0;
        if (lastPositionX != velocityX)
        {
            velocityX = (transform.position.x - lastPositionX);
            lastPositionX = transform.position.x;
        }
        if (lastPositionY != velocityY)
        {
            velocityY = (transform.position.y - lastPositionY);
            lastPositionY = transform.position.y;
        }

        //     print("velocityX");
        //    print(velocityX);
        //    print("velocityY");
        //    print(velocityY);
        if (velocityY != 0)
        {
            //animator.SetBool("moving", true);
            animator.SetFloat("moveY", velocityY);
        }
        if (velocityX != 0)
        {
            //animator.SetBool("moving", true);
            animator.SetFloat("moveX", velocityX);
        }
        CheckDistance();
    }



    void CheckDistance()
    {
        //print(time);
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
        && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            // print(Vector3.Distance(target.position, transform.position));
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            myRigidbody.MovePosition(temp);
        }
        if (Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            print("can attack!");
            if (time > 1f)
            {
                StartCoroutine(AttackCo());
            }
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        time = 0;
    }

    public override void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        StartCoroutine(cameraShake.Shake(.05f, .1f));
        if (health <= 0)
        {
            print("EE dead");
            SceneManager.LoadScene(3);
            MyPlayer.instance.money += 10;
            PlayerPrefs.SetInt("money", MyPlayer.instance.money);
            PlayerPrefs.SetFloat("attack", MyPlayer.instance.attack);
        }
        imageHealthBar.fillAmount = health / healthMax;
    }

}
