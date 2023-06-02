using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_gen : MonoBehaviour
{

	public GameObject tree;

	List<Vector3> LocalVertices;
	List<Vector3> GlobalVertices;
	List<Vector3> CornerVertices;
    
	List<int> CornerIDs = new List<int> {0,10,110,120};
	float radius = 3.0f;
	
	Mesh mesh;
    Vector3[] vertices;
	
	// Start is called before the first frame update
    void Start()
    {
		
		GlobalVertices = new List<Vector3>();
		CornerVertices = new List<Vector3>();
		GetVertices();
		
    }

    // Update is called once per frame
    void Update()
    {
		
		//GetVertices();

    }
	
	void GetVertices()
    {
		LocalVertices = new List<Vector3>(GetComponent<MeshFilter>().mesh.vertices);
		GlobalVertices.Clear();
		foreach(Vector3 point in LocalVertices)
		{
			GlobalVertices.Add(transform.TransformPoint(point));
		}
		
		mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
		
		for (var i = 0; i < vertices.Length; i++)
        {
			Vector3 tenp_vec = transform.TransformPoint(LocalVertices[i]);
			float p_noize = Mathf.PerlinNoise(tenp_vec.x/1000.0f,tenp_vec.z/1000.0f)*400.0f;
			Vector3 test_point = new Vector3(LocalVertices[i].x, p_noize, LocalVertices[i].z);
			//Debug.Log(tenp_vec);		
			//Debug.Log(p_noize);	
            vertices[i] = test_point;
			
        }

		if (Random.Range(0,20) == 2){

				//GameObject tree_t = Instantiate(tree,vertices[Random.Range(0, vertices.Length-1)], Quaternion.identity) as GameObject;
				GameObject tree_t = Instantiate(tree,transform.TransformPoint(vertices[Random.Range(0, vertices.Length-1)]), Quaternion.identity) as GameObject;
				tree_t.transform.SetParent(this.transform);
				
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
	
}
