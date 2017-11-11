using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;

public class Player_Melee_Attack_System : PlayerCubeBehavior
{
    // peremenii napisat v private e.t.c for security
    public float Damage;
    public GameObject HitBox;
    public bool meleeequiped;
    
    //[System.NonSerialized]
    public Player_Health_System victim;
   
   
    // Use this for initialization
    void Start () {

        
        
        if (!networkObject.IsOwner)
        {
            transform.GetChild(1).GetChild(1).gameObject.SetActive(false);

            return;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (!networkObject.IsOwner)
        {
            return;
        }
        
        if (meleeequiped)
        {
           
            if (Input.GetMouseButtonDown(0))
            {
                
                if (victim)
                //=========================================================PEREPISAT NA COROUTINE======================================
                {
                    Debug.Log("attack");
                    victim.Health -= Damage;
                      
                }
            }
        }


        }
   
    
    }
