using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Ýtem Name")]
    [SerializeField] GameObject ItemNameObject;
    [SerializeField] Text ItemNameText;
    private void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out InteractionRaycast, InteractionDistance))
        {
            if (InteractionRaycast.transform.GetComponent<IInteractable>() != null)
            {
                DynamicCrosshair.Instance.Avaiable = false;
                ItemNameObject.SetActive(true);
                ItemNameText.text = InteractionRaycast.transform.GetComponent<IInteractable>().Name;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InteractionRaycast.transform.GetComponent<IInteractable>().Interact();
                }
               
            }
            else
            {
                DynamicCrosshair.Instance.Avaiable = true;
                ItemNameObject.SetActive(false);
            }
        }
        else
        {
            DynamicCrosshair.Instance.Avaiable = true;
            ItemNameObject.SetActive(false);
        }
    }
}
