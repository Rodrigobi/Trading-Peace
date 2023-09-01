using UnityEngine;
using UnityEngine.UI;

public class DeactivateElementOnButtonClick : MonoBehaviour
{
    public GameObject elementToDeactivate; // Reference to the GameObject within the Canvas

    public void OnButtonClick()
    {
        // Deactivate the GameObject when the button is clicked
        elementToDeactivate.SetActive(false);
    }
}
