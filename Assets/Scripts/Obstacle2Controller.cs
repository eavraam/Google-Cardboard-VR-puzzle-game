using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Obstacle2Controller : MonoBehaviour
{

    public GameObject leverBase;
    public GameObject lever;
    public PathFollower ballPathFollower;
    public SecondObstacleController secondObstacleController;
    private bool isInstantiated;

    public Highlight highlightLeverMechanism;
    private bool gazeEnableInteractions = false;

    private Renderer baseRenderer;

    private Vector3 initialRotation;
    private Vector3 maxRotation;

    // Start is called before the first frame update
    void Start()
    {
        baseRenderer = leverBase.GetComponent<Renderer>(); // Get 1st child's Renderer (item named: "Base")

        initialRotation = lever.transform.rotation.eulerAngles;
        maxRotation = new Vector3(0, 0, 260.0f);    // Hard coded value for maximum rotation angle
        isInstantiated = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure that the next puzzles can't be solved beforehand.
        // If the first one isn't solved yet, do nothing for now.
        if (ballPathFollower.firstObstacleResolved == false)
            return;

        // Handle Controller inputs
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gazeEnableInteractions)
        {
            if (gamepad.leftShoulder.IsPressed())
            //if (Input.GetKeyDown(KeyCode.Space))
            {
                highlightLeverMechanism.ToggleHighlight(true);  // Keep it highlighted while rotating

                if (lever.transform.rotation.eulerAngles.z >= maxRotation.z)
                {
                    // Rotate by 60 degrees on the z-axis
                    lever.transform.Rotate( Vector3.back * 60 * Time.deltaTime);
                }
            }
        }
       

        if (lever.transform.rotation.eulerAngles.z <= maxRotation.z)
        {
            if (isInstantiated == false)
            {
                // Create a box
                Instantiate(secondObstacleController.cubeToSpawn,
                    secondObstacleController.spawner.transform.position,
                    Quaternion.identity);
                
                // Disable further instantiations
                isInstantiated = true;

                highlightLeverMechanism.ToggleHighlight(false);
            }
        }
    }


    public void OnPointerEnter()
    {
        // Make sure that the next puzzles can't be solved beforehand.
        // If the first one isn't solved yet, do nothing for now.
        if (ballPathFollower.firstObstacleResolved == false)
            return;

        gazeEnableInteractions = true;
        highlightLeverMechanism.ToggleHighlight(true);
    }

    public void OnPointerExit()
    {
        gazeEnableInteractions = false;
        highlightLeverMechanism.ToggleHighlight(false);
    }
}
