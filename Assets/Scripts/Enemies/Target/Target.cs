/* Created by Wilson World Games, August 2022 */
/* The Target class updates the score display once before resetting, and plays a rotation animation depending on the track type and where the target was shot. */

using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    public GameObject PivotPoint;
    public RangeTrack TargetTrack;
    public GunRangeScoreDisplay ScoreDisplay;
    public float ResetTime = 3.0f;

    bool isUpdating = false;
    bool isResetting = false;

    public bool Updating
    {
        get { return isUpdating; } 
        set { isUpdating = value; }
    }

    // Update the Score Display World UI by increasing it's value by the damage of the shot.
    public void UpdateDisplayScore(int damageValue)
    {
        if (isUpdating == false || isResetting == true)
            return;

        ScoreDisplay.DisplayScore += damageValue;
    }

    // Starts the reset cool down and plays a lowering animation. Lowers the target back and downward, and disables scoring.
    public void LowerTarget()
    {
        if (isUpdating == false || isResetting == true)
            return;

        isResetting = true;
        PivotPoint.GetComponent<Animator>().Play("LowerTarget");
        StartCoroutine(ResetRaisedTarget());
    }

    void RaiseTarget()
    {
        PivotPoint.GetComponent<Animator>().Play("RaiseTarget");
    }

    // Starts the reset cooldown and plays a leftward rotation animation. Turns the target 90 degrees to the rleft and disables scoring.
    public void RotateTargetLeft()
    {
        if (isUpdating == false || isResetting == true)
            return;

        isResetting = true;
        PivotPoint.GetComponent<Animator>().Play("RotateLeft");
        StartCoroutine(ResetLeftRotatedTarget());
    }

    void ReturnRotateTargetFromLeft()
    {
        PivotPoint.GetComponent<Animator>().Play("RotateLeftReturn");
    }

    // Starts the reset cooldown and plays a rightward rotation animation. Turns the target 90 degrees to the right and disables scoring.
    public void RotateTargetRight()
    {
        if (isUpdating == false || isResetting == true)
            return;

        isResetting = true;
        PivotPoint.GetComponent<Animator>().Play("RotateRight");
        StartCoroutine(ResetRightRotatedTarget());
    }

    void ReturnRotateTargetFromRight()
    {
        PivotPoint.GetComponent<Animator>().Play("RotateRightReturn");
    }

    // Reset timers called based on initial rotation that was called. Plays corresponding animation and lets the player shoot the target again.
    IEnumerator ResetRaisedTarget()
    {
        yield return new WaitForSeconds(ResetTime);
        RaiseTarget();
        isResetting = false;
    }

    IEnumerator ResetLeftRotatedTarget()
    {
        yield return new WaitForSeconds(ResetTime);
        ReturnRotateTargetFromLeft();
        isResetting = false;
    }

    IEnumerator ResetRightRotatedTarget()
    {
        yield return new WaitForSeconds(ResetTime);
        ReturnRotateTargetFromRight();
        isResetting = false;
    }
}
