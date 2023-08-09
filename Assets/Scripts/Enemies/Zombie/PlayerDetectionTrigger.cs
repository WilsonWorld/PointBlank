/* Created by Wilson World Games, August 2022 */

using UnityEngine;

public class PlayerDetectionTrigger : MonoBehaviour
{
    public Enemy EnemyRef;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            EnemyRef.DetectedPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            EnemyRef.DetectedPlayer = false;
    }
}
