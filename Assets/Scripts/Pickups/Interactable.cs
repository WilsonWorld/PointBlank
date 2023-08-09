/* Created by Wilson World Games, August 2022 */
/* The Interactable class is an abstract class that is inherited from by all interactables. This is responsible for controlling the Interactable UI display and text when the player is looking at the object and is within the pickup range. */

using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float Distance;
    public float DistanceMax = 6.0f;
    public GameObject ActionDisplay;
    public GameObject ActionText;

    protected virtual void Update()
    {
        Distance = PlayerCasting.DistanceFromTarget;
    }

    // Activates player casting and the Interactable UI display/text when within range
    protected void ActivateDisplayUI()
    {
        PlayerCasting.ActivateCasting();

        if (Distance > DistanceMax || Distance <= 0)
            return;

        ActionDisplay.SetActive(true);
        ActionText.SetActive(true);
    }

    // Deactivates player casting and the Interactable UI display/text, also resetting the casting distance.
    protected void DeactivateDisplayUI()
    {
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
        PlayerCasting.StopCasting();
        PlayerCasting.DistanceFromTarget = 1000.0f;
    }

    // Update the text to be displayed by the Interaction UI
    protected void SetInteractionText(string iText)
    {
        ActionText.GetComponent<Text>().text = iText;
    }
}
