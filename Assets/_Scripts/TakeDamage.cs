using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public Color[] colors = new Color[] { Color.red };


    public int life = 1;

    public Collider coll;

    public Outline outline;

    public TestBoom boom;

    IMove move;

    private void Awake()
    {
        move = GetComponent<IMove>();
    }


    public void Damage()
    {
        life--;



        if(life <= 0)
        {
            coll.enabled = false;
            move.StopMove();

            boom?.Explode();
        }
        else
        {
            Debug.Log("SetColor");
            outline.SetColor(colors[life]);
        }
    }
}
