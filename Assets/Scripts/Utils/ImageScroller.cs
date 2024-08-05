using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class is responsible for scrolling a texture on a RawImage component.
/// Make sure the image is set to "Repeat" in the texture import settings.
/// </summary>
public class ImageScroller : MonoBehaviour
{
    [Header("Scrolling properties")]
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;

    private RawImage _image;

    void Start()
    {
        _image = GetComponent<RawImage>();
    }

    void Update()
    {
        // Move the texture based on the scrolling speed values
        _image.uvRect = new Rect(_image.uvRect.position + new Vector2(_horizontalSpeed, _verticalSpeed) * Time.deltaTime, _image.uvRect.size);
    }
}