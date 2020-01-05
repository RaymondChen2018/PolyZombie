using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Projectile_Bullet : MonoBehaviour {
    [SerializeField] float speed = 10.0f;
    AttackVictim attackInfo;
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
    public void SetInfo(AttackVictim _attackInfo)
    {
        attackInfo = _attackInfo;
        hitMask |= _attackInfo.activator.getTeamComponent().GetOpponentLayerMask();
    }
}
