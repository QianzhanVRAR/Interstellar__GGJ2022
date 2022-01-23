using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QZVR
{
    public  class ISceneControl : AbstractController
    {
        public List<IControl> AllControl = new List<IControl>();

        protected virtual void Awake()
        {
            foreach (var item in FindObjectsOfType<IControl>())
            {
                AllControl.Add(item);
            }
            for (int i = 0; i < 10; i++)
            {
                foreach (var item in AllControl)
                {
                    if (item.Priority == i)
                    {
                        item.Cinit();
                    }
                }
            }
        }

     

    }


}
