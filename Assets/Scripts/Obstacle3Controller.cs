using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Obstacle3Controller : MonoBehaviour
{

    public PathFollower ballPathFollower;
    public Highlight carHighlight;

    private bool gazeEnableInteractions = false;

    private Vector3 initialPosition;
    private Vector3 maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        maxPosition = initialPosition + new Vector3(0, 0, 5.0f); ;
    }

    // Update is called once per frame
    void Update()
    {

        // Make sure that the next puzzles can't be solved beforehand.
        // If the second one isn't solved yet, do nothing for now.
        if (ballPathFollower.secondObstacleResolved == false)
            return;

        // Handle Controller inputs
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.


        if (gazeEnableInteractions)
        {
            if (gamepad.leftShoulder.IsPressed())
            {
                carHighlight.ToggleHighlight(true); // Keep it highlighted while moving

                if (transform.position.z <= maxPosition.z)
                {
                    transform.position += Vector3.forward * 5 * Time.deltaTime;
                }
            }
        }

        if (transform.position.z >= maxPosition.z)
        {
            carHighlight.ToggleHighlight(false);
            ballPathFollower.thirdObstacleResolved = true;
        }
    }


    public void OnPointerEnter()
    {
        // Make sure that the next puzzles can't be solved beforehand.
        // If the first one isn't solved yet, do nothing for now.
        if (ballPathFollower.secondObstacleResolved == false)
            return;

        gazeEnableInteractions = true;
        carHighlight.ToggleHighlight(true);
    }

    public void OnPointerExit()
    {
        gazeEnableInteractions = false;
        carHighlight.ToggleHighlight(false);
    }

}
