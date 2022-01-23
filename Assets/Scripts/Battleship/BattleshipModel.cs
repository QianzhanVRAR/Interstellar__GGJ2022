using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace QZVR
{
    public class BattleshipModel : AbstractModel
    {
        [System.Serializable]
        struct BattleshipAduentueData
        {
            public float m_StartAduentueTime;
            public float m_DurationTime;
            public float EnergyExpended;
            public float DistanceTravelledEnergy;

            //TODO ��õ���Դ
            //TODO �ɴ���־
            public Battleship Battleship;
        }


        private Battleship m_TestPitchOn;
        public  Battleship PitchOn {
            get
            {
                    TraversalBattleships((data) => {
                        
                        m_TestPitchOn = data;
                    });
               
                return m_TestPitchOn;

            } }

        private Dictionary <BattleshipEnum, List<Battleship>>  AllBattleships = new  Dictionary<BattleshipEnum, List<Battleship>>();

        private List<BattleshipAduentueData> Detached = new List<BattleshipAduentueData>();



        public void TraversalBattleships(BattleshipEnum battleship, Action<Battleship> action)
        {
            foreach (var items in AllBattleships.Keys)
            {
                if (items != battleship) continue ;
                foreach (var item in AllBattleships[items])
                {
                    action(item);
                }
            }
        }

        public void TraversalBattleships(Action<Battleship> action)
        {
            foreach (var items in AllBattleships.Keys)
            {
                foreach (var item in AllBattleships[items])
                {
                    action(item);
                }
            }
        }

        public void AddBattlenships(BattleshipEnum battleship )
        {
            BattleshipDetails details = GetCrewData(battleship);
            List<Battleship> tempList;
            if (!AllBattleships.TryGetValue (battleship ,out tempList))
            {
                tempList = new List<Battleship>();
                AllBattleships.Add(battleship, tempList);
            }
            Battleship temp = new Battleship(details);
            tempList.Add(temp);
            SaveBattlenships();
        }

        public void  DispatchBattlenship(BattleshipEnum battleship,float _startTime,float _durationTime,float _energyExpended, float _distanceTravelledEnergy)
        {
          
            if (AllBattleships[battleship].Count== 0)
            {
                Debug.LogError("�����ڸô�");
                return;
            }

            BattleshipAduentueData data = new BattleshipAduentueData();
            data.m_StartAduentueTime = _startTime;
            data.m_DurationTime = _durationTime;
            data.EnergyExpended = _energyExpended;
            data.DistanceTravelledEnergy = _distanceTravelledEnergy;
            data.Battleship = AllBattleships[battleship][0];
            Detached.Add(data);

            if (data.Battleship.Crews .Count == 0)
            {
                Crew temp = this.GetModel<CrewModel>().GetCrew();
                data.Battleship.Crews.Add(temp);
                temp.SetBattleship(data.Battleship);
            }

            SaveDetachedBattleships();
        }


        public void Save()
        {
            SaveBattlenships();
            SaveDetachedBattleships();
        }
        public  void SaveBattlenships()
        {
            ES3.Save("IdleBattleships", AllBattleships);
           
        }

        public void SaveDetachedBattleships()
        {
            ES3.Save("DetachedBattleships", Detached);
        }


        protected override void OnInit()
		{
            LoadBattlenships();
        }


        private void LoadBattlenships()
        {
            if (!ES3.KeyExists("IdleBattleships")) return;
            AllBattleships = ES3.Load < Dictionary< BattleshipEnum, List <Battleship>>> ("IdleBattleships");
        }

        private BattleshipDetails GetCrewData(BattleshipEnum crew)
        {
            BattleshipDetails details = new BattleshipDetails();

            var data = SQLit.ReadTable("BattleshipDataTables", new string[] { "����", "�������޼ӳ�", "���", "����", "����", "�ٶ�", "����" }, new string[] { "����" }, new string[] { "=" }, new string[] { $"'{crew}'" });
            while (data.Read())
            {
                details.Name = data.GetString(data.GetOrdinal("����"));
                details.ADDMaxEnergy  = data.GetInt32(data.GetOrdinal("�������޼ӳ�"));
                details.price  = data.GetFloat(data.GetOrdinal("���"));
                details.HP  = data.GetInt32(data.GetOrdinal("����"));
                details.Luck = data.GetInt32(data.GetOrdinal("����"));

                details.Speed  = data.GetInt32(data.GetOrdinal("�ٶ�"));
                details.Skill = data.GetInt32(data.GetOrdinal("����"));
            }
            return details;
        }


        #region NewEnum
#if UNITY_EDITOR
        public void GetBattleshipEnum()
        {
            string audioEnumFile = @"
namespace QZVR
{
    [System.Serializable]
    public enum BattleshipEnum
    {
";
            var data = SQLit.ReadFullTable("BattleshipDataTables");
            while (data.Read())
            {
                var temp = data.GetString(data.GetOrdinal("����"));
                audioEnumFile += "        " + temp + ",\n";
            }
            audioEnumFile += @"
    }
}";
            string filePath = Application.dataPath + "/Scripts/Battleship/BattleshipEnum.cs";
            File.WriteAllText(filePath, audioEnumFile);
            AssetDatabase.Refresh();
        }
        #endif
    #endregion
    }


}
