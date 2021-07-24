using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour 
{
    public GameObject localcam;
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.isMine)
        {
            localcam.SetActive(false);

            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        for(int i = 0; i<scripts.Length; i++)
         {
              if (scripts[i] is NetworkPlayer) continue;
              else if (scripts[i] is PhotonView) continue;

              scripts[i].enabled = false;
          }
       }
       
    }

}
