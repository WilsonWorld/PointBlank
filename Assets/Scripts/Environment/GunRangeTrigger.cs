/* Created by Wilson World Games, August 2022 */
/* The Gun Range Trigger allows for a range's objects to update while the player is in range to use them. When out of range, objects that can update will stop to increase performance. */

using System.Collections.Generic;
using UnityEngine;

public class GunRangeTrigger : MonoBehaviour
{
    [Header("Gun Range Objects")]
    public List<Target> RangeTargets;
    public List<RangeTrack> RangeTracks;
    public List<Light> RangeLights;
    public GunRangeScoreDisplay RangeScoreDisplay;

    // Activates the gun range's objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        RangeScoreDisplay.transform.GetChild(0).gameObject.SetActive(true);
        RangeScoreDisplay.Updating = true;

        foreach (var light in RangeLights)
        {
            light.enabled = true;
        }

        foreach (var target in RangeTargets)
        {
            target.Updating = true;
        }

        foreach (var track in RangeTracks)
        {
            track.Updating = true;
        }
    }

    // Disables the gun range's objects
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        RangeScoreDisplay.Updating = false;
        RangeScoreDisplay.transform.GetChild(0).gameObject.SetActive(false);

        foreach (var target in RangeTargets)
        {
            target.Updating = false;
        }

        foreach (var track in RangeTracks)
        {
            track.Updating = false;
        }

        foreach (var light in RangeLights)
        {
            light.enabled = false;
        }
    }
}
