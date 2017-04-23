using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtils{

	public static float RangeFloat(Vector2 range){
		return Random.Range(range.x, range.y);
	}

	public static int RangeInt(Vector2 range){
		return (int)Random.Range(range.x, range.y);
	}
}