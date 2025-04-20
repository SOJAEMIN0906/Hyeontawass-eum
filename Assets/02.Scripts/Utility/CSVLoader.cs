using System.Collections.Generic;
using UnityEngine;

public class CSVLoader
{
    private static CSVLoader instance;
    public static CSVLoader Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }

    Dictionary<string, Dictionary<string, string>> skillInfoDic;

    void LoadSkillInfo()
    {
        skillInfoDic = new();

        TextAsset csvData = Resources.Load<TextAsset>("CSV/SkillInfo");

        string[] infos = csvData.text.Split('\n');

        string[] headers = infos[0].Split(',');

        for (int i = 1; i < headers.Length; i++)
        {
            string[] data = infos[i].Split(',');

            Dictionary<string, string> keyValuePairs = new();

            for (int row = 1; row < data.Length; row++)
            {
                keyValuePairs.Add(headers[row], data[row]);
            }

            skillInfoDic.Add(data[0], keyValuePairs);
        }
    }

    public string GetSkillInfo(string name, string arg)
    {
        if (skillInfoDic == null)
        {
            LoadSkillInfo();
        }

        return skillInfoDic[name][arg];
    }
}
