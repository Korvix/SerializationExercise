using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSerializer : GameObjectSerializer
{
    private static readonly string PLAYER_PREFAB_PATH = "Prefabs/Player";
    private PlayerStats playerStats;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        playerRigidbody = player.GetComponent<Rigidbody>();
    }
    public override void CreateObject(Save save)
    {
        playerStats.transform.position = save.playerEntity.position;
        playerStats.transform.rotation = Quaternion.Euler(save.playerEntity.rotation);
        playerRigidbody.velocity = save.playerEntity.velocity;
        playerStats.SetPoints(save.playerEntity.points);
    }

    public override Save Serialize(Save save)
    {
        save.playerEntity = CreateEntity();
        return save;
    }

    private PlayerEntity CreateEntity()
    {
        PlayerEntity playerEntity = new PlayerEntity();
        playerEntity.points = playerStats.GetPoints();
        playerEntity.velocity = playerRigidbody.velocity;
        playerEntity.position = playerStats.transform.position;
        playerEntity.rotation = playerStats.transform.rotation.eulerAngles;
        return playerEntity;
    }
}
