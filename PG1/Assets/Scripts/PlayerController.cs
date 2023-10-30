using System;
using UnityEngine;
using UnityEngine.InputSystem;
//Handles all ingame Movment for player character
//ADD SOUNDS AND CHANGE HEARING DISTANCE FOR ENEMY

public class PlayerController : MonoBehaviour{
    private PlayerInput input;
    private Rigidbody rb;
    private bool sprinting;
    private bool grounded;
    public Transform playerCam;
    private float rotationX = 0f;
    private Vector2 lookInput;
    [SerializeField] Transform arms;

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpHeight = 7f;
    [SerializeField] float sprintMultiplier = 1.5f;
    [SerializeField] float mouseSensitivity = 2f;

    void Awake(){//Awake is good for setting up references
        rb = GetComponent<Rigidbody>();
        input = new PlayerInput();
        //playerCam = transform.Find("Player Camera"); 
        Cursor.lockState = CursorLockMode.Locked;//Lock cursor off screen
        input.Player.Sprint.performed += SprintToggle; 
    }

    void FixedUpdate(){  //Use for physics and rb.  dont waste two days ficking with your code agiain
        CameraRotation();    
        

        //Get movement input
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();

        //set move direction to camera
        Vector3 moveDirection = (playerCam.forward * moveInput.y) + (playerCam.right * moveInput.x);
        moveDirection.y = 0f; //disables vertical movement when not jumping, do I need this?

        //apply movement forces
        //sprinting
        float currentMoveSpeed;
        if (sprinting){
            currentMoveSpeed = movementSpeed * sprintMultiplier;
        }
        else{
            currentMoveSpeed = movementSpeed;
        }

        //use addforce so it works w gravity
        //Apply force for movement only if there is input
        Vector3 horizontalForce = moveDirection.normalized * currentMoveSpeed;
        if (moveDirection.magnitude > .1f){
        rb.AddForce(horizontalForce, ForceMode.Acceleration);
        } 
        else{
        //apply stoppping force to avoid ice rink
        Vector3 oppositeVelocity = -rb.velocity;
        oppositeVelocity.y = 0f; //Ignore vertical velocity
        rb.AddForce(oppositeVelocity.normalized * currentMoveSpeed * 2f, ForceMode.Acceleration);
        }

        //jump
        if(input.Player.Jump.triggered && grounded){
            rb.AddForce(Vector3.up * jumpHeight / Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    void Update(){
        //interaction
        if(input.Player.Interact.triggered){
            Interact();
        }
    }

    private void CameraRotation(){
        //get rotation from cam
        lookInput = input.Player.Look.ReadValue<Vector2>();
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        //apply it
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        playerCam.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        arms.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up, mouseX);
    }

    private void SprintToggle(InputAction.CallbackContext context){
        if (context.performed){
            sprinting = !sprinting;
        }
    }

    private void Interact(){
        //interact stuff
        Debug.Log("interaction completed");
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Ground")){
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision){
        if (collision.gameObject.CompareTag("Ground")){
            grounded = false;
        }
    }

    private void OnEnable(){
        input.Enable();
    }

    private void OnDisable(){
        input.Disable();
    }
}
