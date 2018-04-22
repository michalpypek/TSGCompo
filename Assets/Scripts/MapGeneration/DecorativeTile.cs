using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeTile : MonoBehaviour
{
	SpriteRenderer rend;

	public void SetRandomSprite()
	{
		rend = GetComponent<SpriteRenderer>();
		rend.sprite = MapGenerator.instance.possibleDecorativeTiles[Random.Range(0, MapGenerator.instance.possibleDecorativeTiles.Count)];
	}
}
