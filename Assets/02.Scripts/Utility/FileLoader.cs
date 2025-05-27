[System.Serializable]
public class DataPack
{
    public SkillData skillData;
    public StatusData statusData;
    public GameData gameData;
}

[System.Serializable]
public class SkillData
{
    public SkillPack[] skillPacks;
}

[System.Serializable]
public class SkillPack
{
    public SkillPack(EPlayerSkill EPlayerSkill, int level)
    {
        this.EPlayerSkill = EPlayerSkill;
        this.level = level;
    }

    public EPlayerSkill EPlayerSkill;
    public int level;
}

[System.Serializable]
public class StatusData
{
    public Status status;
    public int level;
    public int CriticalRate;
    public int CriticalDamage;
    public float AttackScaleIncrease;
    public float AttackSpeedIncrease;
    public float PowerIncrease;
    public float DefenseIncrease;
    public float recoverIncrease;
    public float expBonus;
}

[System.Serializable]
public class GameData
{
    public float GameTime;
    public int HuntMass;
}

public class FileLoader
{
    public static void SaveFile(StatusData statusData)
    {
        string path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, "StatusData");

        string jsonData = UnityEngine.JsonUtility.ToJson(statusData);

        System.IO.File.WriteAllText(path, jsonData);
    }

    public static void SaveFile(SkillData skillData)
    {
        string path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, "SkillData");

        string jsonData = UnityEngine.JsonUtility.ToJson(skillData);

        System.IO.File.WriteAllText(path, jsonData);
    }

    public static void SaveFile(GameData gameData)
    {
        string path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, "GameData");

        string jsonData = UnityEngine.JsonUtility.ToJson(gameData);

        System.IO.File.WriteAllText(path, jsonData);
    }

    public static DataPack GetDataPack()
    {
        DataPack dataPack = new();

        string path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, "StatusData");

        if (System.IO.File.Exists(path))
        {
            string data = System.IO.File.ReadAllText(path);

            dataPack.statusData = UnityEngine.JsonUtility.FromJson<StatusData>(data);
        }
        else
        {
            return null;
        }

        path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, "SkillData");

        if (System.IO.File.Exists(path))
        {
            string data = System.IO.File.ReadAllText(path);

            dataPack.skillData = UnityEngine.JsonUtility.FromJson<SkillData>(data);
        }
        else
        {
            return null;
        }

        path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, "GameData");

        if (System.IO.File.Exists(path))
        {
            string data = System.IO.File.ReadAllText(path);

            dataPack.gameData = UnityEngine.JsonUtility.FromJson<GameData>(data);
        }
        else
        {
            return null;
        }

        return dataPack;
    }

    public static void DeleteData()
    {
        string path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, "StatusData");

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }

        path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, "SkillData");

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }

        path = System.IO.Path.Combine(UnityEngine.Application.persistentDataPath, "GameData");

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
}
