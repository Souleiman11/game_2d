using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerAttackTest
{
    private GameObject player;
    private PlayerAttack playerAttack;

    [SetUp]
    public void Setup()
    {
        player = new GameObject("Player");
        player.AddComponent<Animator>();
        player.AddComponent<MockPlayerMovement>();
        playerAttack = player.AddComponent<PlayerAttack>();
    }

    [UnityTest]
    public IEnumerator AttackTest()
    {
        yield return null;
        Assert.IsNotNull(playerAttack, "PlayerAttack should be attached to the player.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
    }

    private class MockPlayerMovement : MonoBehaviour
    {
        public bool canAttack() => true;
    }
}
