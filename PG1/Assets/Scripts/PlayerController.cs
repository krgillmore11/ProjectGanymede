using System;
using UnityEngine;
using UnityEngine.InputSystem;
//Handles all ingame Movment for player character

public class PlayerController : MonoBehaviour{
    private PlayerInput input;
    private Rigidbody rb;
    
    private bool sprinting;
    private bool grounded;
    private Transform playerCam;
    private float rotationX = 0f;
    private Vector2 lookInput;

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpHeight = 7f;
    [SerializeField] float sprintMultiplier = 1.5f;
    [SerializeField] float mouseSensitivity = 2f;

    void Awake(){//Awake is good for setting up references
        rb = GetComponent<Rigidbody>();
        input = new PlayerInput();
        playerCam = transform.Find("Player Camera"); 
        //Cursor.lockState = CursorLockMode.Locked;//Lock cursor off screen
        input.Player.Sprint.performed += SprintToggle; 
    }

    // Update is called once per frame
    void FixedUpdate(){  //Use for physics and rb.  dont waste two days ficking with your code oagaubn
        CameraRotation();    
          
        //Get movement
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();
        Debug.Log("Move Input: " + moveInput);

        Vector3 moveDirection = (playerCam.forward * moveInput.y) + (playerCam.right * moveInput.x);
        moveDirection.y = 0f; //disables vertical movment when not jumping, do I need this?
        Debug.DrawRay(transform.position, moveDirection.normalized * 2f, Color.green);


        //apply movement
        float currentMoveSpeed;
        if (sprinting){
            currentMoveSpeed = movementSpeed * sprintMultiplier;
        }
        else{
            currentMoveSpeed = movementSpeed;
        }
        rb.velocity = moveDirection.normalized * currentMoveSpeed;

        //jump
        if(input.Player.Jump.triggered && grounded){
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

        Debug.Log("Grounded: " + grounded);
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
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        Debug.Log("Mouse X: " + mouseX + " | Mouse Y: " + mouseY);

        //apply it
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        playerCam.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up, mouseX);
    }

    private void SprintToggle(InputAction.CallbackContext context){
        if (context.performed){
            sprinting = !sprinting;
            Debug.Log("Sprinting: " + sprinting);
        }
    }

    private void Interact(){
        //interact stuff
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
