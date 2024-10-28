using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem 
{
    protected override void ApplyModifier()
    {
        player.CurrentMoveSpeed *= passiveItemData.Multipler / 100f;
    }
}
