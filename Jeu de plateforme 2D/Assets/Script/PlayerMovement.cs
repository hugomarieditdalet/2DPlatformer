using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed ;
    public float climbSpeed ;
    public float jumpForce ; 

    private bool isJumping ;
    private bool isGrounded;
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private float verticalMovement;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la sc�ne ");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            isJumping = true;
        }

        Flip(rb.linearVelocity.x);

        float characterVelocity = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing",isClimbing);

    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,collisionLayers);
        MovePlayer(horizontalMovement, verticalMovement);
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.linearVelocity.y);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);

            if(isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping=false;
            }
        }
        else 
        {
            //déplacement verticale
            Vector3 targetVelocity = new Vector2(0,_verticalMovement);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);

        }
    }

    void Flip(float _velocity)
    {
        if (_velocity>0.1f)
        {
            spriteRenderer.flipX = false;
        }else if (_velocity <-0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
