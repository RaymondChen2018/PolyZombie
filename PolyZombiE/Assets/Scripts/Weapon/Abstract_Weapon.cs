using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Weapon: MonoBehaviour{
    [SerializeField] protected float primaryCycleTime = 0.0f;
    [SerializeField] protected string primaryAnimation = "";
    [SerializeField] protected float secondaryCycleTime = 0.0f;
    [SerializeField] protected string secondaryAnimation = "";
    private float prevUseTime = 0.0f;

    public void PrimaryAttack(LayerMask targetFilter, Abstract_Identity activator)
    {
        prevUseTime = Time.time;
        PrimaryAttackDerived(targetFilter, activator);
    }
    public void SecondaryAttack(LayerMask targetFilter, Abstract_Identity activator)
    {
        prevUseTime = Time.time;
        SecondaryAttackDerived(targetFilter, activator);
    }
    public bool primaryReady() { return Time.time > prevUseTime + primaryCycleTime; }
    public bool secondaryReady() { return Time.time > prevUseTime + secondaryCycleTime; }
    public string getPrimaryAnimation()
    {
        return primaryAnimation;
    }
    public string getSecondaryAnimation()
    {
        return secondaryAnimation;
    }

    abstract public void PrimaryAttackDerived(LayerMask targetFilter, Abstract_Identity activator);
    abstract public void SecondaryAttackDerived(LayerMask targetFilter, Abstract_Identity activator);

    protected Vector2 getDirectionVec()
    {
        float rotationAngle = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        return new Vector2(Mathf.Cos(rotationAngle), Mathf.Sin(rotationAngle));
    }
}
