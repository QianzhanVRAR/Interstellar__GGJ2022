using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace QZVR
{

    public struct ExploreEventData
    {
       public  ExploreEventEnum EventEnum;
        public string EventTop;
        public int MaxCount;
        public float HpEvent;
        public string EventDescribe;
        public float[] assets;
        public float TriggerTime;

        public ExploreEventData Dubug(bool LogE = false )
        {
            if (LogE)
            {

                Debug.LogError($"�����־���¼����ͣ�{EventEnum}," +
                    $"\r\n �¼����⣺{EventTop}��" +
                    $"\r\n �¼�������{EventDescribe}," +
                    $"\r\n �¼�����ʱ�䣺{TriggerTime}");
                return this;
            }

            Debug.LogWarning($"�����־���¼����ͣ�{EventEnum}," +
                $"\r\n �¼����⣺{EventTop}��" +
                $"\r\n �¼�������{EventDescribe}," +
                $"\r\n �¼�����ʱ�䣺{TriggerTime}");
            return this;
        }
    }
    public class ExploreModel :AbstractModel
    {
        //TODO����ʱ��Ļص�
        public Dictionary <List <ExploreEventData> ,int > ShowExploreEventData = new Dictionary <List<ExploreEventData>, int > (); 
		protected override void OnInit()
		{

		}



        public ExploreEventData GetExploreEventData(ExploreEventEnum  eventEnum)
        {

            SqliteDataReader data = SQLit.ReadTable("ExpeditionEvent", new string[] { "�¼�����", "�¼�����", "��������", "�¼�����", "��ֻӰ��", "���", "����", "��Ʒ" }, new string[] { "�¼�����" }, new string[] { "=" }, new string[] { $"'{eventEnum}'" });
            return SQLdatas_RanItemToClass(data);
        }

        public ExploreEventData GetStartEventData(string planetName )
        {
            Debug.LogWarning(planetName);
            SqliteDataReader  data = SQLit.ReadTable("ExpeditionEvent", new string[] { "�¼�����", "�¼�����", "��������", "�¼�����", "��ֻӰ��", "���" , "����", "��Ʒ" }, new string[] { "�¼�����" , "������������" }, new string[] { "=","=" }, new string[] { $"'{ExploreEventEnum.A}'",$"'{planetName}'" });
            return SQLdataToClass(data);

        }

        public ExploreEventData GetEndEventData()
        {

            SqliteDataReader data = SQLit.ReadTable("ExpeditionEvent", new string[] { "�¼�����", "�¼�����", "��������", "�¼�����", "��ֻӰ��", "���", "����", "��Ʒ" }, new string[] { "�¼�����" }, new string[] { "=" }, new string[] { $"'{ExploreEventEnum.C}'" });
            return SQLdataToClass(data);

        }

        private ExploreEventData SQLdataToClass(SqliteDataReader data)
        {
            ExploreEventData eventData = new ExploreEventData();
            while (data.Read ())
            {
             
                eventData.EventEnum = GetEventEnumType(data.GetString(data.GetOrdinal("�¼�����")));
                eventData.EventTop = data.GetString(data.GetOrdinal("�¼�����"));
                eventData.MaxCount = data.GetInt32(data.GetOrdinal("��������"));
                eventData.EventDescribe = data.GetString(data.GetOrdinal("�¼�����"));
                eventData.HpEvent = data.GetFloat(data.GetOrdinal("��ֻӰ��"));
                eventData.assets = new float[3];
                eventData.assets[0] = data.GetFloat(data.GetOrdinal("���"));
                eventData.assets[1] = data.GetFloat(data.GetOrdinal("����"));
                eventData.assets[2] = data.GetFloat(data.GetOrdinal("��Ʒ"));
                eventData.TriggerTime = 0;
                eventData.Dubug();
               
            }
            return eventData;
        }

        private ExploreEventData SQLdatas_RanItemToClass(SqliteDataReader data)
        {
            List<ExploreEventData> Alldata = new List<ExploreEventData>();
            while (data.Read())
            {
                ExploreEventData eventData = new ExploreEventData();
                eventData.EventEnum = GetEventEnumType(data.GetString(data.GetOrdinal("�¼�����")));
                eventData.EventTop = data.GetString(data.GetOrdinal("�¼�����"));
                eventData.MaxCount = data.GetInt32(data.GetOrdinal("��������"));
                eventData.EventDescribe = data.GetString(data.GetOrdinal("�¼�����"));
                eventData.HpEvent = data.GetFloat(data.GetOrdinal("��ֻӰ��"));
                eventData.assets = new float[3];
                eventData.assets[0] = data.GetFloat(data.GetOrdinal("���"));
                eventData.assets[1] = data.GetFloat(data.GetOrdinal("����"));
                eventData.assets[2] = data.GetFloat(data.GetOrdinal("��Ʒ"));
                eventData.TriggerTime = 0;
                eventData.Dubug();
                Alldata.Add (eventData); 
            }
           
            return Alldata[Random.Range(0, Alldata.Count)].Dubug(true );
        }

        private ExploreEventEnum GetEventEnumType(string name)
        {
            return (ExploreEventEnum)System.Enum.Parse(typeof(ExploreEventEnum), name);
        }




#if UNITY_EDITOR
        #region NewEnum
        public void GetExploreEventEnum()
        {
            string audioEnumFile = @"
namespace QZVR
{
    [System.Serializable]
    public enum ExploreEventEnum
    {
";
            var data = SQLit.ReadFullTable("ExpeditionEvent");
            List <string > type=new List<string>(); 
            while (data.Read())
            {
                var temp = data.GetString(data.GetOrdinal("�¼�����"));
                if (!type.Contains(temp))
                {
                    type.Add(temp);
                }
               
            }
            foreach (var item in type)
            {
                audioEnumFile += "        " + item + ",\n";
            }
            audioEnumFile += @"
    }
}";
            string filePath = Application.dataPath + "/Scripts/Explore/ExploreEventEnum.cs";
            File.WriteAllText(filePath, audioEnumFile);
            AssetDatabase.Refresh();
        }
        #endregion
#endif
    }
}