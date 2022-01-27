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
    [System.Serializable]
    public struct BattleshipAduentueData
    {

        public int[] m_DataTime;
        public float  m_DurationTime;
        public int EnergyExpended;
        public BindableProperty <float>  Progress;
        public PlanetDetails StartPlanet;
        public PlanetDetails EndPlanet;
        public  List <ExploreEventData> LogData;
        public Battleship Battleship;

    }
    public class BattleshipModel : AbstractModel
    {
       
        public static readonly string AllBattleshipsKey = "AllBattleships";
        public static readonly string AllBattleshipsCountKey = "AllBattleshipsCount";
        public static readonly string DetachedKey = "Detached";

        private Battleship m_TestPitchOn;
        public  Battleship PitchOn {
            get
            {
                    TraversalBattleships((data) => {
                        
                        m_TestPitchOn = data;
                    });
               
                return m_TestPitchOn;

            } }

        public int AllBattleshipsCount;

        public Dictionary <BattleshipEnum, List<Battleship>>  AllBattleships = new  Dictionary<BattleshipEnum, List<Battleship>>();

        public  List<BattleshipAduentueData> Detached = new List<BattleshipAduentueData>();

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



        public void RetureBattlenships(BattleshipAduentueData data)
        {
            data.Battleship.SetArrivePlanet(data.EndPlanet);
            RemoveDetached(data);
            AddBattlenships(data.Battleship);
            this.SendEvent(new BattleshipEvent.ReturnDispatchBattlenship() { data = data, Count = Detached.Count });
        }


        public void AddBattlenships(BattleshipEnum battleship)
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
            AllBattleshipsCount++;
            this.SendEvent(new BattleshipEvent.AddBattleship() { BattleshipEnum  = battleship, Battleship= temp ,Count = AllBattleshipsCount});
            SaveBattlenships();
        }
        public void AddBattlenships(Battleship battleship)
        {
            BattleshipEnum battleshipEnum = GetBlattlenshipType(battleship);
            List<Battleship> tempList;
            if (!AllBattleships.TryGetValue(battleshipEnum, out tempList))
            {
                tempList = new List<Battleship>();
                AllBattleships.Add(battleshipEnum, tempList);
            }
            tempList.Add(battleship);
            AllBattleshipsCount++;
            this.SendEvent(new BattleshipEvent.AddBattleship() { BattleshipEnum = battleshipEnum, Battleship = battleship, Count = AllBattleshipsCount });
            SaveBattlenships();
        }


        public void AddDetached(BattleshipAduentueData data)
        {
            Detached.Add(data);
            this.SendEvent(new BattleshipEvent.DispatchBattlenship() { data = data,Count = Detached.Count  });
            SaveDetachedBattleships();
        }


        public void RemoveBattlenships(Battleship battleship)
        {
            AllBattleships[GetBlattlenshipType(battleship)].Remove(battleship);
            AllBattleshipsCount--;
            this.SendEvent(new BattleshipEvent.RemoveBattleship() { BattleshipEnum = GetBlattlenshipType(battleship), Battleship = battleship, Count = AllBattleshipsCount });
            SaveBattlenships();
        }

        public void RemoveDetached(BattleshipAduentueData data)
        {
            Detached.Remove(data);
            this.SendEvent(new BattleshipEvent.RemoveDispatchBattlenship() { data = data, Count = Detached.Count });
            SaveDetachedBattleships();
        }

        public   BattleshipEnum GetBlattlenshipType(Battleship battleship)
        {
            return (BattleshipEnum)System.Enum.Parse(typeof(BattleshipEnum), battleship.Details.Name);
        }
        public List<Battleship> GetBattleships(BattleshipEnum battleship)
        {
            return AllBattleships[battleship];
        }



        public void Save()
        {
            SaveBattlenships();
            SaveDetachedBattleships();
        }
        public  void SaveBattlenships()
        {
            ES3.Save(AllBattleshipsKey, AllBattleships);
            ES3.Save(AllBattleshipsCountKey, AllBattleshipsCount);
        }

        public void SaveDetachedBattleships()
        {
            ES3.Save(DetachedKey, Detached);
        }

        private void LoadBattlenships()
        {
            if (ES3.KeyExists(AllBattleshipsKey))
            {
                AllBattleships = ES3.Load<Dictionary<BattleshipEnum, List<Battleship>>>(AllBattleshipsKey);
            }
            if (ES3.KeyExists(DetachedKey))
            {
                Detached = ES3.Load<List<BattleshipAduentueData>>(DetachedKey);
            }
            if (ES3.KeyExists(AllBattleshipsCountKey))
            {
                AllBattleshipsCount = ES3.Load<int  >(AllBattleshipsCountKey);
            }
        }

        protected override void OnInit()
		{
            LoadBattlenships();
        }


       
        private BattleshipDetails GetCrewData(BattleshipEnum crew)
        {
            BattleshipDetails details = new BattleshipDetails();

            var data = SQLit.ReadTable("BattleshipDataTables", new string[] { "名称", "能量上限加成", "造价", "生命", "技能", "速度", "运气" }, new string[] { "名称" }, new string[] { "=" }, new string[] { $"'{crew}'" });
            while (data.Read())
            {
                details.Name = data.GetString(data.GetOrdinal("名称"));
                details.ADDMaxEnergy  = data.GetInt32(data.GetOrdinal("能量上限加成"));
                details.price  = data.GetFloat(data.GetOrdinal("造价"));
                details.HP  = data.GetInt32(data.GetOrdinal("生命"));
                details.Luck = data.GetInt32(data.GetOrdinal("运气"));

                details.Speed  = data.GetInt32(data.GetOrdinal("速度"));
                details.Skill = data.GetInt32(data.GetOrdinal("技能"));
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
                var temp = data.GetString(data.GetOrdinal("名称"));
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
