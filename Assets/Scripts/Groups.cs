using System.Collections.Generic;

public class Groups
{
	static class Storage<T>
	{
		public static readonly List<T> Elements = new List<T>();
	}

	public static void Add<T>(T element) { Storage<T>.Elements.Add(element); }
	public static bool Remove<T>(T element) { return Storage<T>.Elements.Remove(element); }
	public static List<T> Get<T>() { return Storage<T>.Elements; }
}
