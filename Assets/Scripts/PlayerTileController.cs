using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileController : MonoBehaviour
{
	void Update()
	{
		MapGenerator.instance.GenerateTilesToPlayerPos(transform.position);
	}
}
