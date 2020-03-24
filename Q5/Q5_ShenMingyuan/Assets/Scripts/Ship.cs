using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // Outlet
    public GameObject projectilePrefab;

    // State Tracking
    public float firingDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FiringTimer");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(0, Mathf.Sin(GameController.instance.timeElapsed) * 3f);
    }

    void FireProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    IEnumerator FiringTimer()
    {
        yield return new WaitForSeconds(firingDelay);

        FireProjectile();

        StartCoroutine("FiringTimer");
    }
}
