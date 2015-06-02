using UnityEngine;
using System.Collections;

public interface IFieldGenerator<T>
{
	int Width { get; set; }
	int Height { get; set; }
	int VisibleRadius { get; set; }
	T this[int x, int y] { get; set; }

	void Generate();
	void Clear();
}
