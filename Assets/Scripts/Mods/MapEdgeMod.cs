using System;
using UnityEngine;

public class MapEdgeMod : Modifier
{
    public float set = 1;

    public override void Modify(ref float[,,] valueMap, Vector3Int chunkNo)
    {
		//Left
		if(chunkNo.x == 0)
		{
			int x = 0;
			for(int y = 0; y < valueMap.GetLength(1); y++)
			{
				for(int z = 0; z < valueMap.GetLength(2); z++)
				{
					valueMap[x,y,z] = set;
				}
			}
		}
		//Right
		if(chunkNo.x == controller.startChunks.x - 1)
		{
			int x = valueMap.GetLength(0) - 1;
			for(int y = 0; y < valueMap.GetLength(1); y++)
			{
				for(int z = 0; z < valueMap.GetLength(2); z++)
				{
					valueMap[x,y,z] = set;
				}
			}
		}

		//Bottom
		if(chunkNo.y == 0)
		{
			int y = 0;
			for(int x = 0; x < valueMap.GetLength(0); x++)
			{
				for(int z = 0; z < valueMap.GetLength(2); z++)
				{
					valueMap[x,y,z] = set;
				}
			}
		}
		//Top
		if(chunkNo.y == controller.startChunks.y - 1)
		{
			int y = valueMap.GetLength(1) - 1;
			for(int x = 0; x < valueMap.GetLength(0); x++)
			{
				for(int z = 0; z < valueMap.GetLength(2); z++)
				{
					valueMap[x,y,z] = set;
				}
			}
		}

		//Back
		if(chunkNo.z == 0)
		{
			int z = 0;
			for(int y = 0; y < valueMap.GetLength(1); y++)
			{
				for(int x = 0; x < valueMap.GetLength(0); x++)
				{
					valueMap[x,y,z] = set;
				}
			}
		}
		//Front
		if(chunkNo.z == controller.startChunks.z - 1)
		{
			int z = valueMap.GetLength(2) - 1;
			for(int y = 0; y < valueMap.GetLength(1); y++)
			{
				for(int x = 0; x < valueMap.GetLength(0);x++)
				{
					valueMap[x,y,z] = set;
				}
			}
		}
    }
}
