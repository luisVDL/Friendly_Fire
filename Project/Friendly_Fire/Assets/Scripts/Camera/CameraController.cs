using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    // Start is called before the first frame update

    [SerializeField] private Transform m_UpLeftLimit;
    [SerializeField] private Transform m_UpRightLimit;
    [SerializeField] private Transform m_DownLeftLimit;
    [SerializeField] private Transform m_DownRightLimit;
    private Vector3 m_Aux;
    
    void Start()
    {
        m_Aux= new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
        transform.position = m_Aux;
    }

    // Update is called once per frame
    void Update()
    {
        m_Aux.x = Mathf.Clamp(m_Target.position.x, m_DownLeftLimit.position.x, m_DownRightLimit.position.x);
        m_Aux.y = Mathf.Clamp(m_Target.position.y, m_DownLeftLimit.position.y, m_UpLeftLimit.position.y);
        transform.position = m_Aux;


    }
}
