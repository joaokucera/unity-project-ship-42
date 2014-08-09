using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class Barril : GenericMovement, IAmmo, IDamage, IEnemy
{
    [SerializeField]
    private float turningSpeed = 20f;
    [SerializeField]
    private float gravityScale = 0.25f;
    [SerializeField]
    private float timeToFloat = 0.25f;

    //private Animator animator;
    //private float originalTurningSpeed;

    public bool Splashed { get; set; }
    public int Damage
    {
        get { return (int)AmmoDamage.Barril; }
    }

    //void Start()
    //{
    //    //animator = GetComponent<Animator>();

    //    base.Initialize();

    //    originalTurningSpeed = turningSpeed;
    //}

    void Update()
    {
        if (rigidbody2D.gravityScale > 0)
        {
            transform.Rotate(new Vector3(0, 0, turningSpeed * Time.deltaTime));

            base.Updating();
        }
        else
        {
            //TODO: Talvez, criar uma feature para 
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Wave" && !Splashed)
        {
            Splashed = true;

            Invoke("StartToFloat", timeToFloat);

            WaterSplashPooling.Instance.SpawnSplashFromPool(transform.position);
        }
    }

    private void StartToFloat()
    {
        rigidbody2D.gravityScale = 0f;
        rigidbody2D.velocity = Vector2.zero;

        //transform.tag = TagName.Barril.ToString();
        
        //animator.SetBool("ToFloat", true);
    }

    void OnBecameVisible()
    {
        Splashed = false;
        rigidbody2D.gravityScale = gravityScale;

        transform.rotation = Quaternion.Euler(Vector3.zero);
        //transform.tag = TagName.EnemyAmmo.ToString();

        //animator.SetBool("ToFloat", false);
    }

    public void SetDamage()
    {
        DeactivateTargets();

        gameObject.SetActive(false);
    }

    public void DeactivateTargets()
    {
        List<Target> targets = GetComponentsInChildren<Target>().ToList();
        targets.ForEach(t => t.SendMessage("Deactivate"));
    }
}
