/* Created by Wilson World Games, September 2022 */

using System.Collections;
using UnityEngine;

public class UseElevator : Interactable
{
    [Header("Elevator Button Settings")]
    public ElevatorLift LiftPlatform;

    private void OnMouseOver()
    {
        if (LiftPlatform.LiftState == true)
            return;

        ActivateDisplayUI();
        SetInteractionText("Press Button");

        if (Input.GetButtonDown("Interact") && Distance <= DistanceMax) {
            DeactivateDisplayUI();
            ActivateLift();
        }
    }

    private void OnMouseExit()
    {
        DeactivateDisplayUI();
    }

    // Start the elevator
    void ActivateLift()
    {
        //this.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(PlayButtonFX());

        LiftPlatform.ActivateLift();
    }

    // Plays the effects for pressing the button
    IEnumerator PlayButtonFX()
    {
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<Animator>().Play("ButtonPress");

        yield return new WaitForSeconds(0.15f);

        this.GetComponent<Animator>().Play("New State");
    }
}
