using UnityEngine;

public class ObjectVisibility : MonoBehaviour
{
    public GameObject objectToShow; // Reference to the GameObject you want to display

    private bool isVisible = false; // Initial state is invisible

    private void Start()
    {
        // Make sure the object is initially deactivated
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleObjectVisibility();
        }
    }

    private void SetObjectVisibility(bool isVisible)
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(isVisible);
        }
    }

    private void ToggleObjectVisibility()
    {
        isVisible = !isVisible;
        SetObjectVisibility(isVisible);
    }
}

