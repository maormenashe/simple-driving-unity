using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private AndroidNotificationsHandler androidNotificationHandler;
    [SerializeField] private IOSNotificationHandler iOSNotificationHandler;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;

    private int energy;

    private const string ENERGY_KEY = "Energy";
    private const string ENERGY_READY_KEY = "EnergyReady";

    private void Start()
    {
        int currentHighScore = PlayerPrefs.GetInt(ScoreSystem.HIGH_SCORE_KEY, 0);
        highScoreText.text = $"High Score: {currentHighScore}";

        energy = PlayerPrefs.GetInt(ENERGY_KEY, maxEnergy);
        if (energy == 0)
        {
            ManageEnergyRecharge();
        }

        energyText.text = $"Play ({energy})";
    }

    private void ManageEnergyRecharge()
    {
        string energyReadyString = PlayerPrefs.GetString(ENERGY_READY_KEY, string.Empty);

        if (energyReadyString == string.Empty)
        {
            return;
        }

        if (!DateTime.TryParse(energyReadyString, out DateTime energyReadyAt))
        {
            return;
        }

        if (DateTime.Now >= energyReadyAt)
        {
            energy = maxEnergy;
            PlayerPrefs.SetInt(ENERGY_KEY, energy);
        }

    }

    public void Play()
    {
        if (energy < 1)
        {
            return;
        }

        energy--;
        PlayerPrefs.SetInt(ENERGY_KEY, energy);

        if (energy == 0)
        {
            DateTime energyReadyAt = DateTime.Now.AddMinutes(energyRechargeDuration);

            PlayerPrefs.SetString(ENERGY_READY_KEY, energyReadyAt.ToString());

#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReadyAt);
#elif UNITY_IOS
            iOSNotificationHandler.ScheduleNotification(energyRechargeDuration);
#endif
        }

        SceneManager.LoadScene("Scene_Game");
    }
}
