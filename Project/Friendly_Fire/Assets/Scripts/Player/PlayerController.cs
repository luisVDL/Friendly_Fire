using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private bool m_AbilityObstacleActivated;
    
    [Header("Player Health")] [SerializeField] private PlayerHealth m_PlayerHealth;
     
    [Header("Movement")] private Rigidbody2D m_PlayerRB;

    private PlayerInputKeyboard m_PlayerInput_KEYBOARD;
    [SerializeField] private float m_MovementSpeed = 4f;
    private Vector2 m_MoveInput;
    private Vector3 m_StartPosition;
    
    [SerializeField] private Camera m_Camera;

    private Vector2 m_CursorPosition;
    private Vector2 m_MouseActualValue;

    [Header("Recoil")]
    [SerializeField] private float m_RecoilSpeed = 4f;
    [SerializeField] private int m_RecoilingFrames = 10;
    private bool m_Recoiling;

    [Header("Animation")] 
    private Animator m_Animator;
    

    void Awake()
    {
        m_PlayerInput_KEYBOARD = new PlayerInputKeyboard();
    }

    private void OnEnable()
    {
        m_PlayerInput_KEYBOARD.Enable();
    }
    
    private void OnDisable()
    {
        m_PlayerInput_KEYBOARD.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        //Cursor.visible = false;
        m_StartPosition = transform.position;
        TryGetComponent(out m_PlayerRB);
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        m_MoveInput.x = m_PlayerInput_KEYBOARD.GameMovement.HorizontalMovement.ReadValue<float>();
        m_MoveInput.y = m_PlayerInput_KEYBOARD.GameMovement.VerticalMovement.ReadValue<float>();
        m_PlayerRB.velocity = m_MoveInput * m_MovementSpeed * Time.deltaTime;


        AnimateMovement();
        /*
         * if(m_RB.velocity.x<0f){ // Change Sprite to LEFT}else{ // Change Sprite to RIGHT}
         */
    }

    private void AnimateMovement()
    {
        if (Mathf.Abs(m_MoveInput.x) > Mathf.Abs(m_MoveInput.y))//Vertical movement
        {
            if (m_MoveInput.x > 0)
            {
                m_Animator.SetBool("UP", false);
                m_Animator.SetBool("LEFT", false);
                m_Animator.SetBool("RIGHT", true);
                m_Animator.SetBool("DOWN", false);
            }
            else
            {
                m_Animator.SetBool("UP", false);
                m_Animator.SetBool("LEFT", true);
                m_Animator.SetBool("RIGHT", false);
                m_Animator.SetBool("DOWN", false);
            }
        }
        else //Horizontal movement
        {
            if (m_MoveInput.y < 0)
            {
                m_Animator.SetBool("UP", false);
                m_Animator.SetBool("LEFT", false);
                m_Animator.SetBool("RIGHT", false);
                m_Animator.SetBool("DOWN", true);
            }
            else
            {
                m_Animator.SetBool("UP", true);
                m_Animator.SetBool("LEFT", false);
                m_Animator.SetBool("RIGHT", false);
                m_Animator.SetBool("DOWN", true);//when i have the up animation this will change
            }
        }
    }
    
    
    public void PlayerTakesDamage(float l_DamageTaken, Vector3 l_Direction, bool bullet)
    {
        //m_Animator.SetTrigger("PlayerDamaged");
        m_PlayerHealth.TakeDamage(l_DamageTaken);
        if(!bullet) StartCoroutine(Recoil(l_Direction));
    }
    
    private IEnumerator Recoil(Vector3 l_Direction)
    {
        m_Recoiling = true;
        Vector2 l_recoil = l_Direction * m_RecoilSpeed;
        for (int i = 0; i < m_RecoilingFrames; i++)
        {
            m_PlayerRB.velocity += l_recoil * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        m_Recoiling = false;
    }
    
    public void Restart()
    {
        transform.position = m_StartPosition;
        m_PlayerHealth.PlayerRespawn();
    }

    public bool CanPassObstacle()
    {
        return m_AbilityObstacleActivated;
    }
}