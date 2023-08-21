using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BoneJoviFriendScript : OffensiveFriend, IRestartable
{
    // Start is called before the first frame update

    [SerializeField] private int m_MaxShots = 5;
    [SerializeField] private float m_CooldownAbility = 4f;
    [SerializeField] private float m_CooldownShot = 0.2f;
    [SerializeField] private PoolScript m_BonePool;
    private bool m_Ability;
    
    
    public override bool ActivateFriendAbility()
    {
        m_Ability = true;
        m_Animator.SetTrigger("Attack");
        m_LastTimeAbility = Time.time;
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
        m_LastTimeAbility=Time.time;
    }
    

    public void Restart()
    {
        //throw new System.NotImplementedException();
    }

    void Start()
    {
        m_LastTimeAbility = Time.time;
        m_Ability = false;
        m_CurrentDamage = m_InitialDamage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_LastTimeAbility + m_CooldownAbility< Time.time  && !m_Ability)
        {
            ActivateFriendAbility();
        }
    }
}
