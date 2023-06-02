using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadtree_map_generation : MonoBehaviour
{
	public GameObject block_GameObject;
	
	public GameObject player;
	
	private int World_Size_X = 40;
	private int World_Size_Z = 40; // for now the world need to be a square
	private float Grid_Spacing = 32.0f * 10.0f;
	
	private bool new_block_entered = false;
	private float pos_block_removed = 0;
	
	public QuadTree tree;
	
	private List<Vector3> block_Position = new List<Vector3>();
	private List<GameObject> block_list = new List<GameObject>();
    
	
	public void Init()
	{
		InitializeQuadTree();
	}
	
	private void InitializeQuadTree()
	{
		tree = new QuadTree(new Vector3(0, 0, 0), World_Size_X*Grid_Spacing, 0, 10);
	}
	
	// Start is called before the first frame update
    void Start()
    {
       for(int x = -1; x < World_Size_X-1; x++) 
	   {
		   for(int z = -World_Size_Z/2; z < World_Size_Z/2; z++) 
		   {
			   // generate a block the size will depend on where the camera is
			   Vector3 chunck_pose = new Vector3(x  * Grid_Spacing + player.transform.position.x, 0, z * Grid_Spacing + player.transform.position.z );
			   
			   GameObject block = Instantiate(block_GameObject, chunck_pose, Quaternion.identity) as GameObject;
			   
			   block.transform.SetParent(this.transform);
			   
			   block_Position.Add(chunck_pose);
			   block_list.Add(block);
		   }
		}
		
		pos_block_removed = player.transform.position.x; // there to make sure I am not deleting block until pos_block_removed reach the xposition at startup
	}


    // Update is called once per frame
    void Update()
    {
		// will probably need to generate the quadtree for LOD and then do the camera vew stuff her
		
		if (pos_block_removed <= player.transform.position.x)
		{
			
			
			for(int i =0; i<World_Size_Z; i++) // get ride of the old line of block
			{
				GameObject.DestroyImmediate(block_list[0]);
				block_list.RemoveAt(0);
				block_Position.RemoveAt(0);
			}
			
		   for(int z = -World_Size_Z/2; z < World_Size_Z/2; z++) // generate a new raw
		   {
			   // generate a block the size will depend on where the camera is
			   Vector3 chunck_pose = new Vector3((World_Size_X-1) * Grid_Spacing + pos_block_removed, 0, z * Grid_Spacing + (float)((double)player.transform.position.z ));
			   
			   GameObject block = Instantiate(block_GameObject, chunck_pose, Quaternion.identity) as GameObject;
			   
			   block.transform.SetParent(this.transform);
			   
			   block_Position.Add(chunck_pose);
			   block_list.Add(block);
		   }
		   
		   pos_block_removed += Grid_Spacing;
		}
    }
	
	private void OnDrawGizmos()
	{
		if (null == tree)
		{
			Init();
			return;
		}
		tree.OnDrawGizmo();
	}
}

