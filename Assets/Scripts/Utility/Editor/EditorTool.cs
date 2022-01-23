
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


    [MenuItem("Tools/CloseConnection", false, 2001)]
    public static void CloseConnection()
    {
        InterstellarApp.SQLite.CloseConnection ();
    }
    [MenuItem("Tools/CreatePlanetEnum", false, 2002)]
    public static void PlanetEnum()
    {
        InterstellarApp.Instance.GetModel<PlanetModel>().GetPlanetEnum();
    }

}
