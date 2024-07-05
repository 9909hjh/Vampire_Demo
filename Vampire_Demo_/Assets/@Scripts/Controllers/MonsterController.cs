using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{
    public override bool Init()
    {
       if(base.Init())
            return false;

        _objectType = ObjectType.Monster;

       return true;
    }

    void Update()
    {
        
    }
}
