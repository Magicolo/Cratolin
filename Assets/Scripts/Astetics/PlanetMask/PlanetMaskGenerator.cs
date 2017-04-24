using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMaskGenerator : MonoBehaviour 
{
	
	const float timeBetweenUpdate = 0.1f;

	public String SplatterTag;
	
	Texture2D mask;
	int width;
	int height;

	float nextUpdate;

	Color[] map;

	public Sprite testSplater;
	private Color[] testPixels;

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

		testPixels = testSplater.texture.GetPixels();
	}

	void Update()
	{
		if (Chronos.Instance.Time < nextUpdate)
			return;

		nextUpdate = Chronos.Instance.Time + timeBetweenUpdate;

		foreach (var item in Groups.Get<SectionPoint>())
		{
			if(item.lavaLevel >= 0.8f){
				var x = (int)(item.transform.localPosition.x + width / 2);
				var y = (int)(item.transform.localPosition.y + height / 2);
				Draw(x, y, testSplater,testPixels);
			}
		}
		
		mask.SetPixels(map);
		mask.Apply();
	}

	private void Draw(int sourceX, int sourceY, SplatterComponent splatter)
	{
		Draw(sourceX,sourceY,splatter.Splatter, splatter.Pixels);
	}

	private void Draw(int sourceX, int sourceY, Sprite sprite, Color[] colors)
	{
		if (sprite == null) return;
		var splatterWidth = (int)sprite.bounds.size.x;
		var splatterHeight = (int)sprite.bounds.size.y;

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