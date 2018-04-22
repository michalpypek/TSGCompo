using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance;

    public List<GameObject> possibleObstacles;

    public List<Tile> possibleTiles = new List<Tile>();

    public Tile startTile;
    public Sprite finalSprite;
    public List<Tile> generatedTiles = new List<Tile>();
    public List<Sprite> possibleDecorativeTiles = new List<Sprite>();

    public bool initialized = false;

    float range = 100;
    public Vector3 targetPosition;

    [HideInInspector]
    public bool finalTargetFound = false;
    public Vector3 finalTarget;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        SetNewTargetPosition();
    }

    void Start()
    {
        generatedTiles.Add(startTile);
        startTile.GenerateObstacles();
        initialized = true;
    }

    public Vector2 tileSize
    {
        get { return possibleTiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size; }
    }

    public void SetNewTargetPosition()
    {
        targetPosition = targetPosition + Quaternion.AngleAxis(Random.Range(0f, 1f) * 360f, transform.forward) * (Vector3.up * range);
    }

    Tile PlayerPosToTile(Vector2 playerPos)
    {
        foreach (var tile in generatedTiles)
        {
            if (TileContainsPlayer(playerPos, tile))
            {
                return tile;
            }
        }
        return null;
    }

    public void SetFinalTarget()
    {
        RaycastHit2D raycastHit2D;
        bool hit = false;
        do
        {
            raycastHit2D = Physics2D.BoxCast(targetPosition + Quaternion.AngleAxis(Random.Range(0f, 1f) * 360f, transform.forward) * (Vector3.up * 25f), new Vector2(5f, 5f), 0f, Vector3.zero);
            hit = raycastHit2D.collider == null ? false : true;
        } while (!hit || raycastHit2D.collider.GetComponent<DecorativeTile>() == null);

        targetPosition = finalTarget = raycastHit2D.collider.transform.position;
        raycastHit2D.collider.GetComponent<SpriteRenderer>().sprite = finalSprite;
        finalTargetFound = true;
    }

    bool TileContainsPlayer(Vector2 playerPos, Tile tile)
	{
		var xFits = playerPos.x > tile.transform.position.x - (tileSize.x / 2) && playerPos.x < tile.transform.position.x + (tileSize.x / 2) ;
		var yFits = playerPos.y > tile.transform.position.y - (tileSize.y / 2) && playerPos.y < tile.transform.position.y + (tileSize.y / 2);
		return xFits && yFits;
	}

	public void GenerateTilesToPlayerPos(Vector2 playerPos)
	{
		if(!initialized)
		{
			return;
		}

		Tile current = PlayerPosToTile(playerPos);

		foreach (var neighbourPos in GetNeighbouringPositionsToSpawn(current))
		{
			Tile random = possibleTiles[Random.Range(0, possibleTiles.Count)];

			GameObject instance = Instantiate(random.gameObject, this.transform) as GameObject;
			instance.transform.position = neighbourPos;
			Tile tile = instance.GetComponent<Tile>();
			generatedTiles.Add(tile);
			tile.GenerateObstacles();
		}
	}

	List<Vector2> GetNeighbouringPositionsToSpawn(Tile tile)
	{
		var toRet = new List<Vector2>();
		var up = new Vector2(tile.transform.position.x, tile.transform.position.y + tileSize.y);
		var down = new Vector2(tile.transform.position.x, tile.transform.position.y - tileSize.y);
		var left = new Vector2(tile.transform.position.x - tileSize.x , tile.transform.position.y );
		var right = new Vector2(tile.transform.position.x + tileSize.x, tile.transform.position.y);

		var upleft = new Vector2(up.x - tileSize.x, up.y);
		var upright = new Vector2(up.x + tileSize.x, up.y);
		var downleft = new Vector2(down.x - tileSize.x, down.y);
		var downright = new Vector2(down.x + tileSize.x, down.y);

		var poss = new List<Vector2>
		{up, down, left, right, upleft, upright, downleft, downright};

		foreach (var pos in poss)
		{
			if(PlayerPosToTile(pos) == null)
			{
				toRet.Add(pos);
			}
		}

		return toRet;

	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 5f);
    }
}
