using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skills : MonoBehaviour
{
    public string skillName;
    public string description;
    public SO_BulletType bulletType;
    
    // Method will be called when the skill is activated
    public abstract void Activate(GameObject target);

    public virtual void ApplyToBullet(GameObject bullet) {}
}
