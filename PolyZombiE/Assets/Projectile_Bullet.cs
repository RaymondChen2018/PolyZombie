using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class UnityEventAttackHit : UnityEvent<float, Abstract_Identity, Abstract_Identity>
{

}
public class Projectile_Bullet : MonoBehaviour {
    Attack attackInfo;
    public UnityEventAttack OnHit = new UnityEventAttack();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (false)
        {
            OnHit.Invoke(attackInfo);
        }
	}
    public void SetInfo(Attack _attackInfo)
    {
        attackInfo = _attackInfo;
    }
}
