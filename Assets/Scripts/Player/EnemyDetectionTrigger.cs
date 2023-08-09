/* Created by Wilson World Games, August 2022 */
/* The Enemy Detection trigger simply grabs a zombie object and sets an attrack trigger to true/false when hitting the trigger. */

using UnityEngine;

public class EnemyDetectionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ZombieBody")
            if (other.gameObject.GetComponent<Zombie>())
                other.gameObject.GetComponent<Zombie>().AttackTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ZombieBody")
            if (other.gameObject.GetComponent<Zombie>())
                other.gameObject.GetComponent<Zombie>().AttackTriggered = false;
    }
}
