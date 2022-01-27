using QZVR;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : SerializedMonoBehaviour,IController

{
    [ShowInInspector]
    public  Dictionary<BattleshipEnum, List<Battleship>> AllBattleships ;
    [ShowInInspector]
    public Dictionary<CrewEnum, List<Crew>> AllCrewData ;

    [ShowInInspector]
    public  List<BattleshipAduentueData> Detached = new List<BattleshipAduentueData>();
    public IArchitecture GetArchitecture()
    {
     return InterstellarApp.Instance ;
    }

    private void Start()
    {
        if (ES3.KeyExists(BattleshipModel.AllBattleshipsKey))
        {

            AllBattleships = ES3.Load<Dictionary<BattleshipEnum, List<Battleship>>>(BattleshipModel.AllBattleshipsKey);
           
        }
        if (ES3.KeyExists(BattleshipModel.DetachedKey))
        {
            Detached = ES3.Load<List<BattleshipAduentueData>>(BattleshipModel.DetachedKey);
        }

       /* AllCrewData = ES3.Load<Dictionary<CrewEnum, List<Crew>>>("CrewData");
        foreach (var items in AllBattleships.Keys )
        {
            foreach (var item in AllBattleships[items])
            {
                Debug.Log(item.Details.Name);
            }
        }*/
    }
}
