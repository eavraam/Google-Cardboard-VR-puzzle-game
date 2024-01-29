using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSharing : MonoBehaviour
{

    public Renderer _Arrow;
    public Renderer _Circle;


    // Update is called once per frame
    void Update()
    {
        ShareMaterial();
    }

    private void ShareMaterial()
    {
        _Circle.material = _Arrow.material;
    }
}
