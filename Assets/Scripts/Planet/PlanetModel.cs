using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace QZVR
{

    
    public class PlanetModel :AbstractModel
    {

        public PlanetDetails StartPlanet
        {
            get
            {
                return GetPlanetData( PlanetEnum.spaceStation);
            }
        }


        public Dictionary <PlanetEnum, PlanetDetails> PlanetBufferMemory = new Dictionary<PlanetEnum, PlanetDetails>();
        protected override void OnInit()
		{

        }

        public PlanetDetails SetPlanetWordLocation(PlanetEnum planet, Vector2  vector2)
        {
            PlanetDetails data = GetPlanetData(planet) ;
            data.WordLocation = vector2;
            PlanetBufferMemory[planet]  = data;
            Debug.Log(data.WordLocation);
            Debug.Log( PlanetBufferMemory[planet].WordLocation);
            return data;
        }


        public PlanetDetails GetPlanetData(PlanetEnum planet)
        {
            if (PlanetBufferMemory.ContainsKey(planet))
            {
                return PlanetBufferMemory[planet] ;
            }

            PlanetDetails details = new PlanetDetails();
            var data = SQLit.ReadTable("PlanetTables", new string[] { "Name", "Description", "X", "Y"}, new string[] { "Name" }, new string[] { "=" }, new string[] { $"'{planet}'" });
            while (data.Read())
            {
                details.Name = data.GetString(data.GetOrdinal("Name"));
                details.Description  = data.GetString(data.GetOrdinal("Description"));
                details.Location.x   = data.GetInt32(data.GetOrdinal("X"));
                details.Location.y   = data.GetInt32(data.GetOrdinal("Y"));
            }
            PlanetBufferMemory.Add(planet, details);
            return details;
        }

        #region NewEnum
#if UNITY_EDITOR
        public void GetPlanetEnum()
        {
            string audioEnumFile = @"
namespace QZVR
{
    [System.Serializable]
    public enum PlanetEnum
    {
";
            var data = SQLit.ReadFullTable("PlanetTables");
            while (data.Read())
            {
                var temp = data.GetString(data.GetOrdinal("Name"));
                audioEnumFile += "        " + temp + ",\n";
            }
            audioEnumFile += @"
    }
}";
            string filePath = Application.dataPath + "/Scripts/Planet/PlanetEnum.cs";
            File.WriteAllText(filePath, audioEnumFile);
            AssetDatabase.Refresh();
        }
#endif
        #endregion
    }

}
