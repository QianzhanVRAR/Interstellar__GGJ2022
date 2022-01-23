using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QZVR;

[System .Serializable]
public class Battleship 
{

    public   BattleshipDetails m_Details;
    public bool Init = false;
    public bool OnIdle = true;
    public BindableProperty<PlanetDetails> OnPlanet = new BindableProperty<PlanetDetails>();
    public BindableProperty<Vector2Int> Location = new BindableProperty<Vector2Int>();



    public BattleshipDetails Details { get => m_Details; }

    

    public Battleship(BattleshipDetails details)
    {
        m_Details = details;
    }

    public void InitPlanet(PlanetDetails planet)
    {
        OnPlanet.Value = planet ;
        Location .Value = planet.Location ;
        Init = true;
    }

    public void SetToPlanet(PlanetDetails planet)
    {
        Debug.Log(planet.Location);
        OnPlanet.Value = planet;
        Location.Value = planet.Location;
    }
    public Battleship()
    {

    }
    public List<Crew> Crews { get; private set; }  = new List<Crew>();

}
