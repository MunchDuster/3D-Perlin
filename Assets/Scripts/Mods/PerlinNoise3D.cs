using System;
using UnityEngine;

public class PerlinNoise3D : Modifier
{
    public Vector3 offset = Vector3.zero;
    public float noiseDensity = 1;

    private float CalculatePoint(Vector3 pos)
    {
        float XY = Mathf.PerlinNoise(offset.x + pos.x * noiseDensity, offset.y + pos.y * noiseDensity);
        float YZ = Mathf.PerlinNoise(offset.y + pos.y * noiseDensity, offset.z + pos.z * noiseDensity);
        float ZX = Mathf.PerlinNoise(offset.z + pos.z * noiseDensity, offset.x + pos.x * noiseDensity);

        float YX = Mathf.PerlinNoise(offset.y + pos.y * noiseDensity, offset.x + pos.x * noiseDensity);
        float ZY = Mathf.PerlinNoise(offset.z + pos.z * noiseDensity, offset.y + pos.y * noiseDensity);
        float XZ = Mathf.PerlinNoise(offset.x + pos.x * noiseDensity, offset.z + pos.z * noiseDensity);

        float val = (XY + YZ + ZX + YX + ZY + XZ) / 6f;
        return val;
    }

    public override void Modify(ref float[,,] valueMap, Vector3Int chunkNo)
    {
		for(int x = 0; x < valueMap.GetLength(0); x++)
		{
			for(int y = 0; y < valueMap.GetLength(1); y++)
			{
				for(int z = 0; z < valueMap.GetLength(2); z++)
				{
					Vector3 pos = GetPosition(new Vector3Int(x, y, z), chunkNo);
					valueMap[x,y,z] += CalculatePoint(pos);
				}
			}
		}
    }
}
