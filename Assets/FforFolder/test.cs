using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class test : PlayerCubeBehavior
{

	// Use this for initialization
	void Start () {
        if (!networkObject.IsOwner)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    // Этот оверрайд почему то необходимо прописывать для всех классов которые являются дочерними классами для PlayerCubeBehavior , используется только в атак системе
    public override void Attack(RpcArgs args)
    {
        throw new System.NotImplementedException();
    }
}
