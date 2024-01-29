using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public delegate void ChangeEvent();
    public static event ChangeEvent cubeLandedEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if(cubeLandedEvent!=null)
            cubeLandedEvent();
    }
}
