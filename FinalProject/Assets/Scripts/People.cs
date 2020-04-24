using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class People : MonoBehaviour
{
    public float health;
    public float healthMax;
    public string Name;
    public int baseAttack;
    public Image imageHealthBar;
    public Text HealthUI;

    public CameraShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
