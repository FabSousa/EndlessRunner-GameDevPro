using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollide
{
    void Collide(Collider collider, GameMode gameMode);
}