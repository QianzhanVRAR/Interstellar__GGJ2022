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

    public IArchitecture GetArchitecture()
    {
     return InterstellarApp.Instance ;
    }

    private void Start()
    {
        AllBattleships = ES3.Load<Dictionary<BattleshipEnum, List<Battleship>>>("IdleBattleships");
        AllCrewData = ES3.Load<Dictionary<CrewEnum, List<Crew>>>("CrewData");
        foreach (var items in AllBattleships.Keys )
        {
            foreach (var item in AllBattleships[items])
            {
                Debug.Log(item.Details.Name);
            }
        }
    }
}
