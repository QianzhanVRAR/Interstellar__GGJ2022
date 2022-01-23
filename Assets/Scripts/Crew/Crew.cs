using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Crew 
{
    public CrewDetails Details;
    public Crew(CrewDetails details )
    {
        Details = details;
    }
    public Crew()
    {
    }

    public Battleship battleship { get; private set; }


    public void SetBattleship(Battleship _battleship)
    {
        battleship= _battleship;
    }

}
