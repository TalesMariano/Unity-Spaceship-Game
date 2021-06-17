using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject startMessage;

    public ShipWiggle ship;

    // Start is called before the first frame update
    void Start()
    {
        startMessage.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            startMessage.SetActive(false);

            ship.StartMove();

            Destroy(this);
        }


    }
}
