using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsBullet : MonoBehaviour
{
    
    public abstract GameObject FireBullet(Vector3 l_Direction, Vector3 l_Position, Quaternion l_rotation);
    public abstract List<AbsBullet> NullifyOtherBullets();
    public abstract void Trajectory();
    public abstract float GetDamage();
    
    public abstract void DeactivateBullet();
}
