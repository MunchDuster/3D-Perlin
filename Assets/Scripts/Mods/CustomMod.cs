using System;
using UnityEngine;

public class CustomMod : Modifier
{
	[System.Serializable]
	public struct Point
	{
    	public Vector3Int pos;
		public float value;
	}
	public Point[] points;
	
        public override void Modify(ref float[,,] valueMap, Vector3Int chunkNo)
    {
		for(int x = 0; x < valueMap.GetLength(0); x++)
		{
			for(int y = 0; y < valueMap.GetLength(1); y++)
			{
				for(int z = 0; z < valueMap.GetLength(2); z++)
				{
					valueMap[x,y,z] = GetPoint(new Vector3Int(x,y,z));
				}
			}
		}
    }

	private float GetPoint(Vector3Int pos)
	{
		foreach(Point point in points)
		{
			if(point.pos == pos)
			{
				return point.value;
			}
		}
		return 0;
	}
}