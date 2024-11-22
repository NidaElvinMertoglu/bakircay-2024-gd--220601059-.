using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launch : MonoBehaviour
{

    public GameObject selectedObject;
    public float launchForce = 10f;
    public Transform launchDirection;

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("nesne"))
        {

            if (selectedObject == null)
            {
                Debug.Log("Nesne kutuya girdi: " + other.gameObject.name);
                selectedObject = other.gameObject;


            }
            else
            {

                Debug.Log("Fazla nesne tespit edildi: " + other.gameObject.name);


                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = other.gameObject.AddComponent<Rigidbody>();
                }


                Vector3 direction = (launchDirection != null) ? launchDirection.right : Vector3.right;
                rb.AddForce(direction * launchForce, ForceMode.Impulse);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject == selectedObject)
        {
            Debug.Log("Nesne kutudan çýktý: " + other.gameObject.name);




            selectedObject = null;
        }
    }






}
