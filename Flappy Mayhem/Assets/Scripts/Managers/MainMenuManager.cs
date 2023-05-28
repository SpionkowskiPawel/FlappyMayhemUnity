using Org.BouncyCastle.Utilities.Encoders;
using System.Security.Cryptography;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.TextCore.Text;
using Nethereum.Util;
using System.Text.RegularExpressions;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    [SerializeField] private TextMeshProUGUI inputWalletText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        string targetAddress = Regex.Replace(inputWalletText.text, @"[^\x20-\x7F]", "");

        if (ValidateWallet(targetAddress))
        {
            inputWalletText.color = Color.green;

            PlayerPrefs.SetString("Wallet", targetAddress);

            SceneManager.LoadScene(1);
        }
        else
        {
            inputWalletText.color = Color.red;
        }
    }

    public void SetColor()
    {
        string targetAddress = Regex.Replace(inputWalletText.text, @"[^\x20-\x7F]", "");

        if (ValidateWallet(targetAddress))
        {
            inputWalletText.color = Color.green;
        }
        else
        {
            inputWalletText.color = Color.red;
        }
    }

    private bool ValidateWallet(string wallet)
    {
        Regex r = new("^(0x){1}[0-9a-fA-F]{40}$");
        if (!r.IsMatch(wallet))
        {
            return false;
        }
        else if (wallet == wallet.ToLower())
        {
            return true;
        }
        else
        {
            return new AddressUtil().IsChecksumAddress(wallet);
        }
    }
}
