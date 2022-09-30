using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public abstract class Modifier : MonoBehaviour
{
	[HideInInspector] public MapController controller;

	protected Vector3 GetPosition (Vector3Int valuePoint, Vector3Int chunkNo)
	{
		Vector3 chunkOffset = Vector3.Scale(chunkNo, controller.voxelsInChunk);
		return Vector3.Scale(chunkOffset + valuePoint, controller.voxelSize);
	}

    public abstract void Modify(ref float[,,] valueMap, Vector3Int chunkNo);
}