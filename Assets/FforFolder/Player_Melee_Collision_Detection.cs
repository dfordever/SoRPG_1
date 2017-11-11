using UnityEngine;
using System.Collections;
//using BeardedManStudios.Forge.Networking.Generated;

public class Player_Melee_Collision_Detection : MonoBehaviour
{


    public Player_Melee_Attack_System Attacksys;
    // Use this for initialization
    void Start () {
       // if (!networkObject.IsOwner)
        //{
         //   return;
        //}

        Attacksys = transform.parent.transform.parent.GetComponent<Player_Melee_Attack_System>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider other)
    {
        ///if (!networkObject.IsOwner)
       // {
       //     return;
        //}


        if (other.gameObject.tag == "Player" && other.gameObject != gameObject)
        {
            Debug.Log("victim");
            Attacksys.vicobj = other.gameObject;
            Attacksys.victim = other.GetComponent<Player_Health_System>();
           
        }
    }

    }

