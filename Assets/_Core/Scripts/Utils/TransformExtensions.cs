using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public static class TransformExtensions
    {
        public static GameObject[] GetChilds(this Transform transform)
        {
            var list = getChilds(transform,false);
            return list.ToArray();
        }

        static List<GameObject> getChilds(Transform transform,bool includeParent = true)
        {
            var list = new List<GameObject>();
            if(includeParent)
                list.Add(transform.gameObject);
            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var childs = getChilds(transform.GetChild(i));
                    list.AddRange(childs);
                }
            }

            return list;
        }

        public static int Parents(this Transform transform)
        {
            int count = 0;
            Transform currentTransform = transform;
            while (true)
            {
                if (currentTransform.parent)
                {
                    count++;
                    currentTransform = currentTransform.parent;
                }
                else break;
            }

            return count;
        }


    }
}