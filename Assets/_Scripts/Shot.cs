using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if(other.tag == "Planetoid")
        {
            print("Colidiu");
            //other.GetComponent<TestAsteroid>()?.TakeDamage();
            //other.GetComponent<TestBoom>()?.Explode();
            other.GetComponent<TakeDamage>()?.Damage();
            //other.GetComponent<IMove>()?.StopMove();

            Destroy(gameObject);
        }
    }
}
