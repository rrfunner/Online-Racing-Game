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
    
    public void Start()
    {
       
        CreatePannel.SetActive(false);
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "668E1"; // Please change this value to your own titleId from PlayFab Game Manager
        }

        if (PlayerPrefs.HasKey("EMAIL1"))
        {
            userEmail.text = PlayerPrefs.GetString("EMAIL1");
            userPassword.text = PlayerPrefs.GetString("PASSWORD");
            var request = new LoginWithEmailAddressRequest { Email = userEmail.text, Password = userPassword.text };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
            
        }
      
    }

    private void OnLoginSuccess(LoginResult result)
    {
        PlayerPrefs.SetString("EMAIL1", userEmail.text);
        PlayerPrefs.SetString("PASSWORD", userPassword.text);
        Debug.Log("Congratulations, you made your first successful API call!");
        LoginPannel.SetActive(false);
        CreatePannel.SetActive(false);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        PlayerPrefs.SetString("EMAIL1", userEmail.text);
        PlayerPrefs.SetString("PASSWORD", userPassword.text);
        Debug.Log("Congratulations, Registered ! you made your first successful API call!");
        LoginPannel.SetActive(true);
        CreatePannel.SetActive(false);
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Nahh register nhi hua");
      //  Debug.Log(error.GenerateErrorReport());
        CreatePannel.SetActive(false);
        LoginPannel.SetActive(true);

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
        var requestregister = new RegisterPlayFabUserRequest { Email = CreateEmail.text, Password = CreatePassword.text, Username = Createusername.text , RequireBothUsernameAndEmail =false };
        PlayFabClientAPI.RegisterPlayFabUser(requestregister, OnRegisterSuccess, OnRegisterFailure);
      
    }
    
    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = userEmail.text, Password = userPassword.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

}

