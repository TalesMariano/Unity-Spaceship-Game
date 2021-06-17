using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipShot : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform tipShot;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")|| Input.GetMouseButtonDown(0))
        {
            Instantiate(shotPrefab,tipShot.position, Quaternion.identity, null);
        }
    }
}
