using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QZVR;
public class AduentueSceneControl : ISceneControl
{
    protected override void Awake()
    {
        base.Awake();
        this.SendCommand<SetBattleshipWordLocationCommand>();
    }
}
