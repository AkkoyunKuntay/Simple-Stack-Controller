using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(transform.CompareTag("CollectedObject") && other.CompareTag("Collectible"))
        {
            StackController.Instance.PickUpItem(other.transform);
        }

        if (transform.CompareTag("CollectedObject") && other.CompareTag("Obstacle"))
        {
            StackController.Instance.RemoveLastObject(gameObject.transform);
            StartCoroutine(DestroyCube());
        }
    }

    public IEnumerator DestroyCube()
    {
        //CubeExpoFX.transform.parent = null;
        //CubeExpoFX.SetActive(true);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
