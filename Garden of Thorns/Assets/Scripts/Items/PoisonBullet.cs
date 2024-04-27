using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonBullet : OnHitItem
{
    override
    public void OnHit(Enemy enemy)
    {
        enemy.AddComponent<PoisonDamage>();
    }
}
