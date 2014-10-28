using UnityEngine;
using System.Collections;

public enum ArrowIndex
{
    Captain,
    Engineer,
    EngineerShipHealth,
    Soldier,
    SoldierShipShots,
    FriendAirplane,
    SafeBuoy,
    Enemies,
    LeftScreenSide,
    RightScreenSide
}

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private ArrowIndex arrowIndex;

    [SerializeField]
    private Transform tipToFix;

    void Update()
    {
        Camera mainCamera = Camera.main;
        Renderer arrowRenderer = GetComponentInChildren<Renderer>();

        switch (arrowIndex)
        {
            case ArrowIndex.Captain:
                transform.position = new Vector2(-(mainCamera.aspect * mainCamera.orthographicSize) + arrowRenderer.bounds.size.x,
                                                 arrowRenderer.bounds.size.y * 1.5f);
                break;
            case ArrowIndex.Engineer:
                transform.position = new Vector2(-(mainCamera.aspect * mainCamera.orthographicSize) + arrowRenderer.bounds.size.x * 1.95f * mainCamera.aspect,
                                                 arrowRenderer.bounds.size.y * 1.5f);
                break;
            case ArrowIndex.EngineerShipHealth:
                transform.position = new Vector2(0, -mainCamera.orthographicSize / 2 - arrowRenderer.bounds.size.y / 2);
                break;
            case ArrowIndex.Soldier:
                transform.position = new Vector2(-(mainCamera.aspect * mainCamera.orthographicSize) + arrowRenderer.bounds.size.x * 3.25f * mainCamera.aspect,
                                                 arrowRenderer.bounds.size.y * 1.5f);
                break;
            case ArrowIndex.SoldierShipShots:
                transform.position = new Vector2(-(mainCamera.aspect * mainCamera.orthographicSize) / 1.525f + arrowRenderer.bounds.size.x,
                                                 -mainCamera.orthographicSize / 2 + arrowRenderer.bounds.size.y / 2);
                tipToFix.position = new Vector2(transform.position.x + tipToFix.renderer.bounds.size.x / 8,
                                                tipToFix.position.y);
                break;
            case ArrowIndex.FriendAirplane:
                transform.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize / 2,
                                                 -mainCamera.orthographicSize / 2 - arrowRenderer.bounds.size.y / 1.5f);
                tipToFix.position = new Vector2(transform.position.x,
                                                tipToFix.position.y);
                break;
            case ArrowIndex.SafeBuoy:
                transform.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize / 2,
                                                 -mainCamera.orthographicSize / 2 - arrowRenderer.bounds.size.y);
                tipToFix.position = new Vector2(transform.position.x,
                                                tipToFix.position.y);
                break;
            case ArrowIndex.Enemies:
                transform.position = new Vector2(mainCamera.aspect * mainCamera.orthographicSize / 2,
                                                 -mainCamera.orthographicSize / 2 - arrowRenderer.bounds.size.y);
                tipToFix.position = new Vector2(transform.position.x,
                                                tipToFix.position.y);
                break;
            case ArrowIndex.LeftScreenSide:
                transform.position = new Vector2(-(mainCamera.aspect * mainCamera.orthographicSize) + arrowRenderer.bounds.size.x * 2,
                                                 -mainCamera.orthographicSize / 2 - arrowRenderer.bounds.size.y);
                break;
            case ArrowIndex.RightScreenSide:
                transform.position = new Vector2((mainCamera.aspect * mainCamera.orthographicSize) - arrowRenderer.bounds.size.x * 2,
                                                 -mainCamera.orthographicSize / 2 - arrowRenderer.bounds.size.y);
                break;
        }
    }
}
