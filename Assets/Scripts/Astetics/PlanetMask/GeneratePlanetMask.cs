﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneratePlanetMask : MonoBehaviour {
	public String SplatterTag;
	Texture2D mask;
	int width;
	int height;

	float nextUpdate;
	public float timeBetweenUpdate = 1;

	void Start () {
		var sr = GetComponent<SpriteRenderer>();
		var ssize = sr.bounds.size;
		mask = new Texture2D((int)ssize.x, (int)ssize.y,TextureFormat.ARGB32,false);
		mask.filterMode = FilterMode.Point;
		sr.material.SetTexture("_MaskTex",mask);
		width = (int)ssize.x;
		height = (int)ssize.y;

		nextUpdate =  Chronos.Instance.CurrentTime + timeBetweenUpdate;
	}
	
	void Update ()
    {
		if(Time.time > nextUpdate){
			nextUpdate =  Chronos.Instance.CurrentTime + timeBetweenUpdate;
		}else{
			return;
		}

        MakeItBlack();

        var grass = GameObject.FindGameObjectsWithTag(SplatterTag);

        foreach (var g in grass)
        {
            var x = (int)(g.transform.position.x + width / 2);
            var y = (int)(g.transform.position.y + height / 2);
			var splatterElement = g.GetComponent<SplatterElementComponent>();
			var splater = splatterElement.getSplatter();
            DrawFrom(x, y, splater);
        }

        mask.Apply();
    }

	Color fillColor = new Color(0,0,0,0);
    private void MakeItBlack()
    {
        Color[] cs = new Color[width * height];
        for (int i = 0; i < cs.Length; i++)
            cs[i] = fillColor;
        mask.SetPixels(cs);
    }

    private void DrawFrom(int xc, int yc, Sprite splater)
    {
		var sw = (int)splater.bounds.size.x;
		var sh = (int)splater.bounds.size.y;
		// TODO si on veux separer le texture en plusieurs sprites, on doit 
		// seulement prend la zone du sprite
		var colors = splater.texture.GetPixels();
		int x0 = xc;// - width/2;
		int y0 = yc;// - height/2;
		for (int x = 0; x < sw; x++)
		{
			for (int y = 0; y < sh; y++)
			{
				var c = mask.GetPixel(x0 + x, y0 + y);
				var c2 = colors[y*sw + x];
				mask.SetPixel(x0 + x, y0 + y,c + c2);
			}
		}
       /* for (int x = xc - sw/2; x < xc + sw/2; x++)	
			for (int y = yc - sh/2; y < yc + sh/2; y++)
				mask.SetPixel(x,y,colors[y*sw + sh]);*/
    }
}