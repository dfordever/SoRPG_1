using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
public class Player_Melee_Attack_System : PlayerCubeBehavior
{
    // peremenii napisat v private e.t.c for security
    public float Damage;
    public bool meleeallowed;
    public bool rangedallowed;
    public bool attackallowed;
    [System.NonSerialized]
    public Player_Health_System victim;
    public PlayerCubeBehavior Damage_text;



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
                    //rpc для отправки урона потерпевшему
                    victim.networkObject.SendRpc(RPC_ATTACK, Receivers.Owner,Damage);
                    //тот же самый рпц но на этом объекте для отображения урона по жертве
                    networkObject.SendRpc(RPC_ATTACK, Receivers.Owner, Damage);
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
        //спавнится текст
        Damage_text = NetworkManager.Instance.InstantiatePlayerCubeNetworkObject(1, victim.transform.position + Vector3.up, victim.transform.rotation, true);
        Damage_text.gameObject.GetComponent<GUIText>().text = args.GetNext<float>().ToString();
        Debug.Log("attack");
    }

}

