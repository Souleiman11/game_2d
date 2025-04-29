using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class RangedEnemyTest
{
    private GameObject enemy;
    private RangedEnemy rangedEnemy;

    [SetUp]
    public void Setup()
    {
        // Create enemy GameObject
        enemy = new GameObject("RangedEnemy");
        enemy.transform.localScale = Vector3.one;

        // Add required components
        var animator = enemy.AddComponent<Animator>();
        var boxCollider = enemy.AddComponent<BoxCollider2D>();
        var enemyPatrolObj = new GameObject("EnemyPatrol");
        var enemyPatrol = enemyPatrolObj.AddComponent<EnemyPatrol>();
        enemy.transform.SetParent(enemyPatrolObj.transform);

        // Add RangedEnemy component
        rangedEnemy = enemy.AddComponent<RangedEnemy>();

        // Assign fields using reflection
        typeof(RangedEnemy).GetField("anim", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(rangedEnemy, animator);
        typeof(RangedEnemy).GetField("boxCollider", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(rangedEnemy, boxCollider);
        typeof(RangedEnemy).GetField("enemyPatrol", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(rangedEnemy, enemyPatrol);

        // Create dummy fireballs
        GameObject[] fireballs = new GameObject[1];
        fireballs[0] = new GameObject("Fireball");
        fireballs[0].SetActive(false);
        fireballs[0].AddComponent<EnemyProjectile>();

        Transform firepoint = new GameObject("Firepoint").transform;
        firepoint.parent = enemy.transform;

        typeof(RangedEnemy).GetField("fireballs", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(rangedEnemy, fireballs);
        typeof(RangedEnemy).GetField("firepoint", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(rangedEnemy, firepoint);
    }

    [UnityTest]
    public IEnumerator RangedEnemyTestEnum()
    {
        yield return null;
        Assert.IsNotNull(rangedEnemy, "RangedEnemy script should be attached.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(enemy);
    }
}
