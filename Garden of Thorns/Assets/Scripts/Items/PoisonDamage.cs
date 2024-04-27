using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{
    [SerializeField] private GameObject vfx;
    private Enemy enemy;
    private int damage = 2;
    private float timeOfEffect = 3;
    private float timeBetweenTicks = 0.4f;
    GameObject poisonFX;

    // Start is called before the first frame update
    void Start()
    {
        vfx = Resources.Load<GameObject>("Prefabs/VFX/PoisonFX");
        enemy = gameObject.GetComponent<Enemy>();
        InvokeRepeating("ApplyPoisonDamage", 0f, timeBetweenTicks);
        poisonFX = Instantiate(vfx, enemy.transform.position, Quaternion.identity);
        poisonFX.transform.SetParent(gameObject.transform, true);
        if (enemy is BossController)
        {
            poisonFX.transform.localScale = new Vector3(2, 2, 2);
        }
        poisonFX.GetComponent<ParticleSystem>().Play(true);
    }

    // Update is called once per frame


    void ApplyPoisonDamage()
    {
        if (timeOfEffect >= 0 && enemy.currentHealth > 0)
        {
            enemy.TakeDamage(damage);
            timeOfEffect -= timeBetweenTicks;
        }
        else
        {
            Destroy(this);
        }
    }
}
