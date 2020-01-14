using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Projectile_Bullet : MonoBehaviour {
    [SerializeField] float speed = 10.0f;
    AttackInfo attackInfo;
    public UnityEventAttack OnHit = new UnityEventAttack();
    [SerializeField] LayerMask hitMask;

    [Header("Debug")]
    [SerializeField] private bool debugOn = false;
    [SerializeField] private Color debugColor = Color.red;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float distanceTravelled = speed * Time.deltaTime;
        Vector2 direction = transform.rotation * Vector2.right;

        if (debugOn)
        {
            Debug.DrawLine(transform.position, (Vector2)transform.position + direction * distanceTravelled, debugColor);
        }

        // Hit detection
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanceTravelled, hitMask);
        if (hit)
        {
            // Update position
            transform.position = hit.point;

            // Call back
            attackInfo.posImpact = hit.point;
            attackInfo.victim = hit.collider;
            OnHit.Invoke(attackInfo);

            // Destroy bullet
            Destroy(gameObject);
        }
        else
        {
            // Travel
            transform.position = (Vector2)transform.position + direction * distanceTravelled;
        }
    }

    public void SetInfo(AttackInfo _attackInfo)
    {
        attackInfo = _attackInfo;
        hitMask |= _attackInfo.activator.getTeamComponent().GetOpponentLayerMask();
    }

    public void Deal_damage(AttackInfo attackInfo)
    {
        if (attackInfo.activator == null)
        {
            attackInfo.activator = World_Identity.singleton;
        }

        Assert.IsNotNull(attackInfo.activator);
        Assert.IsNotNull(attackInfo.victim);

        Abstract_Identity activator = attackInfo.activator;
        Abstract_Identity victim = attackInfo.victim.GetComponent<Abstract_Identity>();

        // Hit character
        if (victim != null)
        {
            victim.getHealthComponent().subtractHealth(new DamageInfo(attackInfo.damage, activator.transform.position - victim.transform.position, activator));
        }
        else
        {

        }
    }
}
