using UnityEngine;
using System.Collections;

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

    public AudioClip shitAttack;
    public AudioClip enemyShowingSound;
    public AudioClip enemyBombFallingSound;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void MakeShitAttackSound()
    {
        MakeSound(shitAttack);
    }

    public void MakeEnemyShowingSound()
    {
        MakeSound(enemyShowingSound);
    }

    public void MakeEnemyBombFallingSound()
    {
        MakeSound(enemyBombFallingSound);
    }

    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
