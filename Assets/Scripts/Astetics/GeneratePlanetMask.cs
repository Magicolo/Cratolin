using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneratePlanetMask : MonoBehaviour {

	public int ditterTest;
	Texture2D mask;
	int width;
	int height;

	float nextUpdate;
	public float timeBetweenUpdate = 1;

	void Start () {
		var sr = GetComponent<SpriteRenderer>();
		var ssize = sr.bounds.size;
		mask = new Texture2D((int)ssize.x, (int)ssize.y,TextureFormat.ARGB32,false);
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

        var grass = GameObject.FindGameObjectsWithTag("Grass");

        foreach (var g in grass)
        {
            var x = (int)(g.transform.position.x + width / 2);
            var y = (int)(g.transform.position.y + height / 2);
            DrawFrom(x, y, 200, 0.5f);
        }

        mask.Apply();
    }

    private void MakeItBlack()
    {
        Color[] cs = new Color[width * height];
        for (int i = 0; i < cs.Length; i++)
            cs[i] = Color.black;
        mask.SetPixels(cs);
    }

    private void DrawFrom(int xc, int yc, int size, float ditter)
    {
		int skip = (int)(1/ditter);
        for (int x = xc - size/2; x < xc + size/2; x+= 1)
		{
			int yskip = (int)((1f*x) % 2);
			//Debug.Log(x + " - " + yskip);
			for (int y = yc - size/2 + yskip; y < yc + size/2+yskip; y+=skip)
			{
				mask.SetPixel(x,y,Color.white);
			}
		}
    }
}
