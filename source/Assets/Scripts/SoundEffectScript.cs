using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundEffectClip
{
    ClickButton,
    StartGame,
    ShipAttack,
    ShipExtraAttack,
    ShipFallingOcean,
    ShipHit,
    ShipTakeItemSound,
    ShipTakeSpecialItem,
    ShipRecoverHealth,
    EnemyShowingSound,
    EnemyBombFallingSky,
    EnemyBombFallingOcean,
    EnemyHit,
    EnemyDestroyed,
    CollisionBetweenShots,
    GameTimeEnding
}

public class SoundEffectScript : MonoBehaviour
{
    private static SoundEffectScript instance;
    public static SoundEffectScript Instance
    {
        get
        {
            if (SoundEffectScript.instance == null)
            {
                SoundEffectScript.instance = GameObject.Find("Sounds").GetComponent<SoundEffectScript>();
            }

            return SoundEffectScript.instance;
        }
    }

    // MENU
    [SerializeField]
    private AudioClip clickButtonSound;
    [SerializeField]
    private AudioClip startGameSound;
    // SHIP
    [SerializeField]
    private AudioClip shipAttackSound;
    [SerializeField]
    private AudioClip shipExtraAttackSound;
    [SerializeField]
    private AudioClip shipFallingOceanSound;
    [SerializeField]
    private AudioClip shipHitSound;
    [SerializeField]
    private AudioClip shipTakeItemSound;
    [SerializeField]
    private AudioClip shipTakeSpecialItemSound;
    [SerializeField]
    private AudioClip shipRecoverHealthSound;
    // ENEMY
    [SerializeField]
    private AudioClip enemyShowingSound;
    [SerializeField]
    private AudioClip enemyBombFallingSkySound;
    [SerializeField]
    private AudioClip enemyBombFallingOceanSound;
    [SerializeField]
    private AudioClip enemyHitSound;
    [SerializeField]
    private AudioClip enemyDestroyedSound;
    // SHOT
    [SerializeField]
    private AudioClip collisionBetweenShotsSound;
    //GAME
    [SerializeField]
    private AudioClip gameTimeEndingSound;

    private Dictionary<SoundEffectClip, AudioClip> clipDictionary;

    void Start()
    {
        if (instance == null)
        {
            instance = this;

            CreateDictionary();

            DontDestroyOnLoad(this);
        }
    }

    public void PlaySound(SoundEffectClip soundEffectClip)
    {
        AudioClip originalClip;

        if (clipDictionary.TryGetValue(soundEffectClip, out originalClip))
        {
            MakeSound(originalClip);
        }
    }

    private void CreateDictionary()
    {
        clipDictionary = new Dictionary<SoundEffectClip, AudioClip>();

        clipDictionary.Add(SoundEffectClip.ClickButton, clickButtonSound);
        clipDictionary.Add(SoundEffectClip.CollisionBetweenShots, collisionBetweenShotsSound);
        clipDictionary.Add(SoundEffectClip.EnemyBombFallingOcean, enemyBombFallingOceanSound);
        clipDictionary.Add(SoundEffectClip.EnemyBombFallingSky, enemyBombFallingSkySound);
        clipDictionary.Add(SoundEffectClip.EnemyDestroyed, enemyDestroyedSound);
        clipDictionary.Add(SoundEffectClip.EnemyHit, enemyHitSound);
        clipDictionary.Add(SoundEffectClip.EnemyShowingSound, enemyShowingSound);
        clipDictionary.Add(SoundEffectClip.GameTimeEnding, gameTimeEndingSound);
        clipDictionary.Add(SoundEffectClip.ShipAttack, shipAttackSound);
        clipDictionary.Add(SoundEffectClip.ShipExtraAttack, shipExtraAttackSound);
        clipDictionary.Add(SoundEffectClip.ShipFallingOcean, shipFallingOceanSound);
        clipDictionary.Add(SoundEffectClip.ShipHit, shipHitSound);
        clipDictionary.Add(SoundEffectClip.ShipRecoverHealth, shipRecoverHealthSound);
        clipDictionary.Add(SoundEffectClip.ShipTakeItemSound, shipTakeItemSound);
        clipDictionary.Add(SoundEffectClip.ShipTakeSpecialItem, shipTakeSpecialItemSound);
        clipDictionary.Add(SoundEffectClip.StartGame, startGameSound);
    }

    private void MakeSound(AudioClip originalClip)
    {
        audio.PlayOneShot(originalClip);
    }
}
