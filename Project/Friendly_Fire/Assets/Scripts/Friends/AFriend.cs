using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AFriend : MonoBehaviour
{
    
    
    /*
     * Things that I need to be able to do:
     * - Cast an ability -> DONE
     * - Get the FriendType (offensive, defensive, status, support) -> DONE
     * - The first friend to appear has to be an OFFENSIVE TYPE
     * - I need to get the Cooldown -> DONE
     * - I need to get the duration and be able to increase it. //NOT SURE
     * - I need to apply a multiplier to the cooldown -> DONE
     * - I need to apply a multiplier to the offensive damage -> DONE
     * - I need to apply a multiplier to the defensive damage
     * - I need to apply a multiplier to the duration of the Support and Status abilities
     * - I need to be able to activate and deactivate the friend -> DONE
     */
    public enum FriendType
    { OFFENSIVE, DEFENSIVE, STATUS, SUPPORT }

    

    [SerializeField] protected Image m_CooldownImage;

    [SerializeField] protected FriendType m_FriendType;
    [SerializeField] protected Animator m_Animator;

    

    //We need two cooldowns in order to be able to change it in runtime
    [SerializeField] protected float m_InitialCooldown;
    protected float m_CurrentCooldown;
    protected bool m_FriendIsActive;
    protected float m_LastActivationTime;
    
    //Not sure if I need it 
    //protected string m_ID;
    
    [Header("Friend Substates")] 
    [SerializeField] private GameObject m_AttackUp;

    public FriendType getFriendType()
    {
        return m_FriendType;
    }

    public float getCooldown()
    {
        return m_CurrentCooldown;
    }
    // we store the value in a variable and then we set it back (checking future issues)
    public abstract void ApplyWolfyStatus(float l_DurationTime);
    public abstract void DeApplyWolfyStatus();

    protected void AttackUpAnimation()
    {
        m_AttackUp.SetActive(true);
    }
    protected void HideAttackUpAnimation()
    {
        m_AttackUp.SetActive(false);
    }
    
    public void ReduceFriendActivationTime(float l_Amount)
    {
        m_LastActivationTime -= l_Amount;
    }

    public void ActivateFriend()
    {
        m_FriendIsActive = true;
    }
    public void DeactivateFriend()
    {
        m_FriendIsActive = false;
    }
    public abstract bool ActivateFriendAbility();
   
}
   

