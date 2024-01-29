using System.Collections.Generic;
using UnityEngine;

public class TeleportationManager : MonoBehaviour
{
    public GameObject currentPlayer;

    // Assign all the navigation markers here through the inspector
    [SerializeField]
    private List<NavigationMarkerController> navigationMarkers;

    private void Awake()
    {
        // Cache the navigation markers
        foreach (var marker in navigationMarkers)
        {
            marker.gameObject.SetActive(true); // Ensure all markers are initially active
        }
    }

    public void TeleportToMarker(GameObject marker)
    {
        Vector3 destination = new Vector3(marker.transform.position.x, 3.0f, marker.transform.position.z);

        if (currentPlayer != null)
        {
            currentPlayer.transform.position = destination;
        }

        DisableCurrentMarker(marker);
        EnableAllMarkers();
    }

    private void DisableCurrentMarker(GameObject currentMarker)
    {
        currentMarker.SetActive(false);
    }

    private void EnableAllMarkers()
    {
        foreach (var marker in navigationMarkers)
        {
            marker.gameObject.SetActive(true);
            Debug.Log("Marker enabled: " + marker.name);
        }
    }
}
