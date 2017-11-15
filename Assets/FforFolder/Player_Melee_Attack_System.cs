using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class Player_Melee_Attack_System : PlayerCubeBehavior
{
    // peremenii napisat v private e.t.c for security
    public float Damage;
    public bool meleeallowed;
    public bool rangedallowed;
    public bool attackallowed;
    //[System.NonSerialized]
    public Player_Health_System victim;



    // Use this for initialization
    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (!networkObject.IsOwner)
        { 
            //Если не владеем объектом то ничего не делаем
            return;
        }
        attackallowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!networkObject.IsOwner)
        {
            return;
        }

    }

   

    void OnTriggerStay(Collider other)
    {

        
    if (meleeallowed&&attackallowed)
    {
            if (!networkObject.IsOwner)
            {
                //Если не владеем объектом то ничего не делаем
                return;
            }
            //По клику мыши атакуем цель в ренже нашего персонажа, если цель является другим игроком
            if (Input.GetMouseButtonDown(0))
        {
            if (other.gameObject.tag == "Player")//&&other!=gameObject)
            {
                    victim = other.gameObject.GetComponent<Player_Health_System>();
                    victim.networkObject.SendRpc(RPC_ATTACK, Receivers.Owner,Damage);
                    
                  //  Debug.Log("attack");
                    //запрещаем атаковать снова пока не пройдет кулдаун атаки 
                    attackallowed = false;
                    StartCoroutine("Attackdelay");
            }
        }
    }

    }
    
    //Спустя 2 секунды разрешаем атаковать снова (чтобы нельзя было наносит 10 ударов в секунду а только по анимации)
    private IEnumerator Attackdelay()
    {
        
        yield return new WaitForSeconds(2);
        attackallowed = true;
    }
    //RPC CALL to the server
    public override void Attack(RpcArgs args)
    {
       // victim.Health -= 36.0f;//args.GetNext<float>();
        Debug.Log("attack");
    }

}

