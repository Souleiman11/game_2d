using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MeleeEnemyTest
{
    private GameObject enemy;
    private GameObject player;
    private MeleeEnemy meleeEnemy;

    [SetUp]
    public void Setup()
    {
        // Create enemy GameObject
        enemy = new GameObject("MeleeEnemy");
        enemy.transform.localScale = Vector3.one;

        // Add required components to enemy
        var animator = enemy.AddComponent<Animator>();
        var boxCollider = enemy.AddComponent<BoxCollider2D>();
        var enemyPatrolObj = new GameObject("EnemyPatrol");
        var enemyPatrol = enemyPatrolObj.AddComponent<EnemyPatrol>();
        enemy.transform.SetParent(enemyPatrolObj.transform);

        // Add MeleeEnemy component
        meleeEnemy = enemy.AddComponent<MeleeEnemy>();

        // Assign fields using reflection
        typeof(MeleeEnemy).GetField("anim", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(meleeEnemy, animator);
        typeof(MeleeEnemy).GetField("boxCollider", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(meleeEnemy, boxCollider);
        typeof(MeleeEnemy).GetField("enemyPatrol", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(meleeEnemy, enemyPatrol);

        // Create player GameObject
        player = new GameObject("Player");
        var playerHealth = player.AddComponent<Health>();
        playerHealth.startingHealth = 10;

        // Set player to be in front of the enemy
        player.transform.position = new Vector3(2, 0, 0);
        player.layer = LayerMask.NameToLayer("Player");

        // Assign player layer to enemy
        typeof(MeleeEnemy).GetField("playerLayer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(meleeEnemy, LayerMask.GetMask("Player"));
    }

    [UnityTest]
    public IEnumerator MeleeEnemyAttacksWhenPlayerInSight()
    {
        // Simulate the enemy detecting the player
        meleeEnemy.gameObject.transform.position = new Vector3(0, 0, 0);  // Enemy position

        // Run for one frame to test
        yield return null;

        // Ensure that melee attack trigger is called (check if animation trigger was set)
        Assert.IsTrue(meleeEnemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("meleeAttack"), 
        "Melee attack animation should be triggered when player is in sight.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(enemy);
        Object.DestroyImmediate(player);
    }
}