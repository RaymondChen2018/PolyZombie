using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile_Spawner : MonoBehaviour {
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private UnityEvent OnSpawnProjectile = new UnityEvent();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnProjectile(AttackInfo attackInfo)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile_Bullet projectileComponent = projectile.GetComponent<Projectile_Bullet>();
        projectileComponent.SetInfo(attackInfo);

        OnSpawnProjectile.Invoke();
    }
}
