using System;
using System.Collections.Generic;
using UnityEngine;


public class GeneratePlanetMask : MonoBehaviour
{
	const float timeBetweenUpdate = 0.1f;

	public String SplatterTag;
	Texture2D mask;
	int width;
	int height;

	float nextUpdate;

	Color[] map;

	void Start()
	{
		var sprite = GetComponent<SpriteRenderer>();
		var size = sprite.bounds.size;
		width = (int)size.x;
		height = (int)size.y;
		mask = new Texture2D(width, height, TextureFormat.ARGB32, false) { filterMode = FilterMode.Point };
		sprite.material.SetTexture("_MaskTex", mask);
		map = new Color[width * height];
		nextUpdate = 0;

	}

	void Update()
	{
		if (Chronos.Instance.Time < nextUpdate)
			return;

		nextUpdate = Chronos.Instance.Time + timeBetweenUpdate;

		Array.Clear(map, 0, map.Length);

		List<SplatterComponent> splatters;
		if (SplatterComponent.Splatters.TryGetValue(SplatterTag, out splatters))
		{
			foreach (var splatter in splatters)
			{
				var x = (int)(splatter.transform.localPosition.x + width / 2);
				var y = (int)(splatter.transform.localPosition.y + height / 2);
				DrawFrom(x, y, splatter);
			}
		}

		mask.SetPixels(map);
		mask.Apply();
	}

	private void DrawFrom(int sourceX, int sourceY, SplatterComponent splatter)
	{
		if (splatter == null) return;

		var sprite = splatter.Splatter;
		if (sprite == null) return;
		var splatterWidth = (int)sprite.bounds.size.x;
		var splatterHeight = (int)sprite.bounds.size.y;

		// TODO si on veux separer le texture en plusieurs sprites, on doit 
		// seulement prend la zone du sprite
		var colors = splatter.Pixels;
		int centerX = sourceX - splatterWidth / 2;
		int centerY = sourceY - splatterHeight / 2;

		for (int x = 0; x < splatterWidth; x++)
		{
			for (int y = 0; y < splatterHeight; y++)
			{
				var index = (centerY + y) * width + centerX + x;
				if (index < 0 || index >= map.Length) continue;

				var pixel = map[index];
				var splatterPixel = colors[y * splatterWidth + x];
				map[index] = pixel + splatterPixel;
			}
		}
	}
}
