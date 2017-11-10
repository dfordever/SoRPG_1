using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Unity;




public class Gamemanager : MonoBehaviour {

    public GameObject spawnpos;


    // Use this for initialization
    private void Start()
    {
       
        //NetworkManager.Instance.InstantiatePlayerPrefabNetworkObject();
        NetworkManager.Instance.InstantiatePlayerCubeNetworkObject();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
