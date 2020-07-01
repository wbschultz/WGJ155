using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/**
 * <summary>Class <c>SaveSystem</c> saves and loads game data.</summary>
 */
public static class SaveSystem
{
    /**
     * <summary>Serialize PassiveUnit data and write to a file.</summary>
     * <param name="passiveUnit">Energy generation upgrade data</param>
     */
    public static void SavePassiveUnit (string unitType, int quantity, double powerRatePerQuantity, double priceToPurchase, double priceGrowthRate)
    {
        /**
         * Create binary formatter and setup input stream to write data to. The
         * FileStream uses a persistent path that doesn't change regardless
         * of file system.
         */
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + unitType + "Unit.bin";
        FileStream stream = null;

        UnityEngine.Debug.Log("SaveSystem::SavePassiveUnit() filePath: " + filePath);
        try
        {
            // Convert PassiveUnit to serializable class type and write to file
            stream = new FileStream(filePath, FileMode.Create);
            PassiveUnitData passiveUnitData = new PassiveUnitData(unitType, quantity, powerRatePerQuantity, priceToPurchase, priceGrowthRate);
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
    public static PassiveUnitData LoadPassiveUnit (string unitType)
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
