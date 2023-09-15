using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject m_SettingsPanel;
    [SerializeField] private GameObject m_FirstSettingsButtonSelected;
    
    [Header("Bestiary menu")] 
    [SerializeField] private GameObject m_BestiaryPanel;
    [SerializeField] private GameObject m_FirstBestiaryButtonSelected;

    [Header("Pause Menu")] 
    [SerializeField] private GameObject m_PauseMenuPanel;
    [SerializeField] private GameObject m_FirstPauseButtonSelected;
    private PlayerInputKeyboard m_PlayerInput_KEYBOARD;
    
    
    [Header("Events")] 
    [SerializeField] private UnityEvent ResumeGame;
    
    void Awake()
    {
        m_PlayerInput_KEYBOARD = new PlayerInputKeyboard();
    }
    private void OnEnable()
    {
        m_PlayerInput_KEYBOARD.Enable();
    }
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(m_FirstPauseButtonSelected);
    }

    void Update()
    {
       if(m_PlayerInput_KEYBOARD.Pause.Pause.WasPressedThisFrame()) PauseGame();
    }
    public void QuitGame()
    {
        Application.Quit(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("01_Forest");
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("00_StartMenu");
        Time.timeScale = 1f;
    }

    public void ContinueGame()
    {
        m_PauseMenuPanel.SetActive(false);
        ResumeGame.Invoke();
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (!m_PauseMenuPanel.activeInHierarchy)
        {
            m_PauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(null);
            //set a new active selected object
            EventSystem.current.SetSelectedGameObject(m_FirstPauseButtonSelected);
        }
        else
        {
            m_PauseMenuPanel.SetActive(false);
            m_SettingsPanel.SetActive(false);
            ResumeGame.Invoke();
            Time.timeScale = 1f;
        }
    }

    public void OpenBestiary()
    {
        m_BestiaryPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        //set a new active selected object
        EventSystem.current.SetSelectedGameObject(m_FirstBestiaryButtonSelected);
        
    }

    public void CloseBestiary()
    {
        m_BestiaryPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_FirstPauseButtonSelected);

    }

    public void OpenSettings()
    {
        m_SettingsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        //set a new active selected object
        EventSystem.current.SetSelectedGameObject(m_FirstSettingsButtonSelected);
    }

    public void CloseSettings()
    {
        m_SettingsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_FirstPauseButtonSelected);
    }
}
