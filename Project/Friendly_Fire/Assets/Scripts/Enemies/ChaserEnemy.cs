using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : AEnemy, IRestartable
{
    [SerializeField] private float m_ChaseSpeed=3f;
    [SerializeField] private float m_DealtDamage =10f;
    [SerializeField] private int m_RecoilFrames=10;
    [SerializeField] private float m_RecoilSpeed=4f;
    private bool m_Active = false;
    //private Animator m_Animator;
    private Rigidbody2D m_EnemyRB;
    [SerializeField]private float m_SpawnCooldown;
    
    
    [Header("Hide sprites in spawn")]
    [SerializeField] private GameObject m_Sprite;
    /*[SerializeField] private GameObject m_SpawnSprite;
    [SerializeField] private GameObject m_DeathSprite_Hands;
    [SerializeField] private GameObject m_DeathSprite_Hole;
    [SerializeField] private GameObject m_DamagedSprite;
    
    */

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Friend Ability")
            {
                print("Call an ambulance");
                m_EnemyRB.velocity = new Vector2(0f, 0f);
                AbsBullet l_bullet = other.GetComponent<AbsBullet>();
                print(l_bullet.GetDamage());
                TakeDamage(l_bullet.GetDamage(), (transform.position-other.transform.position).normalized);
                l_bullet.DeactivateBullet();
            }else if (other.tag == "Player")
            {
                other.GetComponent<PlayerController>().PlayerTakesDamage(m_DealtDamage,(other.transform.position-transform.position).normalized, false);
                StartCoroutine(Recoil((transform.position-other.transform.position).normalized));
            }
    }
        
    void Start()
    {
        m_EnemyHealth = GetComponent<EnemyHealth>();
        ExpManager.AddRestartElement(this);
    }

    void Update()
    {
        if (!m_Recoiling && m_Active)
        {
            Chase();
        }
    }
    
    public override void Chase()
    {
        Vector2 l_direction = new Vector2(m_PlayerToChase.position.x-m_EnemyRB.position.x, m_PlayerToChase.position.y-m_EnemyRB.position.y);
        m_EnemyRB.position += l_direction.normalized * m_ChaseSpeed * Time.deltaTime;
    }

    public override void Die()
    {
        NewEnemyManager.DecreaseNumberOfEnemies(this);
        m_Active = false;
        m_EnemyRB.velocity = new Vector2(0f, 0f);
        //m_Animator.SetTrigger("Death");
        gameObject.SetActive(false);
    }
    

    public override void Spawn(Vector3 l_Position, Transform l_Player)
    {
        m_EnemyHealth = GetComponent<EnemyHealth>();
        transform.position = l_Position;
        m_Recoiling = false;
        m_EnemyRB = GetComponent<Rigidbody2D>();
        m_PlayerToChase = l_Player;
        m_EnemyHealth.StartEnemyHealth();
        m_Active = true;
        m_Recoiling = false;
        //m_Animator = GetComponent<Animator>();
        m_Sprite.SetActive(m_Active);
        gameObject.SetActive(m_Active);
        m_CurrentState = m_EnemyIAState.CHASE;
        
        /*
        //spawn hide
        m_SpawnSprite.SetActive(false);
        m_DeathSprite_Hands.SetActive(false);
        m_DeathSprite_Hole.SetActive(false);
        m_DamagedSprite.SetActive(false);
        */
    }

    public override void TakeDamage(float l_amount, Vector3 l_direction)
    {
        m_EnemyHealth.TakeDamage(l_amount);
        //m_Animator.SetTrigger("TakeDamage");
        if(m_EnemyHealth.IsAlive())StartCoroutine(Recoil(l_direction));
    }
    public override IEnumerator Recoil(Vector2 l_Direction)
    {
        m_Recoiling = true;
        Vector2 l_recoil = l_Direction * m_RecoilSpeed;
        for (int i = 0; i < m_RecoilFrames; i++)
        {
            m_EnemyRB.position += l_recoil * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_Recoiling = false;
    }
    public override float GetDealtDamage()
    {
        return m_DealtDamage;
    }

    public override float getSpawnCooldown()
    {
        return m_SpawnCooldown;
    }

    public void Restart()
    {
        gameObject.SetActive(false);
    }
    
}
