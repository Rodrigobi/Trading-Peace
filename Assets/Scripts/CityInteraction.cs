using UnityEngine;
using UnityEngine.UI;

public class CityInteraction : MonoBehaviour
{
    public GameObject cityButton; // Reference to the UI button for entering the city
    public Transform playerPlane; // Reference to the player's plane Transform

    public float activationDistance = 5f; // Distance threshold for button activation

    private void Start()
    {
        cityButton.SetActive(false); // Initially hide the button
    }

    private void Update()
    {
        foreach (Transform cityTransform in CityManager.Instance.cityTransforms)
        {
            float distanceToCity = Vector3.Distance(playerPlane.position, cityTransform.position);

            if (distanceToCity <= activationDistance)
            {
                cityButton.SetActive(true); // Show the button when player is close to a city
                return; // No need to check other cities
            }
        }

        // If the loop completes without activating the button, hide it
        cityButton.SetActive(false);
    }
}
