using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player;

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
        if (other.gameObject.CompareTag("Player"))
        {
            // Player just teleport, so prevent him from teleporting again right away to avoid loops
            if (Player.GetComponent<PlayerController>().justTeleport) {
                Player.GetComponent<PlayerController>().justTeleport = false;
                return;
            }
            StartCoroutine(Teleport());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.5f);
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
        Player.GetComponent<PlayerController>().justTeleport = true;
        Player.GetComponent<PlayerController>().lastPortal = Portal.transform;
    }
}
