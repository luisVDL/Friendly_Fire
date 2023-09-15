using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    //IMAGES
    [SerializeField] private Image m_REALLifebar;
    [SerializeField] private Image m_TRANSITIONLifebar;

    private float m_CurrentHealth, m_MaxHealth;

    //TIME
    [SerializeField] private float m_TransitionSpeed = 2f;
    private float m_LerpTime;

    //MENUS
    [SerializeField] private GameObject m_DeathMenu;

    void Awake()
    {
        m_CurrentHealth = 1f;
        m_MaxHealth = 1f;
    }


    void FixedUpdate()
    {
        AnimateLifebar();
    }

    public void DecreaseLifebar(float l_currentHealth, float l_MaxHealth)
    {
        if (l_currentHealth <= 0)
        {
            ShowDeathMenu();
        }
        else
        {
            m_CurrentHealth = l_currentHealth;
            m_MaxHealth = l_MaxHealth;
            m_LerpTime = 0f;
            m_REALLifebar.fillAmount = m_CurrentHealth / m_MaxHealth;
            AnimateLifebar();
        }
    }

    public void IncreaseLifebar(float l_currentHealth, float l_MaxHealth)
    {
        m_CurrentHealth = l_currentHealth;
        m_MaxHealth = l_MaxHealth;
        m_LerpTime = 0f;
        m_TRANSITIONLifebar.fillAmount = m_CurrentHealth / m_MaxHealth;
        AnimateLifebar();
    }

    private void AnimateLifebar()
    {
        float l_FillFront = m_REALLifebar.fillAmount;
        float l_FillBack = m_TRANSITIONLifebar.fillAmount;
        float l_HFraction = m_CurrentHealth / m_MaxHealth;
        if (l_FillBack > l_HFraction)
        {
            m_LerpTime += Time.deltaTime;
            float percentComplete = m_LerpTime / m_TransitionSpeed;
            m_TRANSITIONLifebar.fillAmount = Mathf.Lerp(l_FillBack, l_HFraction, percentComplete);
        }
        else if (l_FillFront < l_HFraction)
        {
            m_LerpTime += Time.deltaTime;
            float percentComplete = m_LerpTime / m_TransitionSpeed;
            m_REALLifebar.fillAmount = Mathf.Lerp(l_FillFront, l_HFraction, percentComplete);
        }
    }

    public void ShowDeathMenu()
    {
        //m_GameOverSoundAS.PlayOneShot(m_GameOverSound);
        //m_GameplayAS.Stop();
        Time.timeScale = 0f;
        m_DeathMenu.gameObject.SetActive(true);
        //m_GameOverAS.Play();
        //EventSystem.current.SetSelectedGameObject(null);
        //set a new active selected object
        //EventSystem.current.SetSelectedGameObject(m_FirstSelectedButton);
    }
}