using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public List<Transform> obstacleTransforms = new List<Transform>();
	public List<DecorativeTile> decorativeTiles = new List<DecorativeTile>();

	void Start()
	{
		decorativeTiles = GetComponentsInChildren<DecorativeTile>().ToList();
	}

	public void GenerateObstacles()
	{
		foreach (var trasnf in obstacleTransforms)
		{
			if(Random.Range(0,100) < 75)
			{
				GameObject obstacle = MapGenerator.instance.possibleObstacles[Random.Range(0, MapGenerator.instance.possibleObstacles.Count)];
				GameObject instance = Instantiate(obstacle, trasnf) as GameObject;
				instance.transform.localPosition = Vector2.zero;
			}
		}

		foreach (var tajel in decorativeTiles)
		{
			tajel.SetRandomSprite();
		}
	}
}
