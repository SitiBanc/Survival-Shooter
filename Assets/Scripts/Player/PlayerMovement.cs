using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;

    private Vector3 movement;           // Directon of player's movement
    private Rigidbody rb;               // Hold reference to player's rigidbody
    private int floorMask;              // A layer mask so that a ray can be cast just at gameobjects on the floor layer
    private float camRayLength = 100;   // The max distance the ray should check for collisions
    private Animator anim;              // Hold reference to the attached animator component

    // Awake() is called upon loading this script whether or not the script is enabled
    void Awake() {
        // Set up reference
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        // Create layer mask for Floor Layer
        floorMask = LayerMask.GetMask("Floor");
    }

    void FixedUpdate() {
        // Get input axis
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        // Move the player
        Move(moveHorizontal, moveVertical);
        // Turn the player to face the mouse cursor
        Turning();
        // Animate the player
        Animating(moveHorizontal, moveVertical);
    }

    void Move(float h, float v) {
        // Set the movement vector based on the input value
        movement.Set(h, 0.0f, v);
        // Normalize the movement vector and make it proportional to the speed per second
        movement = movement.normalized * speed * Time.deltaTime;
        // Move the player to the current position plus the movement
        rb.MovePosition(transform.position + movement);
    }

    void Turning() {
        // Create a ray from the mouse cursor on screen in the direction of the camera
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Store information about what was hit by the ray
        RaycastHit floorHit;
        // Perform the raycast and if it hit somthing on the floor layer then turn player to face that diraction
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            // The vector from player to the point our raycast hit (on the floor)
            Vector3 playerToMouse = floorHit.point - transform.position;
            // Ensure the vector is on the floor plane
            playerToMouse.y = 0.0f;
            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            // Turn our player to the new rotation (direction)
            rb.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v) {
        // If either h or v has non-zero value, then the player is walking (set 'walking' variable to true)
        bool walking = (h != 0.0f || v != 0.0f);
        // Tell the animator whether the player IsWalking
        anim.SetBool("IsWalking", walking);
    }

}
