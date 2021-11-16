using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public List<bool> partyInvite;
    public List<GameObject> guest;

    void Update() 
    {
        if(CheckInvite() == true)
        {
            for(int x=0; x< guest.Count; x++)
            {
                guest[x].SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            partyInvite[0] = true;
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            partyInvite[1] = true;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            partyInvite[2] = true;
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            partyInvite[3] = true;
        }
    }

    public bool CheckInvite()
    {
        for( int i = 0; i< partyInvite.Count; ++i) 
            {
                if ( partyInvite[i] == false ) 
                {
                    return false;
                }
            }
        return true;
    }
}
