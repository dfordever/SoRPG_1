using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class Player_Health_System : PlayerCubeBehavior
{

    public float Health;
    // Use this for initialization
    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (!networkObject.IsOwner)
        {
            return;
        }
        Health = 150;

    }
	
	// Update is called once per frame
	void Update () {
        if (!networkObject.IsOwner)
        {
            // Если не владелец этого объекта то взять данные с сервера
            Health = networkObject.health;
            return;
        }
        //Если владеем объектом то выполняем логику
        networkObject.health = Health;
        if(Health<0)
        {
            networkObject.Destroy();
        }

    }


    public override void Attack(RpcArgs args)
    {
        Debug.Log("poluchil uron");
        Health -= args.GetNext<float>();
    }
}
