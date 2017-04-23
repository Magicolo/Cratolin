using UnityEngine;
using UnityEngine.UI;

public class WhiteScreen : MonoBehaviour
{
	public static WhiteScreen Instance { get; private set; }

	public Image Image;

	public void Fade(float alpha)
	{
		var color = Image.color;
		color.a = alpha;
		Image.color = color;
	}

	void Awake()
	{
		Instance = this;
	}
}
