using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchlapBulletBehaviour : AbsBullet
{
    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private float m_BulletSpeed;
    [SerializeField] private float m_TimeToLive = 5f;

    private float m_LastTimeShot=0.0f;
    private Vector3 m_Direction;
    private Rigidbody2D m_BulletRB;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override GameObject FireBullet(Vector3 l_Direction, Vector3 l_Position, Quaternion l_rotation)
    {
        transform.position = l_Position;

        return m_BulletPrefab;
    }

    public override void Trajectory()
    {
        m_BulletRB.velocity=m_Direction * m_BulletSpeed;
        m_LastTimeShot = Time.time;
    }

    public override float GetDamage()
    {
        //This bullet won't do any damage because it's a state
        return 0;
    }

    public override void DeactivateBullet()
    {
        throw new System.NotImplementedException();
    }
}
