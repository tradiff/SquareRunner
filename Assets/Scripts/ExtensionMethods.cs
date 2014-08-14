using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

    public static class ExtensionMethods
    {
        public static T RandomElement<T>(this List<T> list)
        {
            var r = Random.Range(0, list.Count);
            return list.Skip(r).FirstOrDefault();
        }

        public static T Choose<T>(this List<T> list) where T : IWeighted
        {
            if (list.Count == 0)
            {
                return default(T);
            }

            int totalweight = list.Sum(c => c.Weight);
            int choice = Random.Range(0, totalweight);
            int sum = 0;

            foreach (var obj in list)
            {
                for (int i = sum; i < obj.Weight + sum; i++)
                {
                    if (i >= choice)
                    {
                        return obj;
                    }
                }
                sum += obj.Weight;
            }

            return list.First();
        }
        public static IEnumerator WaitForRealSeconds(float time)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup < start + time)
            {
                yield return null;
            }
        }



    }

