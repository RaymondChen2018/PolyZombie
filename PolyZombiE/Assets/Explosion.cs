using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosion : MonoBehaviour {
    [SerializeField] float damagePerRay = 5.0f;
    [SerializeField] float radius = 5.0f;
    [SerializeField] int rayCount = 100;
    [SerializeField] bool rayBounce = false;
    [SerializeField] int maxBounce = 5;
    [SerializeField] float bounceOffset = 0.02f;
    [SerializeField] private UnityEventAttack OnHit = new UnityEventAttack();
    [SerializeField] private UnityEvent OnExplode = new UnityEvent();
    [SerializeField] private bool startOn = true;
    [SerializeField] LayerMask hitMask;
    private AttackInfo attackInfo;

    [Header("Debug")]
    [SerializeField] private bool debugOn = false;
    [SerializeField] private Color debugColor = Color.red;

    // Use this for initialization
    void Start () {
        if(attackInfo == null)
        {
            attackInfo = new AttackInfo(World_Identity.singleton);
            attackInfo.damage = damagePerRay;
        }
        if (startOn)
        {
            explode();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void explode()
    {
        //Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hitMask);
        //for(int i = 0; i < hits.Length; i++)
        //{
        //    // Call back
        //    attackInfo.posImpact = hits[i].point;
        //    attackInfo.victim = hit.collider;
        //    OnHit.Invoke(attackInfo);
        //    OnImpact.Invoke(hit.point);
        //}

        OnExplode.Invoke();

        float angleStep = 360.0f / rayCount;
        float bounceDamp = radius / maxBounce;
        for (float rayAngle = 0.0f; rayAngle < 360.0f; rayAngle += angleStep)
        {
            float distLeft = radius;
            Vector2 castPos = transform.position;
            Vector2 castDir = Quaternion.AngleAxis(rayAngle, Vector3.forward) * Vector2.right;
            while (distLeft > 0.0f)
            {
                RaycastHit2D hit = Physics2D.Raycast(castPos, castDir, distLeft, hitMask);

                float distTravelled = distLeft;
                if (hit)
                {
                    distTravelled = Mathf.Max(bounceDamp, (hit.point - castPos).magnitude);

                    attackInfo.damage = damagePerRay * (distLeft / radius);
                    attackInfo.posImpact = hit.point;
                    attackInfo.victim = hit.collider;
                    OnHit.Invoke(attackInfo);

                    //Debug.Log("damage profile: " + attackInfo.victim + "; damage: " + attackInfo.damage + "; dist travelled: " + distTravelled);
                    if (debugOn)
                    {
                        Debug.DrawLine(castPos, hit.point, debugColor, 5.0f);
                    }

                    castDir = Vector2.Reflect(castDir, hit.normal);
                    castPos = hit.point + castDir * bounceOffset;
                }
                
                distLeft -= distTravelled;

                if (!rayBounce)
                {
                    break;
                }
            }
        }

        //Destroy(gameObject);
    }

    public void SetInfo(AttackInfo _attackInfo)
    {
        damagePerRay = _attackInfo.damage;
        hitMask |= _attackInfo.activator.getTeamComponent().GetOpponentLayerMask();
        attackInfo = _attackInfo;
    }

    private void OnDrawGizmos()
    {
        if (debugOn)
        {
            AI_Finder.DrawEllipse(transform.position, radius, debugColor);
        }
    }
}
