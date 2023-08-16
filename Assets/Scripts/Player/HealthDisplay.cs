/* Created by Wilson World Games, August 2022 */
/* Updates the health display of the player's HUD */

using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [Header("UI Properties")]
    public GameObject HealthDisplayText;
    public GameObject HealthDisplayFill;
    public GameObject ArmorDisplayText;
    public GameObject ArmorDisplayFill;

    [Header("Color Settings")]
    [SerializeField] private Color GreenColor;
    [SerializeField] private Color YellowColor;
    [SerializeField] private Color RedColor;

    // Updates the text and color fill of the Health Display with specified value
    public void UpdateHealthDisplay(float healthCount)
    {
        HealthDisplayText.GetComponent<Text>().text = healthCount.ToString();
        HealthDisplayFill.GetComponent<Image>().fillAmount = healthCount / 100.0f;
        UpdateHealthFillColor(healthCount);

    }

    // Updates the text of the Armor Display with specified value
    public void UpdateArmorDisplay(float armorCount)
    {
        ArmorDisplayText.GetComponent<Text>().text = armorCount.ToString();
        ArmorDisplayFill.GetComponent<Image>().fillAmount = armorCount / 100.0f;
    }

    // Update the Fill color based on the remain health %
    void UpdateHealthFillColor(float healthCount)
    {
        if (healthCount <= 100.0f && healthCount > 50.0f)
            HealthDisplayFill.GetComponent<Image>().color = GreenColor;
        else if (healthCount <= 50.0f && healthCount > 15.0f)
            HealthDisplayFill.GetComponent<Image>().color = YellowColor;
        else
            HealthDisplayFill.GetComponent<Image>().color = RedColor;
    }
}
