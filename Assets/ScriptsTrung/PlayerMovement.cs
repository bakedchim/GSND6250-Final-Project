using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6.0f;

    public float groundDrag = 6.0f;

    [Header("Ground Check")]
    public Transform playerObj;
    public CapsuleCollider playerCollider;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    public float jumpForce = 5.0f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.5f;
    bool readyToJump = true;
    
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public Camera playerCam;

    public bool canMove = true;

    public GameControllerTrung gameControllerTrung;

    public GameObject interctableObject;

    [SerializeField]
    private ObjectiveController objectiveController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;    
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameControllerTrung.gameStarted == false)
        {
            return;
        }

        if (interctableObject != null && Input.GetKeyDown(KeyCode.E)) {
            objectiveController.StartInteraction(interctableObject);
        }

        // Rotate the player with the camera
        transform.rotation = Quaternion.Euler(0.0f, playerCam.transform.eulerAngles.y, 0.0f);
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerCollider.height * playerObj.localScale.y * 0.5f + 0.1f, whatIsGround);
        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        if (gameControllerTrung.gameStarted == false)
        {
            return;
        }
        if (!canMove)
            return;
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = playerCam.transform.forward * verticalInput + playerCam.transform.right * horizontalInput;
        moveDirection.y = 0.0f;
        // Debug.Log(moveDirection);

        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            flatVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(flatVel.x, rb.velocity.y, flatVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    
}
