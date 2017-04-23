using System.Collections.Generic;
using UnityEngine;

public abstract class SplatterComponent : MonoBehaviour
{
	public readonly static Dictionary<string, List<SplatterComponent>> Splatters = new Dictionary<string, List<SplatterComponent>>();

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
		List<SplatterComponent> splatters;
		if (!Splatters.TryGetValue(transform.tag, out splatters))
		{
			splatters = new List<SplatterComponent>();
			Splatters[transform.tag] = splatters;
		}

		splatters.Add(this);
	}

	void OnDisable()
	{
		Splatters[transform.tag].Remove(this);
	}

}