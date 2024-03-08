using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string Name { get; set; }

    void Interact();
}