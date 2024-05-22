using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DoorContoller : MonoBehaviour
{
    private bool _isOpen = false;
    private float doorZValue;
    public Transform door;
    [SerializeField] private float openPosition = 1.6f;
    [SerializeField] private float closePosition = 4.49f;
    [SerializeField] private float openCloseDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }
    private void OpenDoor()
    {
        if (!_isOpen)
        {
            _isOpen = true;
            door.DOLocalMoveZ(openPosition, openCloseDuration);
        }
    }
    private void CloseDoor()
    {
        if (_isOpen)
        {
            _isOpen = false;
            door.DOLocalMoveZ(closePosition, openCloseDuration);
        }
    }
}
