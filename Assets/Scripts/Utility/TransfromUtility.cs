using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QZVR
{
    public class TransfromUtility :IUtility
    {
        public  Transform FindChild(Transform parent, string name)
        {
           
            Transform child = null;
            child = parent.Find(name);
            if (child != null)
                return child;
            Transform grandchild = null;
            for (int i = 0; i < parent.childCount; i++)
            {
                grandchild = FindChild(parent.GetChild(i), name);
                if (grandchild != null)
                    return grandchild;
            }
            return null;
        }

        public  T FindChild<T>(Transform parent, string name) where T : Component
        {
            Transform child = null;
            child = FindChild(parent, name);
            if (child != null)
                return child.GetComponent<T>();
            return null;
        }
       
    }
}
