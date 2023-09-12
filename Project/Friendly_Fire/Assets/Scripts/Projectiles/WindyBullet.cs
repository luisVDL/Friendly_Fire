using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindyBullet : AbsBullet, IRestartable
{
    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private float m_BulletSpeed;
    [SerializeField] private float m_TimeToLive = 5f;
    [SerializeField] private float m_DamageDealt = 20f;
    
    private float m_LastTimeShot=0.0f;
    private int currentEnemiesPierced = 0;
    private int m_MaxEnemiesPierced;
    private Vector3 m_Direction;
    private Rigidbody2D m_BulletRB;

    private void Start()
    {
        ExpManager.AddRestartElement(this);
    }
    void Update()
    {
        if(Time.time>m_LastTimeShot+m_TimeToLive) gameObject.SetActive(false);
    }

    public override GameObject FireBullet(Vector3 l_Direction, Vector3 l_Position, Quaternion l_rotation)
    {
        currentEnemiesPierced = 0;
        l_Direction.z = 0;
        transform.position = l_Position;
        transform.rotation = l_rotation;
        m_Direction = l_Direction.normalized;
        m_BulletRB = GetComponent<Rigidbody2D>();
        gameObject.SetActive(true);
        Trajectory();
        return m_BulletPrefab;
        
    }

    public void SetMaxEnemiesPierced(int l_Max)
    {
        m_MaxEnemiesPierced = l_Max;
    }
    public override void Trajectory()
    {
        
        m_BulletRB.velocity=m_Direction * m_BulletSpeed;
        m_LastTimeShot = Time.time;
    }

    public override float GetDamage()
    {
        return m_DamageDealt;
    }

    public override void DeactivateBullet()
    {
        if (currentEnemiesPierced < m_MaxEnemiesPierced)
        {
            currentEnemiesPierced += 1;
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Environment")
        {
            currentEnemiesPierced = m_MaxEnemiesPierced;
            DeactivateBullet();
        }
    }

    public void Restart()
    {
        gameObject.SetActive(false);
    }
}