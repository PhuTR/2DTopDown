using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingleft; }}
    public static PlayerController Instance;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float dashSpeed = 4.0f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    private float startingMoveSpeed;
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimatior; 
    private SpriteRenderer mySpriteRenderer;
    private bool facingleft = false;
    private bool isDashing = false;
    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimatior = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimatior.SetFloat("moveX", movement.x);
        myAnimatior.SetFloat("moveY", movement.y);
    }
    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if(mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingleft = true;
        }
        else
        {
            mySpriteRenderer.flipX= false;
            facingleft = false;
        }
    }
    private void Dash()
    {
        if(!isDashing)
        {
            isDashing = true;
            moveSpeed*= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }
    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
