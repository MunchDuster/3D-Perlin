using UnityEngine;
using System.Collections.Generic;
using System;

public class MapController : MonoBehaviour
{
	private struct Chunk
	{
		public float[,,] valueMap;
		public Mesh mesh;
		public MeshFilter meshFilter;
	}

	[Header("Map Settings")]
	public Modifier[] modifiers;
	public Vector3Int startChunks = Vector3Int.one * 4;
	public Vector3 voxelSize = Vector3.one / 2;
	public Vector3Int voxelsInChunk = Vector3Int.one * 10;
	public int renderChunkAhead = 1;

	public bool updateInPlayMode;

	[Header("Marching Cubes")]	
	[Range(0f,1f)]
	public float threshold = 0.5f;

	public Material material;

	private int maxVertsPerChunk;
	private int maxTrisPerChunk;

	private Dictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();


	// Start is called before the first frame update
	private void Start()
	{
		InitializeMap();
	}

	public void InitializeMap()
	{
		ClearChildren();
		chunks.Clear();
		for(int x = 0; x < startChunks.x; x++)
		{
			for(int y = 0; y < startChunks.y; y++)
			{
				for(int z = 0; z < startChunks.z; z++)
				{
					valueMap = GetValueMap(x, y, z);
					MakeChunkGameObject(x, y, z);
				}
			}
		}
	}

	public void UpdateMap()
	{
		for(int x = 0; x < startChunks.x; x++)
		{
			for(int y = 0; y < startChunks.y; y++)
			{
				for(int z = 0; z < startChunks.z; z++)
				{
					valueMap = GetValueMap(x, y, z);
					UpdateChunkGameObject(x, y, z);
				}
			}
		}
	}

	private void ClearChildren()
	{
		while (transform.childCount > 0) 
		{
			DestroyImmediate(transform.GetChild(0).gameObject);
		}
	}

	float[,,] valueMap;


	private void UpdateChunkGameObject(int x, int y, int z)
	{
		Vector3Int key = new Vector3Int(x, y, z);
		Mesh mesh = new Mesh();
		MeshGenerator.GenerateChunkMesh(valueMap, voxelSize, voxelsInChunk, threshold, ref mesh);
		Chunk chunk = chunks[key];
		chunk.mesh = mesh;
		chunk.meshFilter.mesh = chunks[key].mesh;
		chunk.valueMap = valueMap;
		chunks[key] = chunk;
	}

	private float[,,] GetValueMap(int x, int y, int z)
	{
		float[,,] valueMap = new float[voxelsInChunk.x + 1, voxelsInChunk.y + 1, voxelsInChunk.z + 1];


		//Apply modifications
		foreach(Modifier modifier in modifiers)
		{
			modifier.controller = this;
			modifier.Modify(ref valueMap, new Vector3Int(x, y, z));
		}

		return valueMap;
	}

	private void MakeChunkGameObject(int x, int y, int z)
	{
		Mesh mesh = new Mesh();
		MeshGenerator.GenerateChunkMesh(valueMap, voxelSize, voxelsInChunk, threshold, ref mesh);

		string chunkName = "Chunk " + x + " " + y + " " + z;
		GameObject chunkGameObject = new GameObject(chunkName, typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider));

		chunkGameObject.transform.SetParent(transform);
		chunkGameObject.transform.position = Vector3.Scale(new Vector3(x, y, z), voxelsInChunk);

		chunkGameObject.GetComponent<MeshRenderer>().material = material;
		chunkGameObject.GetComponent<MeshFilter>().mesh = mesh;

		Chunk newChunk = new Chunk();
		newChunk.mesh = mesh;
		newChunk.meshFilter = chunkGameObject.GetComponent<MeshFilter>();
		newChunk.valueMap = valueMap;

		chunks.Add(new Vector3Int(x, y, z), newChunk);
	}

	// OnDrawGizmosSelect is called every editor update when the gameObject is selected
	// private void OnDrawGizmos()
	// {
	// 	if(valueMap != null)
	// 	{
	// 		for(int x = 0; x < valueMap.GetLength(0); x++)
	// 		{
	// 			for(int y = 0; y < valueMap.GetLength(1); y++)
	// 			{
	// 				for(int z = 0; z < valueMap.GetLength(2); z++)
	// 				{
	// 					Gizmos.color = valueMap[x,y,z] >= threshold ? Color.white : Color.black;
	// 					Gizmos.DrawSphere(new Vector3(x, y, z), 0.1f);
	// 				}
	// 			}
	// 		}
	// 	}
	// }

	// FixedUpdate is called every physics update
	private void FixedUpdate()
	{
		if(updateInPlayMode)
			UpdateMap();
	}
}
