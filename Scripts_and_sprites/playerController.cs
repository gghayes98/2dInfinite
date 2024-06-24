using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public int slideDuration = 1;
    private bool isSliding = false;
    private bool isJumping = false;
    private bool isAttacking = false;
    private float heightOffset = -0.5f; // Adjust as needed to start the slide on the ground.
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private float lastJumpInputTime = -1f;
    private float lastSlideInputTime = -1f;
    private float lastAttackInputTime = -1f;
    public float inputBufferTime = 0.2f;
    public Animator animator;
    public CapsuleCollider2D capCollider;
    public BoxCollider2D swordCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * gameManager.Instance.movementSpeed * Time.deltaTime;
        HandleJumpInput();
        HandleSlideInput();
        HandleAttackInput();

        // Input buffer for a smoother experience
        if (Time.time - lastJumpInputTime < inputBufferTime && !isJumping && !isSliding)
            Jump();
        if (Time.time - lastSlideInputTime < inputBufferTime && !isSliding & !isJumping)
            Slide();
        if (Time.time - lastAttackInputTime < inputBufferTime && !isAttacking)
            Attack();

        animator.SetBool("isJumping", isJumping);
        animator.SetFloat("speedMultiplyer", (gameManager.Instance.movementSpeed / 5f) * 0.5f);
        animator.SetBool("isSliding", isSliding);
        animator.SetBool("isAttacking", isAttacking);
        //rigBody.gravityScale = (gameManager.Instance.movementSpeed / 5f) * 0.5f;
    }

    // Handles the buffer for attacking
    void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            lastAttackInputTime = Time.time;
    }

    // Handle attacking
    void Attack()
    {
        isAttacking = true;
        swordCollider.enabled = true;
        Invoke("StopAttacking", 0.5f);
        // Attack Logic
    }

    void StopAttacking()
    {
        isAttacking = false;
        swordCollider.enabled = false;
    }

    // Handles the buffer for sliding
    void HandleSlideInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            lastSlideInputTime = Time.time;
    }

    // Handle sliding
    void Slide()
    {
        transform.eulerAngles = new Vector3(0, 0, -90);
        transform.localPosition = new Vector3(transform.localPosition.x, originalPosition.y + heightOffset, transform.localPosition.z);
        //capCollider.direction = CapsuleDirection2D.Horizontal;

        isSliding = true;
        Invoke("StopSliding", 1);
    }

    // Handles the buffer for jumping
    void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            lastJumpInputTime = Time.time;
    }

    // Handles jumping
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
    }

    // Handle the end of sliding
    void StopSliding()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, originalPosition.y, transform.localPosition.z);
        transform.eulerAngles = Vector3.zero;
        isSliding = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(collision.gameObject);
        }
    }

    // Logic for all collisions, make sure all objects are tagged appropriately
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }   
        if (collision.gameObject.tag == "Obstacle")
        {
                {Debug.Log("Game Over");
                gameManager.Instance.GameOver();}
        }
    }
}
