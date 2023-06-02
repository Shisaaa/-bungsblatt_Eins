using UnityEngine;

public class Camera_mv : MonoBehaviour
{
	public float camera_speed;
	public float camera_hight;
	
	private float seed_inc = 1.0f;
	private float hight_inc = 10.0f;
	
	private Vector3 camera_position;
	
    // Start is called before the first frame update

    void Start()
    {
		camera_position = this.transform.position;

    }
	
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.W))
			camera_speed+=seed_inc;
		if (Input.GetKey(KeyCode.S))
		{
			camera_speed -= seed_inc;
			if (camera_speed < 0)
				camera_speed = 0;
		}
		if (Input.GetKey(KeyCode.UpArrow))
			camera_speed += seed_inc;

		if (Input.GetKey(KeyCode.DownArrow))
		{
			camera_speed -= seed_inc;
			if (camera_speed < 0)
				camera_speed = 0;
		}
		if(Input.GetKeyDown(KeyCode.Escape))
			Application. Quit();
    }
	
	void FixedUpdate()
	{
		transform.Translate(Vector3.forward * camera_speed);

		camera_position.x += camera_speed;
		
		camera_position.y = Mathf.PerlinNoise(camera_position.x/1000.0f,camera_position.z/1000.0f) * 400.0f + camera_hight;
 
		this.transform.position = camera_position;
	}
}
