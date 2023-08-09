/* Created by Wilson World Games, August 2022 */
/* Updates the health display of the player's HUD */

using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public GameObject HealthDisplayUI;
    public GameObject ArmorDisplayUI;

    // Updates the text of the Health Display with specified value
    public void UpdateHealthDisplay(float healthCount)
    {
        HealthDisplayUI.GetComponent<Text>().text = healthCount.ToString();
    }

    public void UpdateArmorDisplay(float armorCount)
    {
        ArmorDisplayUI.GetComponent<Text>().text = armorCount.ToString();
    }
}
