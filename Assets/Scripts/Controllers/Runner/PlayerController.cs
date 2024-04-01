using UnityEngine;

/// <summary>
/// This class provides methods for controlling a player character, including movement, jumping, and interactions with collider objects.
/// </summary>
public class PlayerController : ControllerBase
{
    [Header("Jump properties")]
    [SerializeField] private float _jumpForce = 5f;
    private bool _canJump = true;

    [Header("Debug properties")]
    /// <summary>
    /// If true, the player will automatically move without player input.
    /// Typically preferred for endless runners.
    /// </summary>
    public bool autoMove = false;

    protected override void Update()
    {
        base.Update();

        _anim.SetBool("Running", _canMove && _moving);
        _anim.SetBool("Falling", _falling);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && _canJump && _grounded) AddForce(_jumpForce);

        if (_grounded)
        {
            if (!_canJump) _canJump = true;
            if (_falling) _falling = false;
        }

        if (_canMove)
        {
            if (!autoMove)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    _moving = true;

                    if (_facingRight) Flip();
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    _moving = true;

                    if (!_facingRight) Flip();
                }
                else
                {
                    _moving = false;
                }
            }
            else
            {
                _moving = true;
            }
        }
        else _moving = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_moving) Move();
    }

    void Move()
    {
        if (_moveSpeed <= _maxSpeed && _canAccelerate) _moveSpeed += _acceleration * Time.deltaTime;

        _rb.velocity = new Vector2(_direction.x * _moveSpeed, _rb.velocity.y);
    }

    public void AddForce(float force)
    {
        _canJump = false;
        _anim.SetTrigger("Jump");

        _rb.velocity = new Vector2(_rb.velocity.x, force);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy" || collider.tag == "Spikes")
        {
            base.Dead();
        }
    }
}
