using System;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool IsPickedUp { get; private set; } = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IsPickedUp = true;
            
        }
    }
}
