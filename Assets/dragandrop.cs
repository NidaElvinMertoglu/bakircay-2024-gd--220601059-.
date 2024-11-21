using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draganddrop : MonoBehaviour
{
    Vector3 mousePosition;

    
    public Vector3 minBoundary = new Vector3(-5f, -5f, -5f); 
    public Vector3 maxBoundary = new Vector3(5f, 5f, 5f);    

    private Vector3 GetMousePot()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePot();
    }

    private void OnMouseDrag()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);

        -
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBoundary.x, maxBoundary.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBoundary.y, maxBoundary.y);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minBoundary.z, maxBoundary.z);

        transform.position = targetPosition;
    }
}
