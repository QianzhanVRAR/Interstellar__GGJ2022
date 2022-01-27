
using QZVR;
using UnityEditor;
using UnityEngine;
public class CreateCrewEnum
{
    [MenuItem("Tools/CreateCrewEnum", false, 1999)]
    public static void CrewEnum()
    {
        InterstellarApp.Instance.GetModel<CrewModel>().GetCrewEnum();
    }

    [MenuItem("Tools/CreateBattleshipEnum", false, 2000)]
    public static void BattleshipEnum()
    {
        InterstellarApp.Instance.GetModel<BattleshipModel>().GetBattleshipEnum();
    }

    [MenuItem("Tools/CreatePlanetEnum", false, 2002)]
    public static void PlanetEnum()
    {
        InterstellarApp.Instance.GetModel<PlanetModel>().GetPlanetEnum();
    }

    [MenuItem("Tools/ExploreEventEnum", false, 2003)]
    public static void ExploreEventEnum()
    {
        InterstellarApp.Instance.GetModel<ExploreModel>().GetExploreEventEnum();
    }



    [MenuItem("Tools/CloseConnection", false, 3000)]
    public static void CloseConnection()
    {
        InterstellarApp.SQLite.CloseConnection();
    }

}
