using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    public abstract bool IsAlive();
    public abstract void TakeDamage(float l_Damage);
    public abstract void RecoverHealth(float l_Amount);
    protected abstract void Die();

}
