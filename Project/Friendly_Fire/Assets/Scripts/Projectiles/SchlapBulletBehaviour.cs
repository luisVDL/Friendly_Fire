using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SchlapBulletBehaviour : AbsBullet
{
    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private float m_BulletSpeed;
    [SerializeField] private float m_TimeToLive = 2f;

    private float m_LastTimeShot=0.0f;
    private Vector3 m_Direction;
    private Rigidbody2D m_BulletRB;
    private PoolScript m_Pool;
    
    private SchlapFriendScript m_Schlap;
    
    
    void Start()
    {
        m_BulletRB = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Time.time > m_LastTimeShot + m_TimeToLive)
        {
            SpawnSlimeArea(transform.position);
            DeactivateBullet();
        }
    }

    public override GameObject FireBullet(Vector3 l_Direction, Vector3 l_Position, Quaternion l_rotation)
    {
        m_BulletRB = GetComponent<Rigidbody2D>();
        m_LastTimeShot = Time.time;
        transform.position = l_Position;
        Trajectory();
        gameObject.SetActive(true);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            SpawnSlimeArea(other.transform.position);
            DeactivateBullet();
        }
    }


    private void SpawnSlimeArea(Vector3 l_Position)
    {
        GameObject l_GO = m_Pool.EnableObject();
        SchlapAreaBehaviour l_Area = l_GO.GetComponent<SchlapAreaBehaviour>();
        l_Area.FireBullet(l_Position, l_Position, quaternion.identity);
        l_Area.Spawn(m_Schlap.getAbilityTime());
        DeactivateBullet();
    }

    public override void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }

    public void setParentSchlap(SchlapFriendScript l_Friend)
    {
        m_Schlap = l_Friend;
    }

    public void setAreaPool(PoolScript l_Pool)
    {
        m_Pool = l_Pool;
    }
    
}
