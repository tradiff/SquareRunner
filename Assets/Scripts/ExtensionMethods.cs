using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

    public static class ExtensionMethods
    {
        public static T RandomElement<T>(this List<T> q)
        {
            var r = Random.Range(0, q.Count);
            //q = q.Where(e);
            return q.Skip(r).FirstOrDefault();
        }

    }

