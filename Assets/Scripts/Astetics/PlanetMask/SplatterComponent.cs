using System.Collections.Generic;
using UnityEngine;

public abstract class SplatterComponent : MonoBehaviour
{
	public readonly static List<SplatterComponent> Splatters = new List<SplatterComponent>();

	public abstract Sprite Splatter { get; }
	public Color[] Pixels
	{
		get
		{
			var splatter = Splatter;

			if (splatter != lastSplatter && splatter != null)
			{
				pixels = splatter.texture.GetPixels();
				lastSplatter = splatter;
			}

			return pixels;
		}
	}

	Sprite lastSplatter;
	Color[] pixels = new Color[0];

	void OnEnable()
	{
		Splatters.Add(this);
	}

	void OnDisable()
	{
		Splatters.Remove(this);
	}
}