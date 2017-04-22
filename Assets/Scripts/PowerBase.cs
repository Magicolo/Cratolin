using UnityEngine;

public abstract class PowerBase : MonoBehaviour
{
	public virtual bool CanUse { get { return RemainingUses == -1 || RemainingUses > 0; } }
	public abstract int RemainingUses { get; }

	public virtual bool CanPlace(Vector2 position) { return CanUse; }

	public abstract GameObject Create(Vector2 position);
}
