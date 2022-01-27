using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace QZVR
{
    public class ExploreSystem :AbstractSystem
    {
        BattleshipModel BattleshipModel;
        ExploreModel BxploreModel;
        protected override void OnInit()
        {
            BattleshipModel=  this.GetModel<BattleshipModel>();
            BxploreModel = this.GetModel<ExploreModel>();
           
        }



        public  List<ExploreEventData> GetBattleshipExploreEvent(BattleshipAduentueData data)
        {
            List<ExploreEventData> Data = new List<ExploreEventData>();
            Data.Add(BxploreModel.GetStartEventData(data.StartPlanet.Name));

           

            for (int i = 0; i < data.EnergyExpended * 2 - 2; i++)
            {
               
            }
            Data.Add(BxploreModel.GetEndEventData()); 
            return null;
        }

        private int B1Reobabilityl=25;
        private int B2Reobabilityl=40;
        private int B3Reobabilityl=35;
       /* private ExploreEventData GetItemEventData(BattleshipAduentueData data)
        {
            int renge = ProbabilityTool();
            if (renge < B1Reobabilityl)
            {

            }
            else if (renge> B2Reobabilityl && renge< B1Reobabilityl+ B2Reobabilityl)
            {

            }
            else 
            { 

            }
          
        }*/

        private int  ProbabilityTool()
        {
            return Random.Range(0, 100);
        }

        public   void UpdateExplore(int [] time)
        {
            if (BattleshipModel.Detached.Count == 0) return;
            List<BattleshipAduentueData> Temp = new List<BattleshipAduentueData>();
            foreach (var item in BattleshipModel.Detached)
            {
                if (item.Progress.Value > 1)
                {
                    Temp.Add(item);
                }
                item.Progress.Value = GetProgress(time, item);
            }
            foreach (var item in Temp)
            {
                BattleshipModel.RetureBattlenships(item);
            }
        }

		public float  GetLightYear(Vector2Int planetVector)
        {
            //TODO��ɫ���ܽ��ٹ���
            return (planetVector - BattleshipModel.PitchOn.Location.Value).magnitude;
        }
        public float  GetEnergyConsumption(PlanetDetails details)
        {
            return GetLightYear(details.Location);
        }



        private float  GetProgress(int[] time,BattleshipAduentueData data)
        {

            float HDuration = data.m_DurationTime * 60;
            Debug.Log($"###########�շ֣�{(time[0] - data.m_DataTime[0]) * 24 * 60}" +
                $"����{(time[1] * 60 + time[2]) - ((data.m_DataTime[1]) * 60 + data.m_DataTime[2])}");
            float TempTime =
                ((time[0] - data.m_DataTime[0]) * 24 * 60) +
                ((time[1]*60+ time[2]) - ((data.m_DataTime[1]) * 60 + data.m_DataTime[2]));
            Debug.Log($"�ɴ�����{data.m_DataTime[0]}:{data.m_DataTime[1]}:{data.m_DataTime[2]}");
            Debug.Log($"Ŀ��ʱ����{HDuration}����ǰʱ����{TempTime}������{TempTime / HDuration}");
            return TempTime / HDuration;

        }


    }
}