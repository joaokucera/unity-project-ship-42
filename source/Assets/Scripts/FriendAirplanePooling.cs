using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendAirplanePooling : GenericPooling
{
    [SerializeField]
    private List<FriendAirplaneSpawn> spawnFriendAirplanePoints;
    public float spawnTime = 5f;

    void Start()
    {
        if (spawnFriendAirplanePoints == null || spawnFriendAirplanePoints.Count == 0)
        {
            Debug.LogError("There are no spawn points available!");
        }

        base.Initialize();
    }

    void Update()
    {
        Invoke("SpawnFriendAirplane", spawnTime);
    }

    private void SpawnFriendAirplane()
    {
        int index = Random.Range(0, spawnFriendAirplanePoints.Count);

        var friendAirplaneSpawn = spawnFriendAirplanePoints[index];
        friendAirplaneSpawn.ReverseTranslate();

        SpawnFriendAirplaneFromPool(friendAirplaneSpawn.transform.position, friendAirplaneSpawn.side);

        CancelInvoke("SpawnFriendAirplane");
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
