using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-1.9f, 4.38f), 5f, Random.Range(-4.5f, 4.38f));
    }

}
