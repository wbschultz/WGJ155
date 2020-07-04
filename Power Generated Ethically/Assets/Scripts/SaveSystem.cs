using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

/**
 * <summary>Class <c>SaveSystem</c> saves and loads game data.</summary>
 */
public class SaveSystem : SingletonBase<SaveSystem>
{
    public CoolerCookieClicker resourceManager;
    List<PassiveUnit> passiveUnits = new List<PassiveUnit>();
    List<PassiveUpgrade> passiveUpgrades = new List<PassiveUpgrade>();

    public void Start()
    {
        GameObject[] dataManagers = GameObject.FindGameObjectsWithTag("DataManager");

        foreach (GameObject dataManager in dataManagers)
        {
            // Keep track resource data managers to save/load data
            PassiveUnit passiveUnit = dataManager.GetComponent<PassiveUnit>();
            PassiveUpgrade passiveUpgrade = dataManager.GetComponent<PassiveUpgrade>();
            
            if (passiveUnit != null && passiveUpgrades != null)
            {
                passiveUnits.Add(passiveUnit);
                passiveUpgrades.Add(passiveUpgrade);
            }
        }

        if (PlayerPrefs.GetString("Load_Save", "false") == "true")
        {
            // Player wants to load save data
            this.LoadAllData();
        } 
    }

    /**
     * <summary>Serialize all of game's data and write to a file</summary>
     * <param name="resources">Game resource tracking data</param>
     * <param name="passiveUnitPropsList">PassiveUnit properties</param>
     */
    public void SaveAllData()
    {
        /**
         * Create binary formatter and setup input stream to write data to. The
         * FileStream uses a persistent path that doesn't change regardless
         * of file system.
         */
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "gameData.bin";
        FileStream stream = null;

        UnityEngine.Debug.Log("SaveSystem::SaveAllData() filePath: " + filePath);
        try
        {
            // Convert PassiveUnit to serializable class type and write to file
            stream = new FileStream(filePath, FileMode.Create);
            SaveData gameData = new SaveData(resourceManager, passiveUnits, passiveUpgrades);
            binaryFormatter.Serialize(stream, gameData);
        }
        finally
        {
            if (stream != null)
            {
                // Regardless of any exceptions, close stream if it exists
                stream.Close();
            }
        }
    }


    /**
     * <summary>Deserialize all game data from file and into memory for
     * in game use.</summary>
     */
    public void LoadAllData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "gameData.bin";
        FileStream stream = null;

        UnityEngine.Debug.Log("SaveSystem::LoadAllData() filePath: " + filePath);

        try
        {
            if (File.Exists(filePath))
            {
                // File exists, open it and read PassiveUnit data
                UnityEngine.Debug.Log("SaveSystem::LoadPassiveUnit() file exists");
                stream = new FileStream(filePath, FileMode.Open);
                SaveData saveData = binaryFormatter.Deserialize(stream) as SaveData;

                this.RestoreDataFromSave(saveData);
            }
            else
            {
                Debug.LogError("Save file not found in " + filePath);
                return;
            }
        }
        finally
        {
            PlayerPrefs.SetString("Load_Save", "false");
            if (stream != null)
            {
                // Regardless of exceptions, close stream if it exists.
                stream.Close();
            }
        }
    }

    /**
     * <summary>Restore previous save data into current game state</summary>
     * <param name="saveData">Previous save data</param>
     */
    void RestoreDataFromSave(SaveData saveData)
    {
        resourceManager.RestoreDataFromSave(saveData);

        foreach (PassiveUnit passiveUnit in this.passiveUnits)
        {
            passiveUnit.RestoreDataFromSave(saveData);
        }

        foreach (PassiveUpgrade passiveUpgrade in this.passiveUpgrades)
        {
            passiveUpgrade.RestoreDataFromSave(saveData);
        }
    }

    /**
     * <summary>Serialize PassiveUnit data and write to a file.</summary>
     * <param name="passiveUnit">Energy generation upgrade data</param>
     */
    public void SavePassiveUnit (PassiveUnitProps passiveUnitProps)
    {
        /**
         * Create binary formatter and setup input stream to write data to. The
         * FileStream uses a persistent path that doesn't change regardless
         * of file system.
         */
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + passiveUnitProps.unitType + "Unit.bin";
        FileStream stream = null;

        UnityEngine.Debug.Log("SaveSystem::SavePassiveUnit() filePath: " + filePath);
        try
        {
            // Convert PassiveUnit to serializable class type and write to file
            stream = new FileStream(filePath, FileMode.Create);
            PassiveUnitData passiveUnitData = new PassiveUnitData(passiveUnitProps);
            binaryFormatter.Serialize(stream, passiveUnitData);
        } finally
        {
            if (stream != null)
            {
                // Regardless of any exceptions, close stream if it exists
                stream.Close();
            }
        }
    }

    /**
     * <summary>Deserialize PassiveUnit data from file and into memory for
     * in game use.</summary>
     * <param name="unitType">Unit type name of PassiveUnit file to load</param>
     */
    public PassiveUnitData LoadPassiveUnit(string unitType)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + unitType + "Unit.bin";
        FileStream stream = null;

        UnityEngine.Debug.Log("SaveSystem::LoadPassiveUnit() filePath: " + filePath);

        try
        {
            if (File.Exists(filePath))
            {
                // File exists, open it and read PassiveUnit data
                UnityEngine.Debug.Log("SaveSystem::LoadPassiveUnit() file exists");
                stream = new FileStream(filePath, FileMode.Open);
                PassiveUnitData passiveUnitData = binaryFormatter.Deserialize(stream) as PassiveUnitData;
                return passiveUnitData;
            }
            else
            {
                Debug.LogError("Save file not found in " + filePath);
                return null;
            }
        }
        finally
        {
            if (stream != null)
            {
                // Regardless of exceptions, close stream if it exists.
                stream.Close();
            }
        }
    }

}
