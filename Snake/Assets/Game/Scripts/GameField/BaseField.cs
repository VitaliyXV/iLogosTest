using UnityEngine;
using System.Collections;

public abstract class BaseField : MonoBehaviour, IFieldGenerator<GameObject>
{
	public GameObject TileObject;
	public GameObject Wall;

	protected GameObject[,] field;

	private int width;
	private int height;
	private int visibleRadius;
	private int wallCount;

	public int Width
	{
		get	{ return width;	}
		set	{ width = value; }
	}

	public int Height
	{
		get	{ return height; }
		set	{ height = value; }
	}

	public int WallCount
	{
		get	{ return wallCount; }
		set { wallCount = value; }
	}

	public int VisibleRadius
	{
		get { return visibleRadius; }
		set { visibleRadius = value; }
	}

	public GameObject this[int x, int y]
	{
		get { return field[x, y]; }
		set { field[x, y] = value; }
	}

	void Awake()
	{
		WallCount = 10;
		Width = Height = 10;

		Generate();
		SetRandomWalls();
	}

	public abstract void Generate();

	public void SetRandomWalls()
	{
		for (int i = 0; i < wallCount; i++)
		{
			int x = Random.Range(1, Width);
			int y = Random.Range(1, Height);

			// TODO: ckeck, if wall already present on tail

			CreateWall(field[y, x]);
		}
	}

	protected GameObject CreateWall(GameObject tile)
	{
		var wall = Instantiate(Wall, tile.transform.position, Quaternion.identity) as GameObject;
		wall.transform.SetParent(tile.transform);

		return wall;
	}

	public void Clear()
	{
		field = null;
	}
}
