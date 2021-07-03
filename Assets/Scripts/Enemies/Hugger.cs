using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Hugger : Enemy
{
    protected override void Update()
    {
        if (Registry.PLAYER != null)
        {
            goal = Registry.PLAYER.transform.position;
        }

        base.Update();
    }
}
