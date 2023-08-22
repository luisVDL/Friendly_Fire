using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BoneJoviFriendScript : OffensiveFriend, IRestartable
{
    // Start is called before the first frame update

    [SerializeField] private int m_MaxShots = 5;

    [SerializeField] private float m_CooldownShot = 0.2f;
    [SerializeField] private PoolScript m_BonePool;
    private bool m_Ability;
    
    
    
    void OnEnable()
    {
        m_CooldownImage.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        m_CooldownImage.gameObject.SetActive(false);
    }
    
    public override bool ActivateFriendAbility()
    {
        m_Ability = true;
        m_Animator.SetTrigger("Attack");
        m_LastActivationTime = Time.time;
        StartCoroutine(ShootBones());
        
        return true;
    }

    private IEnumerator ShootBones()
    {
        
        GameObject l_GO;
        BoneBulletBoneJovi l_Bone;
        for (int i = 0; i < m_MaxShots; i++)
        {
            l_GO = m_BonePool.EnableObject();
            l_Bone = l_GO.GetComponent<BoneBulletBoneJovi>();
            l_Bone.setParentFriend(this);
            try
            {
                l_Bone.FireBullet(NewEnemyManager.getEnemyPosition() - transform.position, transform.position,
                    transform.rotation);
            }
            catch (Exception e)
            {
                print("Doesn't get the location");
            }
            
            yield return new WaitForSeconds(m_CooldownShot);
        }
        m_Ability = false;
        //m_LastActivationTime=Time.time;
    }
    

    public void Restart()
    {
        m_CooldownImage.gameObject.SetActive(false);
        //throw new System.NotImplementedException();
    }

    void Start()
    {
        m_LastActivationTime = Time.time;
        m_Ability = false;
        m_CurrentDamage = m_InitialDamage;
        m_CurrentCooldown = m_InitialCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        m_CooldownImage.fillAmount = (Time.time - m_LastActivationTime) / m_CurrentCooldown;
        if (m_LastActivationTime + m_CurrentCooldown< Time.time  && !m_Ability)
        {
            ActivateFriendAbility();
        }
    }
}
