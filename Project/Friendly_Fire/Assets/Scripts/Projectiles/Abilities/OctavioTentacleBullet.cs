using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctavioTentacleBullet : AbsBullet, IRestartable
{
    [SerializeField] private GameObject m_TentaclePrefab;
    private OctavioFriendScript m_OctavioFriend;
    
    public void DeactivateTentacle()
    {
        gameObject.SetActive(false);
    }

    public override GameObject FireBullet(Vector3 l_Direction, Vector3 l_Position, Quaternion l_rotation)
    {
        gameObject.SetActive(true);
        transform.position = l_Position;
        return m_TentaclePrefab;
    }
    
    public override void Trajectory()
    {
  
    }
    
    public override float GetDamage()
    {
        return m_OctavioFriend.getFriendDamage();
    }
    

    public void Restart()
    {
        gameObject.SetActive(false);
    }

    public void setParentFriend(OctavioFriendScript l_Parent)
    {
        m_OctavioFriend = l_Parent;
    }

    public override void DeactivateBullet()
    {
        //It will deactivate itself with the animation
    }
}
