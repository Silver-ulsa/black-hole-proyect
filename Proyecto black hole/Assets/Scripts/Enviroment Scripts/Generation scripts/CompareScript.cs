using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("CompareTag"))
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.up, Color.green);
        }
    }
}