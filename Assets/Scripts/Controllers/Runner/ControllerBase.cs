using UnityEngine;

/// <summary>
/// This class provides a base for controlling a character's movement, ground checking, attacking, and gravity.
/// I've used this in conjunction with the PlayerController class to create a simple endless runner.
/// </summary>
public class ControllerBase : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected bool _canAccelerate = false;

    [Space]
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected float _maxSpeed = 15f;
    [SerializeField] protected float _acceleration = 3f;

    protected bool _moving = false;
    protected bool _canMove = true;

    [Space]
    [SerializeField] protected bool _facingRight = true;

    [Header("Ground check")]
    [SerializeField]
    /// <summary>
    /// The Transform used to check if the character is grounded.
    /// </summary>
    protected Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

    protected bool _grounded = false;
    private const float _groundCheckRadius = 0.25f;

    [Header("Attack properties")]
    [SerializeField] protected float _attackCooldown = 0.5f;
    protected bool _canAttack = true;

    [Header("Gravity")]
    [SerializeField] protected float _defaultGravity = 2.4f;
    [SerializeField] protected float _fallGravity = 3.6f;

    protected bool _dead, _falling = false;

    protected Vector2 _direction;
    protected Animator _anim;
    protected Rigidbody2D _rb;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (_dead) Dead();
        else
        {
            if (!_grounded)
            {
                if (_rb.velocity.y < 0.5) _falling = true;
            }

            if (_falling) _rb.gravityScale = _fallGravity;
            else _rb.gravityScale = _defaultGravity;

            if (_facingRight) _direction = Vector2.right;
            else _direction = Vector2.left;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!_groundCheck) return;

        _grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
    }

    public void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    /// <summary>
    /// Spawns a specified effect at the object's current position.
    /// For example a dust cloud when the character jumps.
    /// </summary>
    public void SpawnEffect(GameObject effect)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }

    public void Dead()
    {
        this.enabled = false;

        _rb.gravityScale = _defaultGravity;
        _rb.velocity = Vector2.zero;

        _anim.SetBool("Dead", true);
    }
}
