using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerMovementTest
{
    private GameObject player;
    private PlayerMovement playerMovement;

    [SetUp]
    public void Setup()
    {
        player = new GameObject("Player");

        // Required components
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<Animator>();
        player.AddComponent<BoxCollider2D>();

        // Add script
        playerMovement = player.AddComponent<PlayerMovement>();
    }

    [UnityTest]
    public IEnumerator MoveTest()
    {
        yield return null;
        Assert.IsNotNull(playerMovement, "PlayerMovement should be attached to the player.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
    }
}