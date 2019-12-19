using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Weapon: MonoBehaviour{
    public void PrimaryAttack(LayerMask targetFilter, Abstract_Identity activator)
    {
        prevUseTime = Time.time;
        PrimaryAttackDerived(targetFilter, activator);
    }
    abstract public void PrimaryAttackDerived(LayerMask targetFilter, Abstract_Identity activator);
    public bool primaryReady() { return Time.time > prevUseTime + primaryCoolDown; }
    [SerializeField] protected float primaryCoolDown = 0.0f;

    public void SecondaryAttack(LayerMask targetFilter, Abstract_Identity activator)
    {
        prevUseTime = Time.time;
        SecondaryAttackDerived(targetFilter, activator);
    }
    abstract public void SecondaryAttackDerived(LayerMask targetFilter, Abstract_Identity activator);
    public bool secondaryReady() { return Time.time > prevUseTime + secondaryCoolDown; }
    [SerializeField] protected float secondaryCoolDown = 0.0f;

    private float prevUseTime = 0.0f;

    protected Vector2 getDirectionVec()
    {
        float rotationAngle = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        return new Vector2(Mathf.Cos(rotationAngle), Mathf.Sin(rotationAngle));
    }
}
