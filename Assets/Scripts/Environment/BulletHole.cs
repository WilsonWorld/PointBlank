/* Created by Wilson World Games, August 2022 */
/* The Bullet Hole script allows a bullet hole FX prefab to simulate a bullet hit and then removes it from the scene for efficeny. */

using System.Collections;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    public ParticleSystem HitPFX;
    public float Lifetime = 10.0f;

    private void Start()
    {
        StartCoroutine(LifeCycle());
    }

    // Plays an impact sound and waits until the lifetime has been reached before destroying the object.
    IEnumerator LifeCycle()
    {
        HitPFX.Play();

        yield return new WaitForSeconds(0.03f);
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }
}
