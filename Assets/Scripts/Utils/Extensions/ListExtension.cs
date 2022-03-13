using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Extensions
{
    public static class ListExtension 
    {
        private static Random rng = new Random();

        public static List<T> Shuffle<T>(this IList<T> list)
		{
            return list.OrderBy(a => rng.Next()).ToList();
        }
    }
}
