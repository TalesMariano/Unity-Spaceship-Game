using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IExplode
{
    

    [Header("Settings")]
    float timeSlowDuration = 3;
    public IExplode chainExplosion;

    [Header("Objs")]
    public GameObject original;
    public GameObject frags;
    public GameObject vfx;
    public Rigidbody[] rbs;

    [Header("Explosion")]
    public float power;
    public float radius;

    [Header("Debries")]
    public float hideTime = 3;
    public bool shrink = true;
    public AnimationCurve hideCurve;

    
    void Start()
    {
        foreach (var item in rbs)
        {
            item.isKinematic = true;
        }
    }

    [ContextMenu("Explode")]
    public void Explode()
    {
        Debug.Log("boom");

        chainExplosion?.Explode();

        original?.SetActive(false);
        frags?.SetActive(true);

        //TesteTime.instance.TimeScale=0.2f;
        TesteTime.instance.SlowTimeShort(0.2f, timeSlowDuration);

        Vector3 explosionPos = transform.position;

        GameObject explosion = Instantiate(vfx, null);
        explosion.transform.position = transform.position;

        foreach (var item in rbs)
        {
            item.isKinematic = false;
            item.AddExplosionForce(power, explosionPos, radius);
        }

        if (shrink) StartCoroutine("HideDebries");

        Destroy(explosion, 5);
    }

    IEnumerator HideDebries()
    {
        for (float i = 0; i < hideTime; i += Time.deltaTime)
        {
            foreach (var item in rbs)
            {
                float scale = (i / hideTime);

                item.transform.localScale = Vector3.one * 100 * hideCurve.Evaluate(scale);
            }


            yield return null;
        }

        Destroy(gameObject);
        /*
        foreach (var item in rbs)
        {
            Destroy(item.gameObject);
        }*/
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
