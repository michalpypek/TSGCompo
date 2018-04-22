using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public Transform obstacleTransformHolder;
	public List<GameObject> obstacleTransforms = new List<GameObject>();
	public List<DecorativeTile> decorativeTiles = new List<DecorativeTile>();
	bool generated = false;

	void Start()
	{
		decorativeTiles = GetComponentsInChildren<DecorativeTile>().ToList();
	}

	public void GenerateObstacles()
	{
		if (generated)
			return;
		obstacleTransforms = new List<GameObject>();
		generated = true;

		var obstacleTransformsobj = obstacleTransformHolder.GetComponentsInChildren<Transform>().ToList();

		foreach (var item in obstacleTransformsobj)
		{
			obstacleTransforms.Add(item.gameObject);
		}

		foreach (var trasnf in obstacleTransforms)
		{
			if(Random.Range(0,100) < 35)
			{
				GameObject obstacle = MapGenerator.instance.possibleObstacles[Random.Range(0, MapGenerator.instance.possibleObstacles.Count)];
				GameObject instance = Instantiate(obstacle, trasnf.transform) as GameObject;
				instance.transform.localPosition = Vector2.zero;
			}
		}

		foreach (var tajel in decorativeTiles)
		{
			tajel.SetRandomSprite();
		}
	}
}
