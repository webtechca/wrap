using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float timeToLive = 4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, timeToLive);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Destroy(gameObject);

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject Player = other.gameObject;
            Transform lastPortal = Player.GetComponent<PlayerController>().lastPortal;
            other.transform.position = new Vector2(lastPortal.transform.position.x, lastPortal.transform.position.y);
            Player.GetComponent<PlayerController>().justTeleport = true;
        }
        // Ground layer is 9
        if (other.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
