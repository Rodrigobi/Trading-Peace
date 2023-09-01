using UnityEngine;
using UnityEngine.UI;

public class ActivateElementAndDisableButton : MonoBehaviour
{
    public GameObject elementToActivate; // Reference to the GameObject within the Canvas
    public Button buttonToDisable; // Reference to the Button you want to disable

    private void Start()
    {
        // Assuming you want to start with the element deactivated
        elementToActivate.SetActive(false);
    }

    public void OnButtonClick()
    {
        // Activate the GameObject when the button is clicked
        elementToActivate.SetActive(true);

        // Deactivate the button's GameObject after it's clicked
        buttonToDisable.gameObject.SetActive(false);
    }
}
