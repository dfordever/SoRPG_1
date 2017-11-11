using UnityEngine;
using System.Collections;

public class Player_Melee_Collision_System : MonoBehaviour {

    Player_Melee_Attack_System atcksys;
    // Use this for initialization
    void Start () {
        atcksys = transform.parent.transform.parent.GetComponent<Player_Melee_Attack_System>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            //Обращаться не отсюда в атак систем а из атак систем сюда!
            atcksys.meleeequiped = false;
        }
    }
}
