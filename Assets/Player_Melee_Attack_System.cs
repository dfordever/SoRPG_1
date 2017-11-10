using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;

public class Player_Melee_Attack_System : PlayerCubeBehavior
{
    public float Damage;
    public GameObject HitBox;
    private bool meleeequiped;
   // private Player_Health_System victim;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider other)
    {
        if(meleeequiped)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(other.gameObject.tag=="Player")
                {
                    /* victim=other.gameObject.GetComponent<Player_Health_System>();
                     * victim.health-=Damage;
                     * */
                }
            }
        }
    }
    }
