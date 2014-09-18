using UnityEngine;
using System.Collections;

public class FriendAirplaneSpawn : GenericSpawn
{
    void Start()
    {
        Vector2 startPosition = StartPosition();

        transform.parent.CreateTrigger(
            string.Format("{0} Friends Airplanes Trigger Up", side), startPosition,
            tagName.ToString(), layerName.ToString());

        transform.parent.CreateTrigger(
            string.Format("{0} Friends Airplanes Trigger Down", side), new Vector2(startPosition.x, 0),
            tagName.ToString(), layerName.ToString());

        transform.position = new Vector2(startPosition.x, startPosition.y / 2);
    }
}
