using UnityEngine;

/// <summary>
/// This class provides methods for controlling a pickup object, including floating towards a target and being collected by a player.
/// Used in a 2D platformer to create collectible coins.
/// </summary>
public class Pickup : MonoBehaviour
{
    [Header("Pickup properties")]
    /// <summary>
    /// Determines if the pickup should float towards the target.
    /// </summary>
    [SerializeField] private bool _floatToTarget = true;
    [SerializeField] private float _floatSpeed = 1f;
    /// <summary>
    /// The radius within which the pickup will float to the target.
    /// </summary>
    [SerializeField] private float _collectRadius = 0.5f;

    [Space]
    [SerializeField] private LayerMask _whatIsTarget;

    [Header("Pickup value")]
    [SerializeField] private int score = 1;

    private bool _collected, _shouldFloatToTarget = false;

    private Animator _anim;
    private GameObject _target;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_collected) return;

        if (_shouldFloatToTarget && _floatToTarget) transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _floatSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Here, we check if the pickup should float towards a target.
    /// If the target is "spotted" within the radius, the pickup will float towards it.
    /// </summary>
    void FixedUpdate()
    {
        if (_shouldFloatToTarget) return;
        _shouldFloatToTarget = false;

        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(transform.position, _collectRadius, _whatIsTarget);
        for (int i = 0; i < groundColliders.Length; i++)
        {
            if (groundColliders[i].gameObject != gameObject)
            {
                _target = groundColliders[i].gameObject;
                _shouldFloatToTarget = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (_collected) return;

        if (collider.CompareTag("Player"))
        {
            _collected = true;
            // ScoreManager.instance.IncreaseScoreCount(score);

            _anim.SetTrigger("Collected");

            Destroy(gameObject, 0.5f);
        }
    }
}