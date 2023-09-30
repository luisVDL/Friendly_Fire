using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExpManager : MonoBehaviour
{

    [Header("Experience")]
    [SerializeField] private List<GameObject> m_PlayerGameObjects;
    //private List<IAbility> m_PlayerAbilities = new List<IAbility>();
    [SerializeField] private int m_ExpAugmentPerLevel = 250;
    [SerializeField] private int m_ExpToFirstLevel = 300;

    /*
    [Header("Display")] 
    [SerializeField] private Image m_ExpBar;
    [SerializeField] private TextMeshProUGUI m_LevelText;
    [SerializeField] private Image m_MaxLevelSprite;
    [SerializeField] private UnityEvent ExpWindowShow;
*/
    [Header("Abilty Selector Window")] 
    [SerializeField] private GameObject m_AbSelectorWindow;
    //[SerializeField] private List<AbilitySelectorButton> m_Buttons;
    [SerializeField] private GameObject m_FirstSelectedButton;

    /*
    [Header("Audio")] 
    [SerializeField] private AudioClip m_LevelUpAudio;
    [SerializeField] private AudioSource m_AudioSource;
    */
    private static List<IRestartable> m_RestartList= new List<IRestartable>();


    private static int m_CurrentExp = 0;
    private int m_NextLevelExp;
    private static int m_CurrentLevel = 0;
    private int m_MaxLevel;
    
    void Start()
    {
        //EnemySpawner.SubscribeToExpEvent(this);
        m_NextLevelExp = m_ExpToFirstLevel;
        //InitializePlayerAbsList();
        //All abs have three levels
        //m_MaxLevel = m_PlayerAbilities.Count * 3;
        //m_LevelText.text=GetLevelText();
    }
/*
    private void InitializePlayerAbsList()
    {
        foreach (GameObject l_GO in m_PlayerGameObjects)
        {
            m_PlayerAbilities.Add(l_GO.GetComponent<IAbility>());
        }
    }
*/

    public void AddExperience(int l_Amount)
    {
        if (m_CurrentLevel == m_MaxLevel)
        {
            //m_MaxLevelSprite.gameObject.SetActive(true);
            //m_ExpBar.fillAmount = (float)m_NextLevelExp / m_NextLevelExp;   
        }
        else
        {
            m_CurrentExp += l_Amount;
            if (m_CurrentExp >= m_NextLevelExp)
            {
                LevelUp();
            }

            //m_ExpBar.fillAmount = (float)m_CurrentExp / m_NextLevelExp; 
        }
        
    }

    private void LevelUp()
    {
        m_CurrentExp -= m_NextLevelExp;
        m_CurrentLevel += 1;

        //m_AudioSource.PlayOneShot(m_LevelUpAudio);
        //show ab selector
        //GetNextShowAbs();
        //ExpWindowShow.Invoke();
        m_AbSelectorWindow.SetActive(true);
        
        
        m_NextLevelExp += m_ExpAugmentPerLevel*m_CurrentLevel;
        //m_LevelText.text=GetLevelText();
        Time.timeScale = 0f;
    }

    private string GetLevelText()
    {
        //beccause is a sprite font it has to keep the prefix <sprite=...>
        string l_Result = "";
        int l_currentUnit;
        int l_AuxLevel = m_CurrentLevel;

        do
        {
            l_currentUnit = l_AuxLevel % 10;
            l_Result = "<sprite=" + l_currentUnit + ">"+l_Result;
            l_AuxLevel=(l_AuxLevel-l_currentUnit)/10;
        } while (l_AuxLevel % 10 != 0);
        return l_Result;
    }
/*
    private void GetNextShowAbs()
    {
        //first we need to deselect the current button
        EventSystem.current.SetSelectedGameObject(null);
        //set a new active selected object
        EventSystem.current.SetSelectedGameObject(m_FirstSelectedButton);
        
        
        List<IAbility> l_Abs = GetAbilitiesCanLevelUp();
        switch (l_Abs.Count)
        {
            case 1:
                m_Buttons[0].SetAbility(l_Abs[0]);
                m_Buttons[1].HideButton();
                m_Buttons[2].HideButton();
                break;
                
            case 2: 
                m_Buttons[0].SetAbility(l_Abs[0]);
                m_Buttons[1].SetAbility(l_Abs[1]);
                m_Buttons[2].HideButton();
                break;
            case 3:
                m_Buttons[0].SetAbility(l_Abs[0]);
                m_Buttons[1].SetAbility(l_Abs[1]);
                m_Buttons[2].SetAbility(l_Abs[2]);
                break;
        }
        
    }

    private List<IAbility> GetAbilitiesCanLevelUp()
    {
        List<IAbility> l_Abs = new List<IAbility>();
        int l_Counter = 0;
        int l_Random;
        IAbility l_A;

        
        while (l_Counter<m_Buttons.Count)
        {
            List<IAbility> l_AvailableAbs = AbilitiesThatCanLevelUp(l_Abs);
            //pillar la lista  con abilitiesCanlevelup
            //revisar si está vacía
            if (l_AvailableAbs.Count == 0) return l_Abs;
            else
            {
                l_Random = Random.Range(0, l_AvailableAbs.Count);
                l_A = l_AvailableAbs[l_Random];

                if (!l_Abs.Contains(l_A))
                {
                    l_Abs.Add(l_A);
                    l_Counter++;
                }
            }
        }
        return l_Abs;
    }

    private bool AbilitiesCanLevelUp()
    {
        foreach(IAbility l_IAb in m_PlayerAbilities)
        {
            if (l_IAb.CanLevelUp()) return true;
        }
        return false;
    }


    private List<IAbility> AbilitiesThatCanLevelUp(List<IAbility> l_AlreadyChosen)
    {
        List<IAbility> l_result= new List<IAbility>();

        foreach (IAbility l_ab in m_PlayerAbilities)
        {
            if(l_ab.CanLevelUp() && !l_AlreadyChosen.Contains(l_ab)) l_result.Add(l_ab);
        }
        return l_result;
    }
*/
    public static void AddRestartElement(IRestartable l_Restartable)
    {
        m_RestartList.Add(l_Restartable);
    }

    public static void RestartGame()
    {
        foreach (IRestartable l_R in m_RestartList)
        {
         l_R.Restart();   
        }
        Time.timeScale = 1f;
        //EnemySpawner.RestartGame();
        

    }

    public  void RestartExp()
    {
        m_CurrentExp = 0;
        m_CurrentLevel = 0;
        m_NextLevelExp = m_ExpToFirstLevel;
        //m_LevelText.text=GetLevelText();
        AddExperience(0);

    }
    
}
