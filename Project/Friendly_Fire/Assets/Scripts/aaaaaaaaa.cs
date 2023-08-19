using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class aaaaaaaaa : MonoBehaviour
{
    public bool Damage = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth l_PH = other.GetComponent<PlayerHealth>();
            if(Damage) l_PH.TakeDamage(20f);
            else l_PH.RecoverHealth(10f);
        }
        
    }
}
