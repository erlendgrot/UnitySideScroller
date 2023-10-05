
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body; //Creates a reference to the players body in the game
    [Header ("Player Movement")]
    [SerializeField] private float speed; //This can be edited in Unity. Speed of the player
    [SerializeField] private float jumpPower; 

    [Header ("Layers")]
    [SerializeField] private LayerMask groundLayer; // Using tags in the SerializeField
    [SerializeField] private LayerMask wallLayer; // Using tags in the SerializeField

    [Header ("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;
    
    private void Awake() //Awake is called when the script instance is being loaded
    {
        //Grab references for rigidbody, boxcollider and animator from gameobject
        body = GetComponent<Rigidbody2D>(); //To get references
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        
    }

    private void Update() //Runs on every frame in the game
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when moving left-right
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1); // This flips the player when moving in the oposite direction

        //transform.localscale to change the scale/flip the player

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0); // To start the run animation
        anim.SetBool("grounded", isGrounded()); // To set the idle animation

        // To move the player according to the input (MOVES THE PLAYER LEFT AND RIGHT)
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        body.gravityScale = 3;

        if(Input.GetKey(KeyCode.Space)) //Check if space is being pressed
            Jump();

            // To only play the jump sound once when the player is jumping
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            {
                SoundManager.instance.PlaySound(jumpSound);
            }
        else if (onWall() && !isGrounded() && horizontalInput != 0) {
                
            print("On wall");

            Vector2 wallDirection = new Vector2(-Mathf.Sign(transform.localScale.x), -1f).normalized;

            // Apply force to push the player away from the wall
            body.AddForce(wallDirection, ForceMode2D.Impulse);
        }

    }       

    private void Jump()
    {
        if (isGrounded())
        {
            // Makes the player jump when we press space
            
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump"); 
            
        } 

    }

    // Checks if the player is on the ground
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    // Checks if the player is on the wall
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    //To decide wether the player can attack or not
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
