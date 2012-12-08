using System;
using System.Collections.Generic;
using System.Linq;

namespace Fusion
{
	public static class Extensions
	{
		public static T Last<T>(this T[] arr)
		{
			return arr[arr.Length - 1];
		}

		public static T Last<T>(this List<T> lst)
		{
			return lst[lst.Count - 1];
		}

		public static bool TrueForAll<T>(this IEnumerable<T> coll, Func<T, bool> fc)
		{
			foreach (T t in coll)
				if (!fc(t))
					return false;
			return true;
		}

		public static void ForEach<T>(this IEnumerable<T> coll, Action<T> action)
		{
			foreach (T t in coll)
				action(t);
		}

		public static Stack<T> Duplicate<T>(this Stack<T> stack)
		{
			T[] elements = stack.ToArray();
			Array.Reverse(elements);
			return new Stack<T>(elements);
		}
	}
}
