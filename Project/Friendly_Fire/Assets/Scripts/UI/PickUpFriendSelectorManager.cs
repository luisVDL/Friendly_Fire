using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickUpFriendSelectorManager : MonoBehaviour
{
    [SerializeField] private GameObject m_FriendSelectorPanel;
    [SerializeField] private GameObject m_FirstSelectedButton;
    [SerializeField] private PlayerFriendManager m_Player;
    private FriendScript m_Friend;

    [SerializeField] private TextMeshProUGUI m_AbilityText;
    [SerializeField] private Image m_FriendType;
    [SerializeField] private Image m_FriendSprite;
    [SerializeField] private List<Sprite> m_FriendTypeList;
    [SerializeField] private TextMeshProUGUI m_FriendNameText;
    [SerializeField] private TextMeshProUGUI m_FriendCreatorText;


    public void OpenFriendSelector(FriendScript l_Friend, PlayerFriendManager l_Player)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_FirstSelectedButton);
        Time.timeScale = 0f;
        m_Friend = l_Friend;
        m_Player = l_Player;

        InitializeFriendTexts();
        InitializeFriendSprites();

        m_FriendSelectorPanel.SetActive(true); 
    }

    private void InitializeFriendTexts()
    {
        m_FriendNameText.text = m_Friend.GetFriendName();
        m_FriendCreatorText.text = m_Friend.GetFriendCreator();

        m_AbilityText.text = m_Friend.GetAbilityDescription();
    }


    private void InitializeFriendSprites()
    {
        m_FriendSprite.sprite = m_Friend.GetFriendSprite();
        m_FriendType.sprite = SetFriendImageType(m_Friend.getFriendAbilityScript());
    }

    private Sprite SetFriendImageType(AFriend l_FriendAb)
    {
        AFriend.FriendType l_type=l_FriendAb.getFriendType();
        switch (l_type)
        {
            case AFriend.FriendType.OFFENSIVE:
                return m_FriendTypeList[0];
            
            case AFriend.FriendType.DEFENSIVE:
                return m_FriendTypeList[1];
            
            case AFriend.FriendType.SUPPORT:
                return m_FriendTypeList[2];
            
            case AFriend.FriendType.STATUS:
                return m_FriendTypeList[3];
        }
        return null;
    }

    public void AddFriend()
    {
        if (m_Player.CanAddFriend())
        {
            m_Player.AddFriendToNewPosition(m_Friend);
            Time.timeScale = 1f;
            m_FriendSelectorPanel.SetActive(false);
        }
        else
        {
            //Open friend inventory selector
        }
        
    }

    public void LeaveFriend()
    {
        Time.timeScale = 1f;
        m_FriendSelectorPanel.SetActive(false);
    }
}
