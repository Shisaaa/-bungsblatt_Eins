using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class cubemv : MonoBehaviour
{

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    Vector3 test = player.transform.position;
    test.x+=5;
    test.y-=1;
    this.transform.position = test;
    }
}
