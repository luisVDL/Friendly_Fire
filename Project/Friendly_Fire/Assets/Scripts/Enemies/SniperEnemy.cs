using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SniperEnemy : AEnemy, IRestartable
{
    [Header("CHASE")] 
    [SerializeField] private float m_ChaseSpeed = 2.5f;
    private Vector3 m_CurrentPosition = new Vector3(0f, 0f, 0f);

    [Header("ATTACK")] 
    [SerializeField] private float m_DistanceToAttack = 3f;
    [SerializeField] private float m_DealtDamage = 10f;
    private PoolScript m_SniperBulletPool;
    [SerializeField] private GameObject m_BulletOrigin;
    private Vector3 m_DizzyScatter = new Vector3();

    
    /*
    [SerializeField] private AudioClip m_FourWayShootSound;
    [SerializeField] private AudioSource m_FourwayAudioSource;
    */

    
    [Header("PREPARING")] 
    [SerializeField] private float m_MaxDistanceToPrepare = 4.5f;
    /*[SerializeField] private float m_TimeToPrepare = 2f;
    private float m_PreparationStartTime;
    [SerializeField] private float m_LaserTimeShow=0.5f;
    private float m_PreparationLaserStart;
    */

    [Header("RECOIL")] [SerializeField] private int m_RecoilFrames = 10;
    [SerializeField] private float m_RecoilSpeed = 4f;
    
    private Animator m_Animator;
    private Rigidbody2D m_EnemyRB;

    [Header("Hide sprites in spawn")]
    [SerializeField] private GameObject m_Sprite;
    /*[SerializeField] private GameObject m_SpawnSprite;
    [SerializeField] private GameObject m_DeathSprite_Hands;
    [SerializeField] private GameObject m_DeathSprite_Hole;
    [SerializeField] private GameObject m_DamagedSprite;
    */
    
    //SPAWN
    void Awake()
    {
        m_SubStatuses = new List<ASubStatus>();
        ExpManager.AddRestartElement(this);
        m_EnemyRB = GetComponent<Rigidbody2D>();
        m_EnemyHealth = GetComponent<EnemyHealth>();
        m_EnemyHealth.StartEnemyHealth();
        m_Animator = GetComponent<Animator>();
        //Navmesh
        m_NavMeshAgent.updateRotation = false;
        m_NavMeshAgent.updateUpAxis = false;
    }
    

    public override void Spawn(Vector3 l_Position, Transform l_Player)
    {
        m_Dizzy = 1;
        m_EnemyHealth = GetComponent<EnemyHealth>();
        transform.position = l_Position;
        m_Recoiling = false;
        //m_EnemyRB = GetComponent<Rigidbody2D>();
        m_PlayerToChase = l_Player;
        m_ChasedObject = m_PlayerToChase;
        m_EnemyHealth.StartEnemyHealth();
        m_Recoiling = false;
        m_Animator = GetComponent<Animator>();
        m_Sprite.SetActive(true);
        gameObject.SetActive(true);
        m_CurrentState = m_EnemyIAState.CHASE;
        //
        /*
        //spawn hide
        m_SpawnSprite.SetActive(false);
        m_DeathSprite_Hands.SetActive(false);
        m_DeathSprite_Hole.SetActive(false);
        m_DamagedSprite.SetActive(false);
        */
        
    }
    
    public override void ChangeToCooldownState()
    {
        m_CurrentState = m_EnemyIAState.COOLDOWN;
    }

    public void SetBulletPool(PoolScript l_Pool)
    {
        m_SniperBulletPool = l_Pool;
    }

    // CHANGE STATES
    void Update()
    {
        switch (m_CurrentState)
        {
            case m_EnemyIAState.CHASE:
                Chase();
                ChangeFromChase();
                break;
            case m_EnemyIAState.PREPARING:
                Preparing();
                ChangeFromPreparing();
                break;
            case m_EnemyIAState.ATTACKING:
                Attack();
                ChangeFromAttacking();
                break;
        }
    }

    //CHASE
    private void ChangeFromChase()
    {
        m_CurrentPosition.x = m_EnemyRB.position.x;
        m_CurrentPosition.y = m_EnemyRB.position.y;
        if ((m_CurrentPosition - m_PlayerToChase.position).magnitude >= m_DistanceToAttack &&
            (m_CurrentPosition - m_PlayerToChase.position).magnitude < m_MaxDistanceToPrepare)
        {
            m_CurrentState = m_EnemyIAState.ATTACKING;
            
            //m_PreparationStartTime = Time.time;
            //m_CurrentState = m_EnemyIAState.PREPARING;
            //m_PreparationLaserStart = 0;
        }
    }

    public override void Chase()
    {
        /*Vector2 l_direction = new Vector2(m_PlayerToChase.position.x - m_EnemyRB.position.x,
            m_PlayerToChase.position.y - m_EnemyRB.position.y)*m_Dizzy*m_SpeedMultiplier;
        m_EnemyRB.position += l_direction.normalized * m_ChaseSpeed * Time.deltaTime;*/
        
        m_NavMeshAgent.SetDestination(m_ChasedObject.position);
    }

    //PREPARING --> Pending to decide if i delete it
    private void Preparing()
    {
        //m_PreparationLaserStart += Time.deltaTime;
    }

    private void ChangeFromPreparing()
    {
        /*if (m_PreparationStartTime + m_TimeToPrepare <= Time.time)
        {

            m_CurrentState = m_EnemyIAState.ATTACKING;
        }
        else if ((m_CurrentPosition - m_PlayerToChase.position).magnitude < m_DistanceToAttack ||
                 (m_CurrentPosition - m_PlayerToChase.position).magnitude > m_MaxDistanceToPrepare)
        {
            m_CurrentState = m_EnemyIAState.CHASE;
        }*/
    }
    
    //ATTACKING
    private void Attack()
    {
        m_NavMeshAgent.isStopped = true;
        m_Animator.SetTrigger("Attack");
        /*
        GameObject l_GM = m_SniperBulletPool.EnableObject();
        Vector3 l_RBPosition = new Vector3(m_BulletOrigin.transform.position.x, m_BulletOrigin.transform.position.y, 0f);
        Quaternion l_Rotation = GetBulletRotation((m_PlayerToChase.position-l_RBPosition).normalized);
        l_GM.GetComponent<AbsBullet>().FireBullet((m_PlayerToChase.position-l_RBPosition), l_RBPosition-(l_RBPosition-m_PlayerToChase.position).normalized,l_Rotation);
        */
        //m_FourwayAudioSource.PlayOneShot(m_FourWayShootSound);
    }

    public void ShootBullet()
    {
        GameObject l_GM = m_SniperBulletPool.EnableObject();
        Vector3 l_RBPosition = new Vector3(m_BulletOrigin.transform.position.x, m_BulletOrigin.transform.position.y, 0f);
        //Quaternion l_Rotation = GetBulletRotation((m_PlayerToChase.position-l_RBPosition).normalized);

        m_DizzyScatter = GetDizzyScatter();
        l_GM.GetComponent<AbsBullet>().FireBullet((m_PlayerToChase.position-l_RBPosition)+m_DizzyScatter, l_RBPosition-(l_RBPosition-m_PlayerToChase.position).normalized,Quaternion.identity);
        m_NavMeshAgent.isStopped = false;
    }
    
    private Vector3 GetDizzyScatter()
    {
        if (m_Dizzy < 1)
        {
            
            int x = Random.Range(-1, 1);
            int y = Random.Range(-1, 1);
            return new Vector3(x, y,0)*3;
        }
        else return Vector3.zero;
    }

    private void ChangeFromAttacking()
    {
        //m_CurrentState = m_EnemyIAState.PREPARING;
        m_CurrentState = m_EnemyIAState.COOLDOWN;
        //m_PreparationStartTime = Time.time;
        //m_PreparationLaserStart = 0;
    }
    


    //TAKE DAMAGE
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Friend Ability")
        {
            m_EnemyRB.velocity = new Vector2(0f, 0f);
            AbsBullet l_bullet = other.GetComponent<AbsBullet>();
            TakeDamage(l_bullet.GetDamage(), (transform.position-other.transform.position).normalized);
            l_bullet.DeactivateBullet();
        }else if (other.tag == "Player")
        {
            m_Animator.SetTrigger("Attack");
            m_EnemyRB.velocity = new Vector2(0f, 0f);
            other.GetComponent<PlayerController>().PlayerTakesDamage(m_DealtDamage,(other.transform.position-transform.position).normalized, false);
            StartCoroutine(Recoil((transform.position-other.transform.position).normalized));
        }else if (other.tag == "Friend Status Ability")
        {
            m_EnemyRB.velocity = new Vector2(0f, 0f);
            ASubStatusSetter l_Setter = other.GetComponent<ASubStatusSetter>();
            l_Setter.setSubStatus(this);
            //Take the status from a method and add it to the list
        }

    }

    public override void TakeDamage(float l_amount, Vector3 l_direction)
    {
        
        m_EnemyHealth.TakeDamage(l_amount);
        //m_Animator.SetTrigger("TakeDamage");
        if (m_EnemyHealth.IsAlive()) StartCoroutine(Recoil(l_direction));
    }

    public override void Die()
    {
        RemoveAllSubStatuses();
        NewEnemyManager.DecreaseNumberOfEnemies(this);
        m_EnemyRB.velocity = new Vector2(0f, 0f);
        //m_Animator.SetTrigger("Death");
        gameObject.SetActive(false);
        
    }

    //RECOIL
    public override IEnumerator Recoil(Vector2 l_Direction)
    {
        m_NavMeshAgent.isStopped = true;
        m_NavMeshAgent.speed = 0.0f;
        m_NavMeshAgent.acceleration = 0.0f;
        m_Recoiling = true;
        Vector2 l_recoil = l_Direction * m_RecoilSpeed;
        for (int i = 0; i < m_RecoilFrames; i++)
        {
            m_EnemyRB.position += l_recoil * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        m_Recoiling = false;
        m_NavMeshAgent.isStopped = false;
        m_NavMeshAgent.speed = m_AgentSpeed;
        m_NavMeshAgent.acceleration = m_AgentAcceleration;
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

    public override void ChangeFromCooldown()
    {
        if (m_SpeedMultiplier < 1f)
        {
            StartCoroutine(WaitForSlowDownSubState());
        }
        else
        {
            m_CurrentState = m_EnemyIAState.CHASE;
        }
    }

    private IEnumerator WaitForSlowDownSubState()
    {
        yield return new WaitForSeconds(0.4f);
        m_CurrentState = m_EnemyIAState.CHASE;
    }
}
