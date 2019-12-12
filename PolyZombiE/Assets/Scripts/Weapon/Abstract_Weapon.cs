using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Weapon: MonoBehaviour{
    
    abstract public void PrimaryAttack(LayerMask targetFilter);
    virtual public float getPrimaryRange() { return -1.0f; }

    abstract public void SecondaryAttack(LayerMask targetFilter);
    virtual public float getSecondaryRange() { return -1.0f; }

    protected Vector2 getDirectionVec()
    {
        float rotationAngle = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        return new Vector2(Mathf.Cos(rotationAngle), Mathf.Sin(rotationAngle));
    }
}
