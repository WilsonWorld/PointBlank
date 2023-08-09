/* Created by Wilson World Games, August 2022 */
/* The Gun Range Display Reset returns the Gun Range's Score Board back to the default value of 0. */

using System.Collections;
using UnityEngine;

public class GunRangeDisplayReset : Interactable
{
    [Header("Score Settings")]
    public GunRangeScoreDisplay ScoreDisplay;

    private void OnMouseOver()
    {
        ActivateDisplayUI();
        SetInteractionText("Reset Score");

        if (Input.GetButtonDown("Interact") && Distance <= DistanceMax) {
            DeactivateDisplayUI();
            ResetScore();
        }
    }

    private void OnMouseExit()
    {
        DeactivateDisplayUI();
    }

    // Disable the collider until resetting is finished, play the reset FXs, update the Score UI, and start the reset timer.
    private void ResetScore()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<Animator>().Play("PressResetButton");
        ScoreDisplay.ResetDisplayScore();
        StartCoroutine(ResetButton());
    }

    // Returns the button to inital pos, and enables the collider so the player can use the button again.
    IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(0.3f);
        this.GetComponent<Animator>().Play("ResetButton");

        yield return new WaitForSeconds(0.1f);
        this.GetComponent<BoxCollider>().enabled = true;
    }
}
