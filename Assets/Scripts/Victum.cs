using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VictumTypes
{
    BigLady,
    TinyLady,
    TinyMan,
    BigMan,
}


public class Victum
{
    public Transform body;
    public Transform spawnPoint;
    public VictumTypes type;
}