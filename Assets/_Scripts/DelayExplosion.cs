using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayExplosion : MonoBehaviour, IExplode
{
    //public TestBoom testBoom;

    public DelayExplosion chainExplosion;

    public bool torque = true;

    [Header("Explosion")]
    public float power;
    public float radius;
    public float delay;

    [Space]

    public Rigidbody[] rbs;

    [Header("Debries")]
    public bool shrink = true;
    public float hideTime = 3;
    public AnimationCurve hideCurve;

    [ContextMenu("Explode2")]
    public void Explode()
    {
        chainExplosion?.Explode();
        StartCoroutine("Delay");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        DelayedExplode();
    }
    [ContextMenu("ExplodeNow")]
    void DelayedExplode()
    {
        //TesteTime.instance.TimeScale=0.2f;
        //TesteTime.instance.SlowTimeShort(0.2f, timeSlowDuration);

        Vector3 explosionPos = transform.position;

        //GameObject explosion = Instantiate(vfx, null);
        //explosion.transform.position = transform.position;

        foreach (var item in rbs)
        {
            item.isKinematic = false;
            item.AddExplosionForce(power, explosionPos, radius);


            if (torque)
            {
                //add Torque
                Vector3 torq = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
                item.AddTorque(torq * 100);
            }


        }


        if (shrink) StartCoroutine("HideDebries");

        //Destroy(explosion, 2);
    }

    IEnumerator HideDebries()
    {
        //float duration = 4;

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
    }
}
