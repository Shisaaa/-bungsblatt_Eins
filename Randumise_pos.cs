using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randumise_pos : MonoBehaviour
{
	List<Vector3> LocalVertices;
	List<Vector3> GlobalVertices;
	List<Vector3> CornerVertices;
    
	List<int> CornerIDs = new List<int> {0,10,110,120};
	float radius = 5.5f;
	
	Mesh mesh;
    Vector3[] vertices;
	
	// Start is called before the first frame update
    void Start()
    {
		
		GlobalVertices = new List<Vector3>();
		CornerVertices = new List<Vector3>();
		//GetVertices();
		
		mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

	void OnPreRender()
    {
        GL.wireframe = true;
    }
	
	void OnPostRender()
    {
        GL.wireframe = false;
    }
    // Update is called once per frame
    void Update()
    {
		
		GetVertices();

		//for(int i=0; i<GlobalVertices.Count; i++)
		//{
		//	GlobalVertices[i].z =   Mathf.PerlinNoise(GlobalVertices[i].x,GlobalVertices[i].y);
		//}
    }
	
	void GetVertices()
    {
		LocalVertices = new List<Vector3>(GetComponent<MeshFilter>().mesh.vertices);
		GlobalVertices.Clear();
		foreach(Vector3 point in LocalVertices)
		{
			/*Vector3 tenp_vec = transform.TransformPoint(point);
			
			float p_noize = Mathf.PerlinNoise(tenp_vec.x/50.0f,tenp_vec.z/50.0f)*50.0f;
			
			Vector3 test_point = new Vector3(tenp_vec.x, tenp_vec.y +  p_noize, tenp_vec.z);
			Debug.Log(tenp_vec);		
			Debug.Log(p_noize);		
			GlobalVertices.Add(test_point);
			
			*/
			
			
			GlobalVertices.Add(transform.TransformPoint(point));
		}
		
		mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
		
		for (var i = 0; i < vertices.Length; i++)
        {
			Vector3 tenp_vec = transform.TransformPoint(LocalVertices[i]);
			float p_noize = Mathf.PerlinNoise(tenp_vec.x/50.0f,tenp_vec.z/50.0f)*50.0f;
			Vector3 test_point = new Vector3(LocalVertices[i].x, p_noize, LocalVertices[i].z);
			Debug.Log(tenp_vec);		
			Debug.Log(p_noize);	
            vertices[i] = test_point;
        }

        // assign the local vertices array into the vertices array of the Mesh.
        //mesh.vertices = vertices;
		
        mesh.SetVertices(vertices);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
		
		
		CornerVertices.Clear();
		
		foreach(int id in CornerIDs)
		{
			CornerVertices.Add(GlobalVertices[id]);
		}
		
    }
	
	void OnDrawGizmos()
    {
		if(GlobalVertices== null)
			return;
		Gizmos.color = Color.red;
		foreach(Vector3 point in GlobalVertices)
		{
			Gizmos.DrawSphere(point,radius);
		}
		Gizmos.color = Color.blue;
		foreach(Vector3 point in CornerVertices)
		{
			Gizmos.DrawSphere(point,radius);
		}
    }
}
