using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendAirplanePooling : GenericPooling
{
    private static FriendAirplanePooling instance;
    public static FriendAirplanePooling Instance
    {
        get
        {
            if (FriendAirplanePooling.instance == null)
            {
                FriendAirplanePooling.instance = GameObject.Find("Generic Pooling").GetComponent<FriendAirplanePooling>();
            }

            return FriendAirplanePooling.instance;
        }
    }

    [SerializeField]
    private List<FriendAirplaneSpawn> spawnFriendAirplanePoints;

    /// <summary>
    /// BALANCE: Spawn de aviões amigos a cada 21 segundos.
    /// </summary>
    private float spawnTime = 21f;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        base.Initialize();

        if (spawnFriendAirplanePoints == null || spawnFriendAirplanePoints.Count == 0)
        {
            Debug.LogError("There are no spawn points available!");
        }

        InvokeRepeating("SpawnFriendAirplane", spawnTime, spawnTime);
    }

    private void SpawnFriendAirplane()
    {
        int index = Random.Range(0, spawnFriendAirplanePoints.Count);

        var friendAirplaneSpawn = spawnFriendAirplanePoints[index];
        friendAirplaneSpawn.ReverseTranslate();

        SpawnFriendAirplaneFromPool(friendAirplaneSpawn.transform.position, friendAirplaneSpawn.side);
    }

    public GameObject SpawnFriendAirplaneFromPool(Vector2 position, MovementSide side)
    {
        GameObject shot = GetObjectFromPool(position);

        if (shot != null)
        {
            if (side == MovementSide.LEFTorDOWN)
            {
                shot.GetComponent<FriendAirplane>().side = MovementSide.RIGHTorUP;
                shot.transform.localScale = new Vector2(1, 1);
            }
            else if (side == MovementSide.RIGHTorUP)
            {
                shot.GetComponent<FriendAirplane>().side = MovementSide.LEFTorDOWN;
                shot.transform.localScale = new Vector2(-1, 1);
            }

            shot.renderer.sortingLayerName = "Foreground";
            shot.renderer.sortingOrder = 2;
        }

        return shot;
    }
}
