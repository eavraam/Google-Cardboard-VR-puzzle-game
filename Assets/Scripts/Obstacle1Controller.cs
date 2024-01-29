using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Obstacle1Controller : MonoBehaviour
{
    public GameObject paddles;  // Reference to the "Paddles" GameObject
    public PathFollower ballPathFollower;

    public Highlight highlight;
    private bool gazeEnableInteractions = false;

    private Renderer paddle1Renderer;
    private Renderer paddle2Renderer;

    private Vector3 initialRotation;
    private Vector3 maxRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the reference to the "Paddles" GameObject
        //paddles = transform.Find("Paddles").gameObject;
        initialRotation = paddles.transform.rotation.eulerAngles;
        maxRotation = initialRotation + new Vector3(0, 0, -40.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure that the next puzzles can't be solved beforehand.
        if (ballPathFollower.firstObstacleResolved ||
            ballPathFollower.secondObstacleResolved ||
            ballPathFollower.thirdObstacleResolved ||
            ballPathFollower.fourthObstacleResolved
            )
            return;

        // Handle Controller inputs
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        // Vector2 move gamepad.leftStick.ReadValue(); // get the stick values

        if (gazeEnableInteractions)
        {
            if (gamepad.leftShoulder.IsPressed())
            {
                highlight.ToggleHighlight(true);  // Keep it highlighted while rotating

                if (paddles.transform.rotation.eulerAngles.z >= maxRotation.z)
                {
                    // Rotate by 60 degrees on the z-axis
                    // !! Only works because I go from 55 to 5 degrees. Then, it's not negative, but goes around a 
                    // POSITIVE 360 rotation.
                    paddles.transform.Rotate(Vector3.back * 60 * Time.deltaTime);
                }
            }

        }


        if (paddles.transform.rotation.eulerAngles.z <= maxRotation.z)
        {
            ballPathFollower.firstObstacleResolved = true;
            highlight.ToggleHighlight(false);
        }

    }

    public void OnPointerEnter()
    {
        gazeEnableInteractions = true;
        highlight.ToggleHighlight(true);
    }

    public void OnPointerExit()
    {
        gazeEnableInteractions = false;
        highlight.ToggleHighlight(false);
    }
}