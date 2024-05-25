using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitpoints = 5;
    [SerializeField] int difficultyRamp = 1;
    int currentHitpoints = 0;

    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitpoints = maxHitpoints;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitpoints--;

        if (currentHitpoints <= 0)
        {
            gameObject.SetActive(false);
            maxHitpoints += difficultyRamp;
            enemy.EnemyDeath();
        }
    }
}
