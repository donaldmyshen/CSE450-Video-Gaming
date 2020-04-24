using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyPlayer : MonoBehaviour
{
    public static MyPlayer instance;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public Text MoneyUI;
    public int money;
    public Text AttackUI;
    public float attack = 1f;

    public float health = 50;
    public float healthMax;
    public string Name;



    public Image imageHealthBar;
    public Text HealthUI;
    public Text HealthMAXUI;
    public CameraShake cameraShake;
    public int lv = 0;
    public float exp = 0;
    public float[] exp_reach = new float[ ]{10, 20, 30, 50, 100 };
    public Text lvUI;
    public Text EXPUI;
    public Text EXP_REACHUI;



    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //attack = PlayerPrefs.GetFloat("attack");
        money = PlayerPrefs.GetInt("money");
        health = 40;
        healthMax = 50;
        attack = 1;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkLevel();
        lvUI.text = lv.ToString();
        EXPUI.text = exp.ToString();
        EXP_REACHUI.text = exp_reach[lv].ToString();
        HealthMAXUI.text = healthMax.ToString();
        HealthUI.text = health.ToString();
        MoneyUI.text = money.ToString();
        AttackUI.text = attack.ToString();
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("attack"))
        {
            StartCoroutine(AttackCo());
        }
       UpdateAnimationAndMove();
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
    }


    void checkLevel()
    {
        if(exp == exp_reach[lv])
        {
            lv++;
            attack++;
            healthMax += 5;
        }
    }


    void UpdateAnimationAndMove(){
        if(change != Vector3.zero)
                {
                    MoveCharacter();
                    animator.SetFloat("moveX", change.x);
                    animator.SetFloat("moveY", change.y);
                    animator.SetBool("moving", true);
                }
                else{
                    animator.SetBool("moving",false);
                }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.fixedDeltaTime
        );
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        StartCoroutine(cameraShake.Shake(.05f, .1f));
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }
        imageHealthBar.fillAmount = health / healthMax;
    }

}
