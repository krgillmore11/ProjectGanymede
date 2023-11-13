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
    public Camera cameraObject;
    

    [SerializeField] float baseMoveSpeed = 5f;
    [SerializeField] float jumpHeight = 7f;
    [SerializeField] float sprintMultiplier = 1.5f;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float deceleration = 2f;
    [SerializeField] float maxSpeed = 1f;
    [SerializeField] float interactDistance = 3f;

    void Awake(){//Awake is good for setting up references
        rb = GetComponent<Rigidbody>();
        input = new PlayerInput();
        //playerCam = transform.Find("Player Camera"); 
        Cursor.lockState = CursorLockMode.Locked;//Lock cursor off screen
        input.Player.Sprint.performed += SprintToggle; 
    }

    void FixedUpdate(){  //Use for physics and rb.  dont waste two days ficking with your code agiain
        CameraRotation();    

        //Debug.Log(rb.velocity);
        
        //Get movement input
        Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();

        //set move direction to camera
        Vector3 moveDirection = (transform.forward * moveInput.y) + (transform.right * moveInput.x);
        moveDirection.y = 0f; //disables vertical movement when not jumping, do I need this?

        //apply movement forces
        //sprinting
        float currentMoveSpeed;
        if (sprinting){
            currentMoveSpeed = baseMoveSpeed * sprintMultiplier;
        }
        else{
            currentMoveSpeed = baseMoveSpeed;
        }
        Vector3 horizontalForce = moveDirection.normalized * currentMoveSpeed;

        //Calculate velocity change to max
        Vector3 velocityChange = (horizontalForce - rb.velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -currentMoveSpeed, currentMoveSpeed);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -currentMoveSpeed, currentMoveSpeed);
        velocityChange.y = 0f;

        //use addforce so it works w gravity
        //Apply force for movement only if there is input
        if (moveDirection.magnitude > .1f ){
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
        } 
        else{
        //apply stoppping force to avoid ice rink
        Vector3 oppositeVelocity = -rb.velocity;
        oppositeVelocity.y = 0f; //Ignore vertical velocity
        rb.AddForce(oppositeVelocity * currentMoveSpeed * deceleration, ForceMode.Acceleration);
        }

        //jump
        if(input.Player.Jump.triggered && grounded){
            rb.AddForce(Vector3.up * jumpHeight / Time.fixedDeltaTime, ForceMode.Impulse);
        }

        //exceeding speed along the horizontal plane
        /*
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float exceedingSpeed = horizontalVelocity.magnitude - maxSpeed;

        //if the character is moving too fast, apply a counteracting force
        if (exceedingSpeed > 0f){
        Vector3 counteractingForce = horizontalVelocity.normalized * exceedingSpeed;
        rb.AddForce(counteractingForce, ForceMode.Acceleration);
        }
        */
    }

    void Update(){
        //interaction
        if(input.Player.Interact.triggered){
            Interact();
        }

        if(input.Player.Attack.triggered){
            Attack();
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
        Debug.Log("interaction completed");     
        Ray ray = new Ray(cameraObject.transform.position, cameraObject.transform.forward);//ray shooting directly from camera
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance)){
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if(interactable != null){
                interactable.Interact();
            }
        }
    }

    private void Attack(){
        
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
