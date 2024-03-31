using UnityEngine;

/// <summary>
/// This class is responsible for creating a parallax effect by moving the GameObject it's attached to at a different speed than the camera.
/// </summary>
public class Parallax : MonoBehaviour
{
    [SerializeField]
    /// <summary>
    /// The speed at which the GameObject will move relative to the camera to create the parallax effect.
    /// </summary>
    private float _parallaxSpeed = 0.05f;

    private Transform _camera;
    private float _startPos;

    void Awake()
    {
        _camera = Camera.main.transform;
    }

    void Start()
    {
        _startPos = transform.position.x;
    }

    void Update()
    {
        float _dist = _camera.transform.position.x * _parallaxSpeed;

        transform.position = new Vector3(_startPos + _dist, transform.position.y, transform.position.z);
    }
}
