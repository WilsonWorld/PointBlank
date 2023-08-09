/* Created by Wilson World Games, August 2022 */

using UnityEngine;

public class RangeTrack : MonoBehaviour
{
    [Header("Track Objects")]
    public GameObject TargetObject;
    public Transform FrontPosition;
    public Transform BackPosition;

    [Header("Track Settings")]
    public float TravelSpeed;
    public bool moveFrontToBack;
    public bool moveUpToDown;
    public bool isMovingOnX;
    public bool isMovingOnY;

    bool isUpdating = false;

    public bool Updating
    {
        get { return isUpdating; }
        set { isUpdating = value; }
    }

    private void Update()
    {
        if (isUpdating == false)
            return;

        if (isMovingOnX) {
            if (moveFrontToBack)
                TravelBackward();
            else
                TravelForward();
        }

        if (isMovingOnY) {
            if (moveUpToDown)
                TravelUpward();
            else
                TravelDownward();
        }
    }

    void TravelBackward()
    {
        Vector3 direction = (BackPosition.position - FrontPosition.position).normalized;
        TargetObject.transform.position += direction * TravelSpeed * Time.deltaTime;

        if (TargetObject.transform.position.x >= BackPosition.position.x + 0.1f)
            moveFrontToBack = false;
    }

    void TravelForward()
    {
        Vector3 direction = (FrontPosition.position - BackPosition.position).normalized;
        TargetObject.transform.position += direction * TravelSpeed * Time.deltaTime;

        if (TargetObject.transform.position.x <= FrontPosition.position.x + 0.1f)
            moveFrontToBack = true;
    }

    void TravelDownward()
    {
        Vector3 direction = (BackPosition.position - FrontPosition.position).normalized;
        TargetObject.transform.position += direction * TravelSpeed * Time.deltaTime;

        if (TargetObject.transform.position.y <= BackPosition.position.y + 0.01f)
            moveUpToDown = true;
    }

    void TravelUpward()
    {
        Vector3 direction = (FrontPosition.position - BackPosition.position).normalized;
        TargetObject.transform.position += direction * TravelSpeed * Time.deltaTime;

        if (TargetObject.transform.position.y >= FrontPosition.position.y + 0.01f)
            moveUpToDown = false;
    }
}
