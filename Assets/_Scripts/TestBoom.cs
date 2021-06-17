using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoom : MonoBehaviour
{
    [Header("Settings")]
    public DelayExplosion chainExplosion;

    public Collider coll;

    float timeSlowDuration = 3;
    

    [Header("Objs")]
    public GameObject original;
    public GameObject frags;
    public GameObject vfx;
    public Rigidbody[] rbs;

    [Header("Explosion")]
    public float power;
    public float radius;

    [Header("Debries")]
    public bool shrink = true;
    public float hideTime = 3;
    public AnimationCurve hideCurve;

    

    // Start is called before the first frame update
    void Start()
    {

        foreach (var item in rbs)
        {
            item.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetExplosion()
    {
        original?.SetActive(false);
        frags?.SetActive(true);
    }

    [ContextMenu("Explode")]
    public void Explode()
    {
        Debug.Log("boom");

        chainExplosion?.Explode();


        SetExplosion();

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

        if(shrink) StartCoroutine("HideDebries");

        //Destroy(explosion, 2);
    }


    IEnumerator HideDebries()
    {
        //float duration = 4;

        for (float i = 0; i < hideTime; i+= Time.deltaTime)
        {
            foreach (var item in rbs)
            {
                float scale =  (i / hideTime);

                item.transform.localScale = Vector3.one * 100 *  hideCurve.Evaluate(scale);
            }


            yield return null;
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
