/* Created by Wilson World Games, August 2022 */
/* The Gun Range Score Display is reponsible for handling the World UI which displays the score/value of damaged targets. Helps the player to gauge a weapon's DPS potential. */

using UnityEngine;
using UnityEngine.UI;

public class GunRangeScoreDisplay : MonoBehaviour
{
    [Header("Display Settings")]
    public Text ScoreText;
    public int DisplayScore = 0;

    bool isUpdating = false;

    private void Update()
    {
        if (isUpdating == false)
            return;

        ScoreText.text = DisplayScore.ToString();
    }

    public void ResetDisplayScore()
    {
        DisplayScore = 0;
    }

    public bool Updating
    {
        get { return isUpdating; }
        set { isUpdating = value; }
    }
}
