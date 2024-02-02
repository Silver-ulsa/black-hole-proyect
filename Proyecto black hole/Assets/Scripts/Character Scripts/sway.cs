using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sway : MonoBehaviour
{
    private Quaternion originRotation;

    // Start is called before the first frame update
    void Start()
    {
        originRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        updateSway();
    }

    private void updateSway()
    {
        //Controls
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        //Calculate target rotation
        Quaternion xAdjustment = Quaternion.AngleAxis(-xMouse * 2.5f, Vector3.up);
        Quaternion yAdjustment = Quaternion.AngleAxis(yMouse * 2.5f, Vector3.right);
        Quaternion targetRotation = originRotation * xAdjustment * yAdjustment;

        //Rotate towards target rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 10f);
    }
}
