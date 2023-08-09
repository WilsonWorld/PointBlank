/* Created by Wilson World Games, August 2022 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    public float ProjectileForce = 1000.0f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObj = collision.gameObject;

        if (collidedObj.tag == "Target")
        {
            collidedObj.GetComponent<Target>().LowerTarget();
        }

        if (collidedObj)
            Destroy(gameObject);
    }

    public void ProjectBullet(Vector3 direction)
    {
        m_Rigidbody.AddForce(direction * ProjectileForce);
    }
}
