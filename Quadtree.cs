using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree
{
	
	
	
    
	public QuadTree parent = null;
	private QuadTree[] children; // will contain the 4 children tree
	
	private int Curent_Depth;
	private int Max_Depth;
	private float Block_size;
	private Vector3 coords;
	
	public QuadTree(Vector3 coords, float Block_size, int Curent_Depth = 0, int Max_Depth = 1)
	{
		this.Curent_Depth = Curent_Depth;
		this.Max_Depth = Max_Depth;
		this.Block_size = Block_size;
		this.coords = coords;
	}
	
	public void UpdateBasedOnProximity(Transform observer)
	{
		
	}
	public void Creat_Child()
	{
		children = new QuadTree[4];

		// Up left:
		children[0] = new QuadTree(new Vector3(this.coords.x - this.Block_size / 2f, 0, this.coords.z + Block_size / 2f), this.Block_size / 2f, this.Curent_Depth + 1, this.Max_Depth);

		// Up right: 
		children[1] = new QuadTree(new Vector3(this.coords.x + this.Block_size / 2f, 0, this.coords.z + Block_size / 2f), this.Block_size / 2f, this.Curent_Depth + 1, this.Max_Depth);

		// Down left: 
		children[2] = new QuadTree(new Vector3(this.coords.x + this.Block_size / 2f, 0, this.coords.z - Block_size / 2f), this.Block_size / 2f, this.Curent_Depth + 1, this.Max_Depth);

		// Down right:
		children[3] = new QuadTree(new Vector3(this.coords.x - this.Block_size / 2f, 0, this.coords.z - Block_size / 2f), this.Block_size / 2f, this.Curent_Depth + 1, this.Max_Depth);

		for (var i = 0; i < 4; i++)
			children[i].parent = this; // there so we can go bacj if needed
	}
	
	List<Vector3> Get_block_point()
         {
             var vertices = new List<Vector3>
             {
                 // Top Left
                 new Vector3(coords.x - Block_size, 0, coords.z - Block_size),
                 // Top Right
                 new Vector3(coords.x + Block_size, 0, coords.z - Block_size),
                 // Bottom Left
                 new Vector3(coords.x - Block_size, 0, coords.z + Block_size),
                 // Bottom Right
                 new Vector3(coords.x + Block_size, 0, coords.z + Block_size)
             };
             return vertices;
         }
		 
	public void OnDrawGizmo()
	{
		if (null != children)
		{
			foreach( QuadTree child in children )
				child.OnDrawGizmo();
		}
		Gizmos.DrawWireCube(coords, new Vector3(Block_size, Block_size, 1f));
	}
}
