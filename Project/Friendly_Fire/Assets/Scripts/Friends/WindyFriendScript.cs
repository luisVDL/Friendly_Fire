using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindyFriendScript : OffensiveFriend, IRestartable
{

    private bool m_Ability;
    //For future level purposes
    private int m_Waves;

    [SerializeField] private PoolScript m_Pool;
    [SerializeField] private float m_CooldownBetweenWaves=0.5f;
    [SerializeField] private float m_CooldownBetweenDirections =0.3f;
    [SerializeField] private int m_MaxEnemiesPierced = 3;

    void OnEnable()
    {
        m_CooldownImage.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        m_CooldownImage.gameObject.SetActive(false);
    }
    
    void Start()
    {
        m_Waves = 1;
        m_LastActivationTime = Time.time;
        m_Ability = false;
        m_CurrentDamage = m_InitialDamage;
        m_CurrentCooldown = m_InitialCooldown;
    }


    void Update()
    {
        m_CooldownImage.fillAmount = (Time.time - m_LastActivationTime) / m_CurrentCooldown;
        if (m_LastActivationTime + m_CurrentCooldown < Time.time && !m_Ability)
        {
            ActivateFriendAbility();
        }
    }

    public override bool ActivateFriendAbility()
    {
        m_LastActivationTime = Time.time;
        m_Ability = true;
        m_Animator.SetTrigger("Attack");
        //m_LastActivationTime = Time.time;
                
        GameObject l_GO;
        WindyBullet l_Bullet;

        l_GO = m_Pool.EnableObject();
        l_Bullet = l_GO.GetComponent<WindyBullet>();
        try
        { 
            Vector3 l_direction = NewEnemyManager.getEnemyPosition() - transform.position; 
            l_Bullet.SetMaxEnemiesPierced(m_MaxEnemiesPierced); 
            l_Bullet.FireBullet(l_direction, transform.position, GetBulletRotation(l_direction));
        }
        catch (Exception e)
        {
            print("Doesn't get the location");
        }

        m_Ability = false;
        //StartCoroutine(ShootWind());
        return true;
    }

    
    private Quaternion GetBulletRotation(Vector3 l_Reference)
    {
        float l_RotationZ=Mathf.Atan2(l_Reference.y, l_Reference.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(l_RotationZ, Vector3.forward);

    }
    private IEnumerator ShootWind()
    {
        for (int i = 0; i < m_Waves; i++)
        {
            if (i > 0) yield return new WaitForSeconds(m_CooldownBetweenWaves);
            
            //Shoot +
            ShootVerticalNHorizontal();
            
            yield return new WaitForSeconds(m_CooldownBetweenDirections);
            
            //Shoot x
            ShootDiagonal();
        }
        m_LastActivationTime = Time.time;
        m_Ability = false;
    }

    public void Restart()
    {
        m_CooldownImage.gameObject.SetActive(false);
        //Needs something more
    }

    private void ShootVerticalNHorizontal()
    {
        GameObject l_GO;
        AbsBullet l_Bullet;

        l_GO = m_Pool.EnableObject();
        l_Bullet = l_GO.GetComponent<AbsBullet>();
        l_Bullet.FireBullet(Vector3.up, transform.position, Quaternion.Euler(0f,0f,0f));
        
        l_GO = m_Pool.EnableObject();
        l_Bullet = l_GO.GetComponent<AbsBullet>();
        l_Bullet.FireBullet(Vector3.down, transform.position, Quaternion.Euler(0f,0f,180f));
        
        l_GO = m_Pool.EnableObject();
        l_Bullet = l_GO.GetComponent<AbsBullet>();
        l_Bullet.FireBullet(Vector3.right, transform.position, Quaternion.Euler(0f,0f,-90f));
        
        l_GO = m_Pool.EnableObject();
        l_Bullet = l_GO.GetComponent<AbsBullet>();
        l_Bullet.FireBullet(Vector3.left, transform.position, Quaternion.Euler(0f,0f,90f));
    }

    private void ShootDiagonal()
    {
        GameObject l_GO;
        AbsBullet l_Bullet;
        
        l_GO = m_Pool.EnableObject();
        l_Bullet = l_GO.GetComponent<AbsBullet>();
        l_Bullet.FireBullet(Vector3.up+Vector3.left, transform.position, Quaternion.Euler(0f,0f,45f));
        
        l_GO = m_Pool.EnableObject();
        l_Bullet = l_GO.GetComponent<AbsBullet>();
        l_Bullet.FireBullet(Vector3.up+Vector3.right, transform.position, Quaternion.Euler(0f,0f,-45f));
        
        l_GO = m_Pool.EnableObject();
        l_Bullet = l_GO.GetComponent<AbsBullet>();
        l_Bullet.FireBullet(Vector3.down+Vector3.right, transform.position, Quaternion.Euler(0f,0f,-135f));
        
        l_GO = m_Pool.EnableObject();
        l_Bullet = l_GO.GetComponent<AbsBullet>();
        l_Bullet.FireBullet(Vector3.down+Vector3.left, transform.position, Quaternion.Euler(0f,0f,135f));
    }
}
