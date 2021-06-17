using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAsteroid : MonoBehaviour, IMove
{
    public Rotatator rotatator;

    public Vector3 startPos, endPos;

    public float speed = 1;

    //bool moving = false;
    float moveFloat = 0;

    public bool moving { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            moveFloat += Time.deltaTime;

            transform.localPosition = Vector3.Lerp(startPos, endPos, moveFloat * speed % 1);
        }
    }

    public void StartMove()
    {
        moving = true;
    }

    public void StopMove()
    {
        moving = false;
        if(rotatator!= null) rotatator.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(startPos, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(endPos, 1);
    }
}
