using Facebook.MiniJSON;
using strange.extensions.signal.impl;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FacebookProvider : ISocialProvider
{
	[Inject]
	public FacebookLoggedSignal facebookLoggedSignal { get; set; }
	
	public void ConnectToSocial()
	{
		FB.Init(FacebookInited, OnHideUnity);
	}

	public void FacebookInited()
	{
		Debug.Log("FacebookInited");

		if (FB.IsLoggedIn)
		{
			Debug.Log("Already logged in");
		}
		else
		{
			Login();
		}
	}

	private void Login()
	{
		FB.Login("public_profile", AuthCallback);
	}

	/// <summary>
	/// Get First name of facebook account
	/// </summary>
	/// <param name="result"></param>
	private void AuthCallback(FBResult result)
	{
		Debug.Log("Auth: " + result.Error);

		FB.API("/me?fields=id,first_name", Facebook.HttpMethod.GET,
			(res) =>
			{
				GameData.Player.Name = DeserializeJSONProfile(res.Text);
				LocalDataProvider.Instance.SavePlayer(GameData.Player);
				facebookLoggedSignal.Dispatch();
			});
	}

	private static void OnHideUnity(bool isGameShown)
	{
		Debug.Log("OnHideUnity");
	}

	/// <summary>
	/// Parse facebook's response
	/// </summary>
	private string DeserializeJSONProfile(string response)
	{
		var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
		object nameH;

		if (responseObject.TryGetValue("first_name", out nameH))
		{
			return (string)nameH;
		}

		return "Unknown";
	}
}
