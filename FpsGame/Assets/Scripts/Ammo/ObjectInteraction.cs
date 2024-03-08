using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public static ObjectInteraction Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] Transform Camera;
    [SerializeField] float InteractionDistance;

    RaycastHit InteractionRaycast;
    private void Update()
    {
        if (Physics.Raycast(Camera.position,Camera.forward, out InteractionRaycast,InteractionDistance))
        {
            if (InteractionRaycast.transform.GetComponent<IInteractable>() != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InteractionRaycast.transform.GetComponent<IInteractable>().Interact();
                }
            }
        }
    }
}
