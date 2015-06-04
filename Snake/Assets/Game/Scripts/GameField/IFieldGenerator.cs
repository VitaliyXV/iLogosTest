using UnityEngine;
using System.Collections;

public interface IFieldGenerator<T>
{
	int Width { get; set; }
	int Height { get; set; }
	int WallCount { get; set; }
	int VisibleRadius { get; set; }
	T this[int y, int x] { get; set; }

	T PlayerStartPosition { get; set; }
	int PlayerStartPositionX { get; set; }
	int PlayerStartPositionY { get; set; }

	void Generate();
	void SetRandomWalls();
	GameObject SetFood(GameObject existingFood);
	void Clear();

	GameObject NextTile(Direction direction, ref int y, ref int x, ref Quaternion rotation);
}
