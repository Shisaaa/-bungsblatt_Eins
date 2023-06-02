using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_capsul_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// Update is called once per frame
	void FixedUpdate()
	{
		transform.Translate(Vector3.forward * 0.75f);
		if (Input.GetKey(KeyCode.LeftArrow))
			transform.Translate(Vector3.left * 0.25f);
		if (Input.GetKey(KeyCode.RightArrow))
			transform.Translate(Vector3.right * 0.25f);
		if (Input.GetKey(KeyCode.UpArrow))
			transform.Translate(Vector3.up * 0.25f);
		if (transform.position.z > -700f)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, -2000f);
		}
	}
}
