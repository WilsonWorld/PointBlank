/* Created by Wilson World Games, August 2022 */
/* The Open Doors scripts creates a seting of horizontally opening doors that the player can activate once.  */

using UnityEngine;

public class OpenDoors : Interactable
{
    [Header("Door Settings")]
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject DoorLight;
    public Color RedLight;
    public Color GreenLight;

    [SerializeField] bool isDoubleDoor = true;

    private void Start()
    {
        if (isDoubleDoor)
        {
            DoorLight.GetComponent<MeshRenderer>().material.color = RedLight;
            DoorLight.transform.GetChild(0).GetComponent<Light>().color = RedLight;
        }
    }

    private void OnMouseOver()
    {
        ActivateDisplayUI();
        SetInteractionText("Open Doors");

        if (Input.GetButtonDown("Interact") && Distance <= DistanceMax) {
            DeactivateDisplayUI();
            Open();
        }
    }

    private void OnMouseExit()
    {
        DeactivateDisplayUI();
    }

    // Opens the doors
    void Open()
    {
        if (isDoubleDoor)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            PlayOpenFX();
            UpdateDoorLight();
        }
        else
        {
            PlayOpenFX();
        }
    }

    // Plays the effects for opening the doors
    void PlayOpenFX()
    {
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<Animator>().Play("ButtonPress");

        if (isDoubleDoor)
        {
            LeftDoor.GetComponent<Animator>().Play("LeftDoorSlide");
            RightDoor.GetComponent<Animator>().Play("RightDoorSlide");
        }
        else
        {
            LeftDoor.GetComponent<Animator>().Play("SingleDoorSlide");
        }
    }

    // Changes the door light from red to green, showing it's been opened
    void UpdateDoorLight()
    {
        DoorLight.GetComponent<MeshRenderer>().material.color = GreenLight;
        DoorLight.transform.GetChild(0).GetComponent<Light>().color = GreenLight;
    }
}
