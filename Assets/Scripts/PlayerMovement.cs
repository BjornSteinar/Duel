using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Misc")]
    public int weaponType = 0;
    public bool drawn;
    public bool canMove = true;
    public bool blocking;
    public CapsuleCollider playerCollider;

    [Header("Character Weapon")]
    public GameObject weapon;

    private GameManager manager;
    
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    public KeyCode forward = KeyCode.W;
    public KeyCode left = KeyCode.A;
    public KeyCode backward = KeyCode.S;
    public KeyCode right = KeyCode.D;

    public KeyCode lClick = KeyCode.Mouse0;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;

    public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public Animator playerAnim;

    public GameObject thirdPersonCam;
    public GameObject combatCam;
    private void Start()
    {
        readyToJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        manager = FindObjectOfType<GameManager>();
        if (manager == null) print("No manager in scene");
        drawn = true;
        drawWeapon();
    }


    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded) {
            rb.drag = groundDrag;
        }
        else
            rb.drag = 0;

        if (Input.GetKeyDown(KeyCode.E))
        {
            drawWeapon();
        }

        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            blocking = true;
            playerAnim.SetBool("Blocking", true);
            playerCollider.enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            blocking = false;
            playerAnim.SetBool("Blocking", false);
            playerCollider.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            swingWeapon();
            //playerAnim.ResetTrigger("swing");
        }
    }
    void swingWeapon()
    {
        // Heavy weapon
        if (weaponType == 3 && drawn && !blocking)
        {
            // playerAnim.Play("Heavy weapon attack");
            playerAnim.SetTrigger("swing");
        }
    }    
    void drawWeapon()
    {
        // Heavy weapon
        if (weaponType == 3)
        {
            if (drawn)
            {
                playerAnim.SetInteger("equip", 0);
                weapon.SetActive(false);
                drawn = false;
            }
            else if (!drawn)
            {
                playerAnim.SetInteger("equip", 3);
                playerAnim.SetTrigger("drawing");
                weapon.SetActive(true);
                drawn = true;
            }
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        playerAnim.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"), 0.1f, Time.deltaTime);
        playerAnim.SetFloat("vertical", Input.GetAxisRaw("Vertical"), 0.1f, Time.deltaTime);
        
        if (thirdPersonCam.activeSelf) { 
            if (Input.GetKey(forward) || Input.GetKey(left) || Input.GetKey(backward) || Input.GetKey(right) && grounded) 
            {
                playerAnim.SetBool("walking", true);
                playerAnim.SetBool("combat walking", false);
            }
            else
            {
                playerAnim.SetBool("walking", false);
            }
        }

        if (combatCam.activeSelf)
        {
            if (Input.GetKey(forward) || Input.GetKey(left) || Input.GetKey(backward) || Input.GetKey(right) && grounded)
            {
                playerAnim.SetBool("combat walking", true);
                playerAnim.SetBool("walking", false);
            }
            else
            {
                playerAnim.SetBool("combat walking", false);
            }
        }

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            playerAnim.SetBool("Jump", true);
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        if (canMove)
        {
            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;


            // on ground
            if (grounded)
            {
                playerAnim.SetBool("Jump", false);
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            // in air
            else if (!grounded)
                playerAnim.SetBool("combat walking", false);
            playerAnim.SetBool("walking", false);
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {

        readyToJump = true;
    }
    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(2);
    }
}
