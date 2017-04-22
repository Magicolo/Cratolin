using UnityEngine;

public abstract class PowerBase : MonoBehaviour
{
	public abstract GameObject Create(Vector2 position);
	public abstract bool CanPlace(Vector2 position);
}
