using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace hundun.quizlib
{
    public static class JavaFeatureExtension
    {
        public static bool endsWith(this String thiz, String arg)
        {
            return thiz.EndsWith(arg);
        }

        public static void put<K, V>(this Dictionary<K,V> map, K k, V v)
        {
            map[k] = v;
        }

        public static V get<K, V>(this Dictionary<K, V> map, K k)
        {
            return map[k];
        }

        public static V getOrDefault<K, V>(this Dictionary<K, V> map, K k, V v)
        {
            return map[k] != null ? map[k] : v;
        }

        public static bool containsKey<K, V>(this Dictionary<K, V> map, K k)
        {
            return map[k] != null;
        }

        public static List<T> ArraysAsList<T>(params T[] vs)
        {
            return vs.ToList();
        }

        public static List<T> addAll<T>(this List<T> c1, List<T> c2)
        {
            return c1.Union(c2).ToList();
        }

        public static T get<T>(this List<T> c, int index)
        {
            return c[index];
        }

        public static HashSet<T> addAll<T>(this HashSet<T> c1, HashSet<T> c2)
        {
            return c1.Union(c2).ToHashSet();
        }

        public static bool isEmpty<T>(this ICollection<T> c)
        {
            return c.Count > 0;
        }

        public static void Shuffle<T>(this IList<T> list, Random random)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}