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
  
    public class CrewModel : AbstractModel
    {
        public  Dictionary <CrewEnum,List <Crew> > AllCrewData = new Dictionary<CrewEnum, List<Crew>>();

        protected override void OnInit()
        {
            LoadCrewData();
        }


        public Crew GetCrew()
        {
            foreach (var items in AllCrewData.Keys)
            {
                foreach (var item in AllCrewData[items])
                {
                    return item;
                }
            }
            Debug.LogError("角色初始化失败，字典内无可用角色");
            return null;
        }

        public void AddCrew(CrewEnum crew)
        {
            List<Crew> temp;
            if (!AllCrewData.TryGetValue(crew,out temp))
            {
                temp = new List<Crew>();
                AllCrewData.Add (crew,temp);
            }
            Crew crewInfo = new Crew(GetCrewData(crew));
            temp.Add(crewInfo);
            SaveCrewData();
        }

        private void SaveCrewData()
        {
            ES3.Save("CrewData", AllCrewData);
        }
        private void LoadCrewData()
        {
            if (!ES3.KeyExists("CrewData")) return;
            AllCrewData = ES3.Load<Dictionary<CrewEnum, List<Crew>>>("CrewData");
        }

        private CrewDetails GetCrewData(CrewEnum crew)
        {
            CrewDetails details=new CrewDetails ();

            var data = SQLit.ReadTable("CrewDataTables", new string[] { "Name", "Health", "Skill", "Speed", "Luck", "BattleArts" }, new string[] { "Name" }, new string[] { "=" }, new string[] { $"'{crew.ToString ()}'" });
            while (data.Read())
            {
                details.Name = data.GetString(data.GetOrdinal("Name"));
                details.Health = data.GetFloat(data.GetOrdinal("Health"));
                details.Skill = data.GetFloat(data.GetOrdinal("Skill"));
                details.Speed = data.GetFloat(data.GetOrdinal("Speed"));
                details.Luck = data.GetFloat(data.GetOrdinal("Luck"));
                details.BattleArts = data.GetString(data.GetOrdinal("BattleArts"));
            }
            return details;
        }
        # if UNITY_EDITOR
        #region NewEnum
        public  void GetCrewEnum()
        {
            string audioEnumFile = @"
namespace QZVR
{
    [System.Serializable]
    public enum CrewEnum
    {
";
            var data= SQLit.ReadFullTable("CrewDataTables");
            while (data.Read())
            {
                  var temp=  data.GetString(data.GetOrdinal("Name"));
                  audioEnumFile+=  "        " + temp + ",\n";
            }
      /*      for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                string extensionName = Path.GetExtension(files[i].Name);
                extensionName = extensionName.ToLower();
                if (extensionName == ".mp3" || extensionName == ".aiff" || extensionName == ".wav" || extensionName == ".ogg")
                {
                    string path = files[i].DirectoryName.Substring(Application.dataPath.Length + 17);
                    string[] paths = path.Split('\\');
                    string temp = "";
                    for (int j = 0; j < paths.Length; j++)
                    {
                        temp += paths[j] + "_";
                    }
                    temp += Path.GetFileNameWithoutExtension(files[i].Name);
                    audioEnumFile += "        " + temp + ",\n";
                }
            }*/
            audioEnumFile += @"
    }
}";
            string filePath = Application.dataPath + "/Scripts/Crew/CrewEnum.cs";
            File.WriteAllText(filePath, audioEnumFile);
            AssetDatabase.Refresh();
        }
        #endregion
#endif
    }


}










