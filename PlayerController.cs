using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
    mass: 1
    linear drag: 0
    angular drag: 0.05
    gravity scale: 5
    */
    public float velocity = 7;
    private float moveInput;
    private Rigidbody2D rb;

    // Jump
    public float jumpForce = 7;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius = 0.3f;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime = 0.3f;
    private bool isJumping;

    // Teleport
    public bool justTeleport = false;
    public Transform lastPortal;

    // Mele attack
    private float timeBetweenAttack;
    public float startTimeBetweenAttack = 0.3f;
    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatAreEnemies;
    public int damage = 1;

    private bool hasWeapon = false;

    // public float initialX;

    // Animator animator;
    // Vector2 movement;

    // public Transform firePoint;
    // public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveInput > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (moveInput < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Jump
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            isJumping = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }

        // Mele attack
        if (hasWeapon && timeBetweenAttack <= 0)
        {
            if (Input.GetKey(KeyCode.F))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatAreEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<PlatrolController>().TakeDamage(damage);
                }
            }
            timeBetweenAttack = startTimeBetweenAttack;
        } else {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * velocity, rb.velocity.y);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    public void Shoot()
    {
        // bullet
        // Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameUiController.instance.incrementCoins();
        }
        if (other.gameObject.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            GameUiController.instance.incrementKeys();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            transform.position = new Vector2(lastPortal.transform.position.x, lastPortal.transform.position.y);
            justTeleport = true;
            // Destroy(other.transform.parent.gameObject);
        }
        if (other.gameObject.CompareTag("Weapon"))
        {
            // Destroy(other.gameObject);
            hasWeapon = true;
            // Move weapon to player
            GameObject WeaponParent = other.gameObject.transform.parent.gameObject;
            WeaponParent.transform.parent = transform;
            WeaponParent.transform.position = new Vector2(attackPosition.position.x, attackPosition.position.y);

            StartCoroutine(GameUiController.instance.FlashMessage("Hit with (F) key!"));
        }
        if (other.gameObject.CompareTag("Door"))
        {
            int keys = GameUiController.instance.GetKeys();
            if (keys > 0)
            {
                Destroy(other.gameObject);
                keys--;
                GameUiController.instance.setKeys(keys);
            }
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            GameUiController.instance.PauseGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {

    }

    public void Jump()
    {

    }
}
