using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace QZVR
{
    public class TimeSystem : AbstractSystem
    {
        ExploreSystem exploreSystem;

        public  float remt = 1;
        private  static  int[] dateTime ;
        private bool TimeRun = true;
        protected override void OnInit()
        {
            if (!Application.isPlaying) return;
            Application.quitting += Application_quitting;
            dateTime = GetTime();
            exploreSystem = this.GetSystem<ExploreSystem>();
            TimeUpdate();
        }

        private void Application_quitting()
        {
            TimeRun = false;
        }

        public async void TimeUpdate()
        {
            while (TimeRun)
            {
                await Task.Delay((int)(1000 / remt));
                dateTime[2] ++;
                if (dateTime[2] > 60)
                {
                    dateTime[2]= 0;
                    dateTime[1]++;
                }
                if (dateTime[1] > 60)
                {
                    dateTime[1] = 0;
                    dateTime[0]++;
                }
                if (dateTime[0] > 23)
                {
                    dateTime[0] = 0;
                }
                exploreSystem.UpdateExplore(dateTime);
            }
        }

        public static  int[] GetTime()
        {
            if (dateTime==null)
            {
                return new int[] { System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second  };
            }
            else
            {
                return new int[] { dateTime[0], dateTime[1], dateTime[2] };
            }
         
           
        }
    }
}