using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlanet : MonoBehaviour, IMove
{
    public Rotatator rotatator;

    public bool moving { get; set; }
    public float moveFloat = 0;
    Vector3 startPos;

    public float speed = 1;

    [Header("things")]
    public TestBoom boom;

    public float sizeX = 1;
    public float sizeY = 1;


    public float timesX = 1;
    public float timesY = 1;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            moveFloat += Time.deltaTime * speed;

            float x = Mathf.Sin(moveFloat * timesX) * sizeX;
            float y = Mathf.Sin(moveFloat * timesY) * sizeY;

            transform.localPosition = startPos + Vector3.right * x + Vector3.up * y;

        }

    }

    [ContextMenu("StopMove")]
    public void StopMove()
    {
        moving = false;



        StartCoroutine(IeStop());
    }

    IEnumerator IeStop()
    {
        print(moveFloat);
        //float start = moveFloat % Mathf.PI;
        
        

        bool mode1 = moveFloat % (Mathf.PI * 2) < Mathf.PI;


        moveFloat = moveFloat % Mathf.PI;


        if (mode1)
        {
            //moveFloat = moveFloat % Mathf.PI;
            print(moveFloat);


            for (float i = 0; moveFloat < Mathf.PI; i += Time.deltaTime)
            {
                moveFloat += Time.deltaTime * speed *0.8f;

                float x = Mathf.Sin(moveFloat * timesX) * sizeX;
                float y = Mathf.Sin(moveFloat * timesY) * sizeY;

                transform.localPosition = startPos + Vector3.right * x + Vector3.up * y;

                //moveFloat

                yield return null;
            }
        }
        else
        {
            //moveFloat = moveFloat % Mathf.PI;

            moveFloat += Mathf.PI;

            print(moveFloat);

            for (float i = 0; moveFloat < Mathf.PI*2; i += Time.deltaTime)
            {
                moveFloat += Time.deltaTime * speed * 0.8f;

                float x = Mathf.Sin(moveFloat * timesX) * sizeX;
                float y = Mathf.Sin(moveFloat * timesY) * sizeY;

                transform.localPosition = startPos + Vector3.right * x + Vector3.up * y;

                //moveFloat

                yield return null;
            }
        }

        if (rotatator != null) rotatator.enabled = false;

        transform.localPosition = Vector3.zero;

        boom?.Explode();
    }

    [ContextMenu("Startmove")]
    public void StartMove()
    {
        moving = true;
    }
}
