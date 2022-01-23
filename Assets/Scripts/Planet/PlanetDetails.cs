using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System .Serializable]
public struct PlanetDetails  : IEquatable<PlanetDetails>
{
    public string Name;
    public string Description;
    public Vector2Int Location;
    public Vector2 WordLocation;
    public bool Equals(PlanetDetails other)
    {
        return other.Name == Name && WordLocation==other .WordLocation;
    }
}
