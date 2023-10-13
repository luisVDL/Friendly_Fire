using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SupportFriend : AFriend
{
    
    // we need a list with all the other friends to add the status
    // we need a function to apply the status to every friend
    //each friend implements it's own function
    
    //to increase the attack of the non offensive friends we add time to the current ability duration
    // to reduce the cooldown we simply add time to the current time to activate the abilities faster (we avoid issues regarding the difference between time amounts)

    // Start is called before the first frame update

    [SerializeField] protected float m_StatusDuration; //for the cooldown will be also this variable

}
