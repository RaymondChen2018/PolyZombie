using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Projectile_Bullet : MonoBehaviour {
    [SerializeField] float speed = 10.0f;
    [SerializeField] float damage = 20.0f;
    AttackInfo attackInfo;
    [SerializeField] private UnityEventAttack OnHit = new UnityEventAttack();
    [SerializeField] private UnityEventVector2 OnImpact = new UnityEventVector2();
    [SerializeField] LayerMask hitMask;

    [Header("Debug")]
    [SerializeField] private bool debugOn = false;
    [SerializeField] private Color debugColor = Color.red;

    // Use this for initialization
    void Start () {
		if(attackInfo == null)
        {
            attackInfo = new AttackInfo(World_Identity.singleton);
            attackInfo.damage = damage;
        }
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
            //UnityEngine.Events.UnityAction action = new UnityEngine.Events.UnityAction(Test_script.Destroy());
            //OnImpact.AddListener(new UnityEngine.Events.UnityAction<Vector2>());
            // Update position
            transform.position = hit.point;

            // Call back
            attackInfo.posImpact = hit.point;
            attackInfo.victim = hit.collider;
            OnHit.Invoke(attackInfo);
            OnImpact.Invoke(hit.point);

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
        damage = _attackInfo.damage;
        hitMask |= _attackInfo.activator.getTeamComponent().GetOpponentLayerMask();
    }
}
