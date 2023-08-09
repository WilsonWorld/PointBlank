/* Created by Wilson World Games, August 2022 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public GunRangeScoreDisplay ScoreDisplay;
    public int ZoneValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        ScoreDisplay.DisplayScore += ZoneValue;
    }
}
