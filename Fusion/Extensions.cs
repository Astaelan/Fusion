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

		public static T[] Slice<T>(this T[] lst, int sIdx, int eIdx)
		{
			T[] ret = new T[eIdx - sIdx];
			for (int i = sIdx, i2 = 0; i < eIdx; i++, i2++)
			{
				ret[i2] = lst[i];
			}
			return ret;
		}

		public static List<T> Slice<T>(this List<T> lst, int sIdx, int eIdx)
		{
			List<T> ret = new List<T>(eIdx - sIdx);
			for (int i = sIdx; i < eIdx; i++)
			{
				ret.Add(lst[i]);
			}
			return ret;
		}

		public static List<T> Slice<T>(this IEnumerable<T> coll, int sIdx, int eIdx)
		{
			List<T> ret = new List<T>(eIdx - sIdx);
			int i = sIdx;
			int i2 = 0;
			foreach (T t in coll)
			{
				if (i >= i2)
				{
					ret.Add(t);
					i++;
				}
				i2++;
				if (i2 == eIdx) break;
			}
			return ret;
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
