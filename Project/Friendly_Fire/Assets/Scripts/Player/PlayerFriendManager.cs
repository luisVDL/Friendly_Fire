using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFriendManager : MonoBehaviour
{
    [SerializeField] private List<Transform> m_FriendPositions;
    private List<FriendScript> m_CurrentFriends;
    [SerializeField] private int m_maxNumberOfFriends = 6;
    [SerializeField] private PickUpFriendSelectorManager m_PickUp;

    void Start()
    {
        m_CurrentFriends = new List<FriendScript>();
        

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Friend")
        {
            /*
             * - Get the Friendscript Component
             * - Open the PickUp Friend Window
             * - If want to add check if CanAddFriend()
             * - If not open a window with all the current friends and the new one choose between them which you want to leave. The selected will be deactivated.
             */
            //if(CanAddFriend()) AddFriend(other.GetComponentInParent<FriendScript>());
            m_PickUp.OpenFriendSelector(other.GetComponentInParent<FriendScript>(), this);
        }
    }

    public bool CanAddFriend()
    {
        return m_CurrentFriends.Count <m_maxNumberOfFriends;
    }

    public void AddFriendToNewPosition(FriendScript l_Friend)
    {
        if (!m_CurrentFriends.Contains(l_Friend))
        {
            l_Friend.SetFriendPosition(GetNextPosition());
            m_CurrentFriends.Add(l_Friend);
        }
        
    }

    private Transform GetNextPosition()
    {
        return m_FriendPositions[m_CurrentFriends.Count];
    }

    public List<FriendScript> GetFriendScripts()
    {
        return m_CurrentFriends;
    }
}