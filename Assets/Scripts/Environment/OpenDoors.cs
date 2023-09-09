/* Created by Wilson World Games, August 2022 */
/* The Open Doors scripts creates a seting of horizontally opening doors that the player can activate once.  */

using System.Collections;
using UnityEngine;

public class OpenDoors : Interactable
{
    [Header("Door Settings")]
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject DoorLight;
    public GameObject LockLight;
    public Inventory PlayerInventoryRef;
    [SerializeField] int DoorCode = -2;
    [SerializeField] bool isDoubleDoor = true;
    [SerializeField] bool bLocked = false;
    bool bOpened = false;

    [Header("Door Effects")]
    public Color RedLight;
    public Color GreenLight;
    [SerializeField] AudioSource MoveSFX;


    private void Start()
    {
        if (isDoubleDoor && DoorLight) {
            DoorLight.GetComponent<MeshRenderer>().material.color = RedLight;
            DoorLight.transform.GetChild(0).GetComponent<Light>().color = RedLight;
            DoorLight.transform.GetChild(1).GetComponent<Light>().color = RedLight;
        }

        if (bLocked && LockLight) {
            LockLight.GetComponent<MeshRenderer>().material.color = RedLight;
            LockLight.GetComponent <Light>().color = RedLight;
        }

        if (bLocked == false && LockLight) {
            LockLight.GetComponent<MeshRenderer>().material.color = GreenLight;
            LockLight.GetComponent<Light>().color = GreenLight;
        }
    }

    private void OnMouseOver()
    {
        ActivateDisplayUI();
        if (bOpened)
            SetInteractionText("Close Doors");
        else
            SetInteractionText("Open Doors");

        if (Input.GetButtonDown("Interact") && Distance <= DistanceMax) {
            DeactivateDisplayUI();
            OnDoorButtonPress();
        }
    }

    private void OnMouseExit()
    {
        DeactivateDisplayUI();
    }

    // Check if the door is open or closed to perform the corresponding action
    void OnDoorButtonPress()
    {
        PlayButtonFX();
        StartCoroutine(ResetCollider());

        if (bOpened)
            Close();
        else
            Open();
    }

    // Checks 'Lock' status and 'Open' status to determine appropriate open action and sets the 'Open' state to true
    void Open()
    {
        CheckForLock();

        if (bLocked)
            return;

        CheckOpenState();
        bOpened = true;
    }

    // Checks 'Open' status to determine appropriate close action and sets the 'Open' state to false
    void Close()
    {
        CheckOpenState();
        bOpened = false;
    }

    // Plays the effects for opening the doors
    void PlayOpenFX()
    {
        PlayMoveSFX();

        if (isDoubleDoor) {
            LeftDoor.GetComponent<Animator>().Play("LeftDoorSlide");
            RightDoor.GetComponent<Animator>().Play("RightDoorSlide");
        }
        else {
            LeftDoor.GetComponent<Animator>().Play("SingleDoorSlide");
        }
    }

    // Plays the effects for closing the doors
    void PlayCloseFX()
    {
        PlayMoveSFX();

        if (isDoubleDoor) {
            LeftDoor.GetComponent<Animator>().Play("LeftDoorReturn");
            RightDoor.GetComponent<Animator>().Play("RightDoorReturn");
        }
        else {
            LeftDoor.GetComponent<Animator>().Play("SingleDoorReturn");
        }
    }

    // Turn the collider off for 2 seconds and turn it back on
    IEnumerator ResetCollider()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2.0f);
        this.GetComponent<BoxCollider>().enabled = true;
    }

    // Look through the keys in the player inventory for a match if the door is locked.
    void CheckForLock()
    {
        if (bLocked == false)
            return;

        if (PlayerInventoryRef == null || PlayerInventoryRef.m_Keys.Count == 0)
            return;

        foreach (Key key in PlayerInventoryRef.m_Keys) {
            if (key.KeyCode == DoorCode) {
                UpdateLockLight();
                bLocked = false;
                return;
            }
        }
    }

    // Check if attached object is a gate or door and play appropriate opening effect
    void CheckOpenState()
    {
        if (isDoubleDoor)
            UpdateDoorLight();

        if (bOpened)
            PlayCloseFX();
        else
            PlayOpenFX();
    }


    // Changes the door light between red and green to show if it's currently open
    void UpdateDoorLight()
    {
        if (bOpened) {
            DoorLight.GetComponent<MeshRenderer>().material.color = RedLight;
            DoorLight.transform.GetChild(0).GetComponent<Light>().color = RedLight;
            DoorLight.transform.GetChild(1).GetComponent<Light>().color = RedLight;
        }
        else {
            DoorLight.GetComponent<MeshRenderer>().material.color = GreenLight;
            DoorLight.transform.GetChild(0).GetComponent<Light>().color = GreenLight;
            DoorLight.transform.GetChild(1).GetComponent<Light>().color = GreenLight;
        }
    }

    // Changes the lock light from red to green, showing it's been unlocked
    void UpdateLockLight()
    {
        if (LockLight) {
            LockLight.GetComponent<MeshRenderer>().material.color = GreenLight;
            LockLight.GetComponent<Light>().color = GreenLight;
        }
    }

    IEnumerator ReturnButtonToIdle()
    {
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Animator>().Play("ButtonIdle");
    }

    // Plays the interaction sfx and press animation for the button
    void PlayButtonFX()
    {
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<Animator>().Play("ButtonPress");
        StartCoroutine(ReturnButtonToIdle());
    }

    void PlayMoveSFX()
    {
        if (MoveSFX)
            MoveSFX.Play();
    }
}
