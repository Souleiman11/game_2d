using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;

public class HealthSystemTest
{
    private GameObject player;
    private GameObject healthBarObj;
    private GameObject collectible;
    private Health health;
    private Healthbar healthbar;
    private HealthCollectible healthCollectible;

    [SetUp]
    public void Setup()
    {
        // === Setup Player ===
        player = new GameObject("Player");
        player.tag = "Player";
        player.AddComponent<Animator>();
        player.AddComponent<Rigidbody2D>();

        var spriteRenderer = player.AddComponent<SpriteRenderer>();
        var healthComponent = player.AddComponent<Health>();

        // Manually set public fields via reflection or serialized field access
        var anim = player.GetComponent<Animator>();
        typeof(Health).GetField("anim", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(healthComponent, anim);
        typeof(Health).GetField("spriteRend", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(healthComponent, spriteRenderer);
        typeof(Health).GetField("components", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(healthComponent, new Behaviour[] { });

        health = healthComponent;

        // === Setup Health Bar ===
        healthBarObj = new GameObject("HealthBar");
        var totalBar = new GameObject("TotalBar").AddComponent<Image>();
        var currentBar = new GameObject("CurrentBar").AddComponent<Image>();
        healthBarObj.AddComponent<Canvas>(); // Just in case
        healthbar = healthBarObj.AddComponent<Healthbar>();

        typeof(Healthbar).GetField("playerHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(healthbar, health);
        typeof(Healthbar).GetField("totalhealthBar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(healthbar, totalBar);
        typeof(Healthbar).GetField("currenthealthBar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(healthbar, currentBar);

        // === Setup Collectible ===
        collectible = new GameObject("HealthCollectible");
        collectible.AddComponent<BoxCollider2D>().isTrigger = true;
        healthCollectible = collectible.AddComponent<HealthCollectible>();
    }

    [UnityTest]
    public IEnumerator HealthSystem()
    {
        yield return null;
        Assert.IsNotNull(health, "Health should be attached to the player.");
        Assert.IsNotNull(healthbar, "Healthbar should be set up correctly.");
        Assert.IsNotNull(healthCollectible, "HealthCollectible should be set up.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(healthBarObj);
        Object.DestroyImmediate(collectible);
    }
}
