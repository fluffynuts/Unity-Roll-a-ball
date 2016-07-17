using UnityEngine;
using System;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 _offset;
    private Transform _playerTransform;
    private Vector3 _tracker;

    // Use this for initialization
    void Start()
    {
        _offset = transform.position - Player.transform.position;
        _playerTransform = Player.transform;
        _tracker = new Vector3(_playerTransform.position.x, _playerTransform.position.y, _playerTransform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // transform.position = _playerTransform.position + _offset;
        var lastTracker = _tracker;
        _tracker = _playerTransform.position + _offset;
        var xDelta = Math.Abs(transform.position.x - _tracker.x);
        var zDelta = Math.Abs(transform.position.z - _tracker.z);
        if (xDelta > 1 || zDelta > 1)
        {
            var trackerDelta = (_tracker - lastTracker);
            transform.position += trackerDelta;
        }
    }
}
