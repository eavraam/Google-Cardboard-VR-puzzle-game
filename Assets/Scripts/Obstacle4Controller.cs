using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Obstacle4Controller : MonoBehaviour
{

    public PathFollower ballPathFollower;
    public Highlight ballHighlight;

    private bool gazeEnableInteractions = false;

    private Vector3 initialScale;
    private Vector3 minScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        minScale = transform.localScale * 0.4f;
    }

    // Update is called once per frame
    void Update()
    {

        // Make sure that the next puzzles can't be solved beforehand.
        // If the third one isn't solved yet, do nothing for now.
        if (ballPathFollower.thirdObstacleResolved == false)
            return;

        // Handle Controller inputs
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gazeEnableInteractions)
        {
            if (gamepad.leftShoulder.IsPressed())
            {
                ballHighlight.ToggleHighlight(true); // Keep it highlighted while scaling

                if (transform.localScale.magnitude >= minScale.magnitude)
                {
                    transform.localScale -= new Vector3(1, 1, 1) * 0.5f * Time.deltaTime;
                    transform.position -= new Vector3(0, 1, 0) * 0.25f * Time.deltaTime;
                }
            }
        }


        if (transform.localScale.magnitude <= minScale.magnitude)
        {
            ballHighlight.ToggleHighlight(false);
            ballPathFollower.fourthObstacleResolved = true;
            transform.position = new Vector3(transform.position.x, 0.38f, transform.position.z);
        }

    }

    public void OnPointerEnter()
    {
        // Make sure that the next puzzles can't be solved beforehand.
        // If the first one isn't solved yet, do nothing for now.
        if (ballPathFollower.secondObstacleResolved == false)
            return;

        gazeEnableInteractions = true;
        ballHighlight.ToggleHighlight(true);
    }

    public void OnPointerExit()
    {
        gazeEnableInteractions = false;
        ballHighlight.ToggleHighlight(false);
    }
}
