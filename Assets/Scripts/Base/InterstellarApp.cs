using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QZVR;
public class InterstellarApp : Architecture<InterstellarApp>
{
    protected override void Init()
    {
        RegisteUtility(new TransfromUtility());
        RegisterModel(new PlayerModel ()) ;
        RegisterModel(new CrewModel());
        RegisterModel(new BattleshipModel());
        RegisterModel(new PlanetModel());
        RegisterModel(new ExploreModel());

        RegisterSystem(new TimeSystem());
        RegisterSystem(new BattleshipSystem());
        RegisterSystem(new ExploreSystem());

    }

   
}
