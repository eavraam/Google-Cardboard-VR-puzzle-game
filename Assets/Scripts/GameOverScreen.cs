using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{

    public Image blackScreen; // Reference to your black screen UI element
    public float fadeSpeed; // Adjust the speed of the fade-in
    public float distanceFromCamera;

    //public Camera _MainCamera; // Enable for Game Over Screen - World Space 

    void Start()
    {
        // Enable 2 following for Game Over Screen - World Space 
        //transform.position = _MainCamera.transform.position + _MainCamera.transform.forward * distanceFromCamera;
        //transform.rotation = _MainCamera.transform.rotation;
        blackScreen.canvasRenderer.SetAlpha(0.0f); // Make sure it starts invisible
        StartCoroutine(FadeIn());
    }


    IEnumerator FadeIn()
    {
        blackScreen.CrossFadeAlpha(1.0f, fadeSpeed, false); // Fades from 0 to 1 over fadeSpeed seconds

        yield return new WaitForSeconds(fadeSpeed);

        yield return null;
    }

}
