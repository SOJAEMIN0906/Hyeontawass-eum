public class StaticData
{
    static StaticData instance;

    public static StaticData Instance
    {
        get
        {
            return instance ??= new();
        }
    }

    public DataPack dataPack;
    public EGameHardness gameHardness;
}
