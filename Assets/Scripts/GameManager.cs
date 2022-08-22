using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private UpgradeSettings upgradeSettingsInstance;

    [SerializeField] UpgradeSettings upgradeSettings;


    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            ResourceManager.Init();

            upgradeSettingsInstance = Instantiate(upgradeSettings);
            PlayerPrefs.DeleteAll();
        }

    }

    public UpgradeSettings GetUpgradeSettingsInstance() => upgradeSettingsInstance;
}
