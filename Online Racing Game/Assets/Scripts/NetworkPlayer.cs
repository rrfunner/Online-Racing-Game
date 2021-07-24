using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayer : Photon.MonoBehaviour 
{
   public GameObject localcam;
    private PhotonView pv;
    

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.isMine)
        {          
         localcam.SetActive(false);

            MonoBehaviour[] script = GetComponents<MonoBehaviour>();
            for( int i = 0;i < script.Length; i++)
            {
                if (script[i] is NetworkPlayer) continue;
                else if(script[i] is PhotonView) continue;
                script[i].enabled = false;
          }
        }
       
    }
    private void Update()
    {
        if (!pv.isMine) return;
    }

}
