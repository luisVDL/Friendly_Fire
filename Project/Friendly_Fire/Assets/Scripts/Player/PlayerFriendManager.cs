using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFriendManager : MonoBehaviour
{
    [SerializeField] private List<Transform> m_FriendPositions;
    private List<FriendScript> m_CurrentFriends;
    [SerializeField] private int m_maxNumberOfFriends = 6;

    void Start()
    {
        m_CurrentFriends = new List<FriendScript>();
        

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Friend")
        {
            if(CanAddFriend()) AddFriend(other.GetComponentInParent<FriendScript>());
        }
    }

    public bool CanAddFriend()
    {
        return m_CurrentFriends.Count <m_maxNumberOfFriends;
    }

    private void AddFriend(FriendScript l_Friend)
    {
        if (!m_CurrentFriends.Contains(l_Friend))
        {
            l_Friend.SetFriendPosition(getNextPosition());
            m_CurrentFriends.Add(l_Friend);
        }
        
    }

    private Transform getNextPosition()
    {
        return m_FriendPositions[m_CurrentFriends.Count];
    }
}