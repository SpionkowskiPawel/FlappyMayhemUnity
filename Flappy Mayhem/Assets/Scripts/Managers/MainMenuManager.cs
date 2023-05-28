using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

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
        PlayerPrefs.SetString("Wallet", inputWalletText.text);
        SceneManager.LoadScene(1);
    }
}
