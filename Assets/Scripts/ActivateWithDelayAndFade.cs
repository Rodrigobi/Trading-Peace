using UnityEngine;
using UnityEngine.UI;

public class ActivateWithDelayAndFade : MonoBehaviour
{
    public GameObject[] elementsToActivate; // References to the GameObjects within the Canvas
    public Button buttonToDisable; // Reference to the Button you want to disable

    private void Start()
    {
        foreach (GameObject element in elementsToActivate)
        {
            element.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        // Disable the button after it's clicked
        buttonToDisable.interactable = false;

        // Start the coroutine to activate elements with a delay
        StartCoroutine(ActivateElementsWithDelay());
    }

    private System.Collections.IEnumerator ActivateElementsWithDelay()
    {
        float delayBetweenElements = 1.0f; // Delay in seconds between elements

        foreach (GameObject element in elementsToActivate)
        {
            // Activate the element
            element.SetActive(true);

            // Initialize the color with full transparency
            Color elementColor = element.GetComponent<Graphic>().color;
            elementColor.a = 0f;
            element.GetComponent<Graphic>().color = elementColor;

            // Gradually increase the alpha (fade-in) over a period of time
            while (elementColor.a < 1f)
            {
                elementColor.a += Time.deltaTime / delayBetweenElements;
                element.GetComponent<Graphic>().color = elementColor;
                yield return null;
            }

            // Wait for the specified delay before activating the next element
            yield return new WaitForSeconds(delayBetweenElements);
        }
    }
}
