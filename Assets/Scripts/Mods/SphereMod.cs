using System;
using UnityEngine;

public class SphereMod : Modifier
{
	public Transform center;
	public float radius = 4;
	
    public override void Modify(ref float[,,] valueMap, Vector3Int chunkNo)
    {
		for(int x = 0; x < valueMap.GetLength(0); x++)
		{
			for(int y = 0; y < valueMap.GetLength(1); y++)
			{
				for(int z = 0; z < valueMap.GetLength(2); z++)
				{
					valueMap[x,y,z] += GetValue(GetPosition(new Vector3Int(x,y,z), chunkNo));
				}
			}
		}
    }

	private float GetValue(Vector3 pos)
	{
		float raw = radius - Vector3.Distance(pos, center.position);
		return Mathf.Clamp(raw, 0, 1);
	}
}