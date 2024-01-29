using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondObstacleController : MonoBehaviour
{
    public GameObject initialWaterBlock;
    public GameObject finalWaterBlock;
    public GameObject initialPlatform;
    public GameObject finalPlatform;

    public GameObject ball;

    public GameObject cubeToSpawn;
    public GameObject spawner;

    private Vector3 delta = new Vector3(0.0f, 0.005f, 0.0f);
    private bool movePlatform = false;

    public delegate void ChangeEvent();
    public static event ChangeEvent platformUpEvent;
    // Start is called before the first frame update
    void Start()
    {
        CubeController.cubeLandedEvent += MovePlatforms;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(cubeToSpawn, spawner.transform.position, Quaternion.identity);
        }   

        if(movePlatform)
        {
            if(initialWaterBlock.transform.localScale.y > 1.0)
            {
                initialWaterBlock.transform.localScale -= delta;
                initialWaterBlock.transform.position -= delta;
                initialPlatform.transform.position -= delta;
            }

            if(finalPlatform.transform.position.y < 5)
            {
                finalWaterBlock.transform.localScale += delta;
                finalWaterBlock.transform.position += delta;
                finalPlatform.transform.position += delta;

                ball.transform.position += delta;
            }

            if(finalPlatform.transform.position.y >= 5){
                movePlatform = false;
                if(platformUpEvent!=null)
                    platformUpEvent();
            }
        }  
    }

    void MovePlatforms()
    {
        movePlatform = true;
    }
}
