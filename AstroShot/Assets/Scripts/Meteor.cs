using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    [SerializeField] float speed = 3f;
    //[SerializeField] int point = 5;
    [SerializeField] float maxLifeTime = 6;

    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, maxLifeTime);
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -speed * 100, 0));
        
    }


}
