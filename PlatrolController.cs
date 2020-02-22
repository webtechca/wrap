using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatrolController : MonoBehaviour
{
    public GameObject BorderLeft;
    public GameObject BorderRight;
    public float movementSpeed = 5;

    public bool isBomber = false;
    public float startTimeBetweenShots = 1;
    private float timeBetweenShots;
    public GameObject bullet;

    public bool isAPlatform = false;
    private Vector3 nextPosition;

    public bool isBoss = false;
    public int health = 5;
    public GameObject bloodEffect;


    // Start is called before the first frame update
    void Start()
    {
        if (isBomber)
        {
            movementSpeed = 7;
        }
        if (isAPlatform)
        {
            nextPosition = BorderRight.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAPlatform)
        {
            if (transform.position.x >= BorderRight.transform.position.x)
            {
                nextPosition = BorderLeft.transform.position;
            }
            if (transform.position.x <= BorderLeft.transform.position.x)
            {
                nextPosition = BorderRight.transform.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
        } else {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
            if (transform.position.x >= BorderRight.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (transform.position.x <= BorderLeft.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (isBomber)
        {
            Shoot();
        }

        if (health <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void Shoot()
    {
        if (timeBetweenShots <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        } else {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }
}
