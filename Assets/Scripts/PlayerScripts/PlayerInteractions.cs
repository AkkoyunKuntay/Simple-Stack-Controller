using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            StackController.Instance.PickUpItem(other.transform);            
        }

        if (other.CompareTag("Obstacle"))
        {
            PlayerController.Instance.forwardSpeed = 0;
            PlayerController.Instance.horizontalSpeed = 0;
            Debug.Log("You Failed!");
        }
    }
}
