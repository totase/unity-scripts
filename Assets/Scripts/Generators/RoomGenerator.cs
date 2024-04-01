using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for generating an endless level with rooms.
/// Source for original script: https://www.kodeco.com/5459-how-to-make-a-game-like-jetpack-joyride-in-unity-2d-part-2/page/2
/// </summary>
public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Transform _levelContainer;

    [Space]
    [SerializeField] private bool _activeEvent;

    [Header("Room objects")]
    [SerializeField] private List<GameObject> _currentRooms = new List<GameObject>();

    [Space]
    [SerializeField] private GameObject _room;
    [SerializeField] private GameObject _eventRoom;

    [Header("Room properties")]
    [SerializeField] private float _roomWidth;
    [SerializeField] private float _roomHeight;

    private float _screenWidthPoints;

    /// <summary>
    /// In Start we calculate the width of the screen in world points.
    /// </summary>
    void Start()
    {
        float _width = 2 * Camera.main.orthographicSize;
        _screenWidthPoints = _width * Camera.main.aspect;
    }

    void Update()
    {
        GenerateRoomIfRequired();
    }

    /// <summary>
    /// Adds a new room to the level at the specified x-coordinate.
    /// </summary>
    /// <param name="farthestRoomX">The x-coordinate where the new room will be added.</param>
    void AddRoom(float farthestRoomX)
    {
        GameObject _toInstantiate;

        // GameManager.instance.IncreaseRoomsCount();

        if (_activeEvent) _toInstantiate = Instantiate(_eventRoom);
        else _toInstantiate = Instantiate(_room);

        _toInstantiate.transform.SetParent(_levelContainer);

        float _roomCenter = farthestRoomX + (_roomWidth / 2);

        _toInstantiate.transform.position = new Vector3(_roomCenter, 0, 0);

        _currentRooms.Add(_toInstantiate);
    }

    void GenerateRoomIfRequired()
    {
        List<GameObject> _roomsToRemove = new List<GameObject>();

        bool _addRoom = true;

        float _playerX = transform.position.x;
        float _addRoomX = _playerX + _screenWidthPoints;
        float _removeRoomX = _playerX - (_screenWidthPoints * 10);

        float _farthestRoomEndX = 0;

        float _roomStartX, _roomEndX;
        foreach (var _room in _currentRooms)
        {
            _roomStartX = _room.transform.position.x - (_roomWidth / 2);
            _roomEndX = _roomStartX + _roomWidth;

            if (_roomStartX > _addRoomX) _addRoom = false;
            if (_roomEndX < _removeRoomX) _roomsToRemove.Add(_room);

            _farthestRoomEndX = Mathf.Max(_farthestRoomEndX, _roomEndX);
        }

        foreach (var _room in _roomsToRemove)
        {
            _currentRooms.Remove(_room);
        }

        if (_addRoom) AddRoom(_farthestRoomEndX);
    }
}
