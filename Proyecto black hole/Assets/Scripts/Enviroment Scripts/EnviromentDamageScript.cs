using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentDamageScript : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player")
            {
                other.GetComponent<HealthSystemScript>().TakeDamage(damage);
            }
        }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player")
        {
            other.GetComponent<HealthSystemScript>().TakeDamage(damage);
        }
    }

}
