/* Created by Wilson World Games, August 2022 */
/* The Open Doors scripts creates a seting of horizontally opening doors that the player can activate once.  */

using UnityEngine;

public class OpenDoors : Interactable
{
    [Header("Door Settings")]
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject DoorLight;
    public GameObject LockLight;
    public Color RedLight;
    public Color GreenLight;
    public Inventory PlayerInventoryRef;

    [SerializeField] int DoorCode = -2;
    [SerializeField] bool isDoubleDoor = true;
    [SerializeField] bool bLocked = false;
    bool bOpened = false;

    private void Start()
    {
        if (isDoubleDoor && DoorLight) {
            DoorLight.GetComponent<MeshRenderer>().material.color = RedLight;
            DoorLight.transform.GetChild(0).GetComponent<Light>().color = RedLight;
        }

        if (bLocked && LockLight) {
            LockLight.GetComponent<MeshRenderer>().material.color = RedLight;
            LockLight.GetComponent <Light>().color = RedLight;
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

    // Check if the door is locked and if the player has the key for it. Opens the doors if able.
    void Open()
    {
        if (bLocked) {
            foreach ( Key key in PlayerInventoryRef.m_Keys) {
                if (key.KeyCode == DoorCode) {
                    UpdateLockLight();
                    bLocked = false;
                    break;
                }
            }
        }

        if (bLocked)
            return;

        if (isDoubleDoor) {
            this.GetComponent<BoxCollider>().enabled = false;
            PlayOpenFX();
            UpdateDoorLight();
        }
        else {
            PlayOpenFX();
        }
    }

    // Plays the effects for opening the doors
    void PlayOpenFX()
    {
        this.GetComponent<AudioSource>().Play();
        this.GetComponent<Animator>().Play("ButtonPress");

        if (isDoubleDoor) {
            LeftDoor.GetComponent<Animator>().Play("LeftDoorSlide");
            RightDoor.GetComponent<Animator>().Play("RightDoorSlide");
        }
        else {
            LeftDoor.GetComponent<Animator>().Play("SingleDoorSlide");
        }
    }

    // Changes the door light from red to green, showing it's been opened
    void UpdateDoorLight()
    {
        DoorLight.GetComponent<MeshRenderer>().material.color = GreenLight;
        DoorLight.transform.GetChild(0).GetComponent<Light>().color = GreenLight;
    }

    // Changes the lock light from red to green, showing it's been unlocked
    void UpdateLockLight()
    {
        if (LockLight) {
            LockLight.GetComponent<MeshRenderer>().material.color = GreenLight;
            LockLight.GetComponent<Light>().color = GreenLight;
        }
    }
}
