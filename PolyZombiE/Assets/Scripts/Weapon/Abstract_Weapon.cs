using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Weapon: MonoBehaviour{
    [SerializeField] private string primaryAttackAnimation = "";
    abstract public void PrimaryAttack(LayerMask targetFilter, Abstract_Identity activator);
    public string getPrimaryAttackAnimation() { return primaryAttackAnimation; }
    virtual public float getPrimaryRange() { return -1.0f; }
    public bool primaryReady() { return Time.time > prevPrimaryTime + primaryCoolDown; }
    [SerializeField] protected float primaryCoolDown = 0.0f;
    protected float prevPrimaryTime = 0.0f;

    [SerializeField] private string secondaryAttackAnimation = "";
    abstract public void SecondaryAttack(LayerMask targetFilter, Abstract_Identity activator);
    public string getSecondaryAttackAnimation() { return secondaryAttackAnimation; }
    virtual public float getSecondaryRange() { return -1.0f; }
    public bool secondaryReady() { return Time.time > prevSecondaryTime + secondaryCoolDown; }
    [SerializeField] protected float secondaryCoolDown = 0.0f;
    protected float prevSecondaryTime = 0.0f;

    protected Vector2 getDirectionVec()
    {
        float rotationAngle = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        return new Vector2(Mathf.Cos(rotationAngle), Mathf.Sin(rotationAngle));
    }
}
