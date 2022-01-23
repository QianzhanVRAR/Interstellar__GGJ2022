using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QZVR;
using TMPro;
public class StarTriggerTool : IControl
{
    public PlanetDetails details;
    public PlanetEnum Enum;
 

    private void OnMouseEnter()
    {
        StarVive.Instance.ShowData(details, transform.position);
    }
    private void OnMouseExit()
    {
        StarVive.Instance.HideData(details);
    }

    private void OnMouseDown()
    {
        DetailedVive.Instance.ShowOrHUi(this);
    }

    public override void Cinit()
    {
        details = this.GetModel<PlanetModel>().SetPlanetWordLocation(Enum, transform.position);
    }
}
