using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendParentRotationScript : MonoBehaviour
{
    [SerializeField] private float m_RotationSpeed = 50f;
    private Vector3 m_RotationDirection = new Vector3(0, 0, 1);
    
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_RotationDirection, m_RotationSpeed);
    }
}
