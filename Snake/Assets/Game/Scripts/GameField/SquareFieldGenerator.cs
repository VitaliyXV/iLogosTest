using UnityEngine;
using System.Collections.Generic;

public class SquareFieldGenerator : BaseField
{
	public SquareFieldGenerator():base()
	{

	}

	public override void Generate()
	{
		field = new GameObject[Height + 2, Width + 2];

		int offsetX = -(Width / 2);
		int offsetY = -(int)(Height / 2.5);

		for (int y = 0; y < Height + 2; y++)
		{
			for (int x = 0; x < Width + 2; x++)
			{
				var type = (CellType)Random.Range(0, System.Enum.GetValues(typeof(CellType)).Length);

				var tile = Instantiate(TileObject, new Vector3(offsetX++, offsetY, 0), Quaternion.identity) as GameObject;
				tile.transform.SetParent(parent.transform);

				var cell = tile.GetComponent<Cell>();
				cell.Type = type;
				cell.SetColor();

				field[y, x] = tile;

				if (y == 0 || x == 0 || y == Height + 1 || x == Height + 1)
				{
					CreateWall(field[y, x]);
				}
			}
			offsetY++;
			offsetX = -(Width / 2);
		}
	}

	public override GameObject NextTile(Direction direction, ref int y, ref int x)
	{
		GameObject tile = null;

		switch (direction)
		{
			case Direction.UP: tile = field[++y, x]; break;
			case Direction.DOWN: tile = field[--y, x]; break;
			case Direction.RIGHT: tile = field[y, ++x]; break;
			case Direction.LEFT: tile = field[y, --x]; break;
		}

		return tile;
	}
}
