using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class PlayFab_Manager : MonoBehaviour
{
    public InputField userEmail;
    public InputField userPassword;
    public InputField Createusername;
    public InputField CreateEmail;
    public InputField CreatePassword;
    public GameObject LoginPannel;
    public GameObject CreatePannel;
    public GameObject DPNamePannel;
    public Text DisplayName;
    public InputField DPName;
    public void Start()
    {
       
        CreatePannel.SetActive(false); 
        DPNamePannel.SetActive(false);
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "668E1"; // Please change this value to your own titleId from PlayFab Game Manager
        }

        if (PlayerPrefs.HasKey("EMAIL3"))
        {
            userEmail.text = PlayerPrefs.GetString("EMAIL");
            userPassword.text = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithEmailAddressRequest { Email = userEmail.text, Password = userPassword.text , InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
            };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
            
        }
      
    }

    private void OnLoginSuccess(LoginResult result)
    {
        PlayerPrefs.SetString("EMAIL", userEmail.text);
        PlayerPrefs.SetString("PASSWORD", userPassword.text);
        Debug.Log("Congratulations, you made your first successful API call!");


        {
            string name = null;
            if (result.InfoResultPayload.PlayerProfile == null)
                name = result.InfoResultPayload.PlayerProfile.DisplayName;

            if (name == null)
                DisplayName.text = result.InfoResultPayload.PlayerProfile.DisplayName;
            else
                DisplayName.text = result.InfoResultPayload.PlayerProfile.PlayerId;
        }

        LoginPannel.SetActive(false);
        CreatePannel.SetActive(false);
        DPNamePannel.SetActive(false);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        PlayerPrefs.SetString("EMAIL", userEmail.text);
        PlayerPrefs.SetString("PASSWORD", userPassword.text);
        Debug.Log("Congratulations, Registered ! you made your first successful API call!");
        LoginPannel.SetActive(true);
        CreatePannel.SetActive(false);
        DPNamePannel.SetActive(false);
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Nahh register nhi hua");
      //  Debug.Log(error.GenerateErrorReport());
        CreatePannel.SetActive(false);
        LoginPannel.SetActive(true);
        DPNamePannel.SetActive(false);

    }
    private void OnLoginFailure(PlayFabError error)
    {
       // Debug.LogWarning("Something went wrong with your first API call.  :(");
       Debug.Log("Here's some debug information:");
       // Debug.LogError(error.GenerateErrorReport());
        LoginPannel.SetActive(false);
        CreatePannel.SetActive(true);

    }

    public void OnClickCreateAccount()
    {
        var requestregister = new RegisterPlayFabUserRequest { Email = CreateEmail.text, Password = CreatePassword.text,  RequireBothUsernameAndEmail =false };
        PlayFabClientAPI.RegisterPlayFabUser(requestregister, OnRegisterSuccess, OnRegisterFailure);
        
    }

    public void clickDisplayName()
    {
        DPNamePannel.SetActive(true);
        CreatePannel.SetActive(false);
        LoginPannel.SetActive(false);

    }
    public void UpdateName()
    {
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = DPName.text };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisPlayNameSuccess, OnError);

    }
    void OnDisPlayNameSuccess( UpdateUserTitleDisplayNameResult result)
    {
        DPNamePannel.SetActive(false);
        CreatePannel.SetActive(false);
        LoginPannel.SetActive(false);

        DisplayName.text = DPName.text;
        Debug.Log("NameUpdated");
    } 
    private void OnError(PlayFabError error)
    {
        clickDisplayName();
    }
    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = userEmail.text, Password = userPassword.text, InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
        {
            GetPlayerProfile = true
        }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        
    }

    public void Logout()
    {
       
    }
}

