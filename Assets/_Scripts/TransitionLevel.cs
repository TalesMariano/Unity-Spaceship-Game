using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionLevel : MonoBehaviour
{
    public Renderer render;

    public ParticleSystem particle;

    public float duration = 1;

    public AnimationCurve curveOffset;
    public AnimationCurve curveMove;

    public float tilePlus = 10;



    [ContextMenu("VFX")]
    public void VFX()
    {
        StartCoroutine(TransictionFX());
        StartCoroutine(ParticleFX());
    }

    IEnumerator ParticleFX()
    {
        yield return new WaitForSeconds(duration * 0.1f);
        particle.Play();
        yield return new WaitForSeconds(duration * 0.7f);
        particle.Stop();
    }


    IEnumerator TransictionFX()
    {
        //Material mat = render.material;


        for (float i = 0; i < duration; i+= Time.deltaTime)
        {
            //mat.SetTextureScale(0,)

            Vector2 scale = Vector2.up + Vector2.right * curveOffset.Evaluate(i / duration);

            Vector2 offset = Vector2.Lerp(Vector2.zero, Vector2.right * tilePlus, curveMove.Evaluate( i / duration));

            render.material.SetTextureOffset("_MainTex", offset);
            render.material.SetTextureScale("_MainTex", scale);
            yield return null;
        }



        //End




    }

}
