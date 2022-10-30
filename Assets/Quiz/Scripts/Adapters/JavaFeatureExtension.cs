using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace hundun.quizlib
{
    public static class JavaFeatureExtension
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

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
            return map.ContainsKey(k) ? map[k] : v;
        }

        public static bool containsKey<K, V>(this Dictionary<K, V> map, K k)
        {
            return map.ContainsKey(k);
        }

        public static List<T> ArraysAsList<T>(params T[] vs)
        {
            return vs.ToList();
        }

        public static T get<T>(this List<T> c, int index)
        {
            return c[index];
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