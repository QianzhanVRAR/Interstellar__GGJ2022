﻿using System.Collections;
using UnityEngine;

namespace QZVR
{
    public interface  ICanGetModel :IBelongToArchitecture
    {

    }
    public static class GanGetModelExtension 
    {
        public static T GetModel<T>(this ICanGetModel self) where T :class ,IModel
        {
            return self.GetArchitecture().GetModel<T>();
        }
    
    }

}