using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneBulletBoneJovi : AbsBullet, IRestartable
{

    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private float m_BulletSpeed;
    [SerializeField] private float m_TimeToLive = 5f;

    private float m_LastTimeShot=0.0f;
    private Vector3 m_Direction;
    private Rigidbody2D m_BulletRB;

    private BoneJoviFriendScript m_BoneFriend;
    
    
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
        l_Direction.z = 0;
        transform.position = l_Position;
        transform.rotation = l_rotation;
        m_Direction = l_Direction.normalized;
        m_BulletRB = GetComponent<Rigidbody2D>();
        gameObject.SetActive(true);
        Trajectory();
        return m_BulletPrefab;
        
    }
    

    public override void Trajectory()
    {
        
        m_BulletRB.velocity=m_Direction * m_BulletSpeed;
        m_LastTimeShot = Time.time;
    }
    
    public override float GetDamage()
    {
        return m_BoneFriend.getFriendDamage();
    }
    

    public override void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Environment")
        {
            gameObject.SetActive(false);
        }
    }

    public void Restart()
    {
        gameObject.SetActive(false);
    }
    public void SetParentFriend(BoneJoviFriendScript l_Parent)
    {
        m_BoneFriend = l_Parent;
    }
}
