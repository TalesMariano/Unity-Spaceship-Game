using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWiggle : MonoBehaviour
{
    public bool moving = true;


    float moveFloat = 0;

    [Header("Wiggle")]
    public float wScale = 1;
    public float wSpeed = 1;

    Vector3 startPos;

    [Header("LaunchEnd")]
    public GameObject launchParti;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {

            moveFloat += Time.deltaTime;


            Vector3 wigglePos = Vector3.right * wScale * Mathf.Sin(moveFloat * wSpeed);

            transform.position = wigglePos + startPos;
        }
    }

    [ContextMenu("EndLaunch")]
    public void EndLaunch()
    {
        moving = false;
        StartCoroutine(IEEndLaunch());
    }
    
    IEnumerator IEEndLaunch()
    {
        StartCoroutine(ieMoveCenter());

        yield return new WaitForSeconds(1.2f);

        launchParti.SetActive(true);

        launchParti.transform.parent = null;

        yield return new WaitForSeconds(1);

        float launchMoveTime = 0.5f;

        Vector3 pos = transform.position;

        for (float i = 0; i < launchMoveTime; i+= Time.deltaTime)
        {
            transform.position = pos + Vector3.up * 10 * i / launchMoveTime;
            yield return null;
        }
    }

    public void StartMove()
    {
        moving = true;
    }

    public void StopShip()
    {
        moving = false;
        moveFloat = 0;

        StartCoroutine(ieMoveCenter());
    }

    IEnumerator ieMoveCenter()
    {
        float duration = 1f;

        for (float i = 0; i < duration; i+= Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, i / duration);

            yield return null;
        }

        transform.position = startPos;
    }
}
