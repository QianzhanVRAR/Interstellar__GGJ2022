using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QZVR;
using TMPro;
public class StarTriggerTool : IControl
{
    public PlanetDetails details;
    public PlanetEnum Enum;
    public bool Action =true ;

    
    private void OnMouseEnter()
    {
        if (Action)
        StarVive.Instance.ShowData(details, transform.position);
    }
    private void OnMouseExit()
    {
        if (Action)
            StarVive.Instance.HideData(details);
    }

    private void OnMouseDown()
    {
        if (Action)
            DetailedVive.Instance.ShowOrHUi(this);
    }

    public override void Cinit()
    {
        BattleshipModel battleship = this.GetModel<BattleshipModel>();
        details = this.GetModel<PlanetModel>().SetPlanetWordLocation(Enum, transform.position);
        if (battleship.AllBattleshipsCount == 0)
        {
            Action = false;
        }
        this.RegisterEvent<BattleshipEvent.AddBattleship>((data => { 
            if (data.Count > 0)
            {
                Action = true ;
            }
        })).UnRegisterWhenGameObjectDestroyed(gameObject);

        this.RegisterEvent<BattleshipEvent.RemoveBattleship>((data => {
            if (data.Count == 0)
            {
                Action = false ;
            }
        })).UnRegisterWhenGameObjectDestroyed(gameObject);
    }
}
