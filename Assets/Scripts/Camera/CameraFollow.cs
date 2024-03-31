using UnityEngine;

/// <summary>
/// This class is responsible for making a camera follow a target object.
/// Attach this script to the camera object, and set the target object (i.e. the player) in the inspector.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _target;

    [Space]
    [SerializeField] private float _smoothSpeed = 10;

    [Header("Position offsets")]
    [SerializeField] private float _offsetX = 3f;
    [SerializeField] private float _offsetY = 0f;

    [Space]
    [SerializeField] private float _targetZ = -10f;

    private Vector3 _targetPosition, _smoothedPosition;
    private Vector3 _velocity = Vector3.zero;

    /// <summary>
    /// In each fixed framerate frame, calculate the new position of the camera and move it there.
    /// </summary>
    void FixedUpdate()
    {
        _targetPosition = new Vector3(_target.position.x + _offsetX, transform.position.y + _offsetY, _targetZ);
        _smoothedPosition = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, _smoothSpeed * Time.deltaTime);

        transform.position = _smoothedPosition;
    }
}
