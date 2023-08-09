/* Created by Wilson World Games, September 2022 */

using System.Collections;
using UnityEngine;

public class ElevatorLift : MonoBehaviour
{
    public Transform BottomPosition;
    public Transform TopPosition;
    public float TravelSpeed = 5.0f;
    public float DelayTime = 3.0f;
    public bool LiftState = false;
    public bool AtBottomPosition = true;

    bool UpMovement = false;
    bool DownMovement = false;

    private void Update()
    {
        if (UpMovement == true)
            TravelUpward();

        if (DownMovement == true)
            TravelDownward();
    }

    public void ActivateLift()
    {
        LiftState = true;
        StartCoroutine(LiftSequence());
    }

    void TravelUpward()
    {
        Vector3 direction = (TopPosition.position - BottomPosition.position).normalized;
        gameObject.transform.position += direction * TravelSpeed * Time.deltaTime;

        if (gameObject.transform.position.y >= TopPosition.position.y + 0.01f) {
            UpMovement = false;
            AtBottomPosition = false;
            StartCoroutine(EndSequence());
        }
    }

    void TravelDownward()
    {
        Vector3 direction = (BottomPosition.position - TopPosition.position).normalized;
        gameObject.transform.position += direction * TravelSpeed * Time.deltaTime;

        if (gameObject.transform.position.y <= BottomPosition.position.y + 0.01f) {
            DownMovement = false;
            AtBottomPosition = true;
            StartCoroutine(EndSequence());
        }
    }

    IEnumerator LiftSequence()
    {
        yield return new WaitForSeconds(DelayTime);

        if (AtBottomPosition == true)
            UpMovement = true;
        else
            DownMovement = true;
    }

    IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(DelayTime);

        LiftState = false;
    }
}
