using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAcquisition : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("FishGet");
        }
    }
}
