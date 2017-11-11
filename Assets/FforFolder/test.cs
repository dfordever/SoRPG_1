using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;

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
}
