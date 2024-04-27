using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackItem : OnHitItem
{
    private int force = 15;
    private int damage = 5;
    private int chanceToProc = 30;


    override
    public void OnHit(Enemy enemy)
   {
        System.Random random = new();
        int rand = random.Next(100);
        if (rand > chanceToProc)
            return;

        if (enemy.GetComponent<Rigidbody>())
        {
            enemy.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        }
        enemy.TakeDamage(damage);
   }
}
