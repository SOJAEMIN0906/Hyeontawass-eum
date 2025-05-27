using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject continueWindow;

    public GameObject hardnessWindow;

    DataPack dataPack;

    void Start()
    {
        float targetAspect = 9f / 16f;

        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    public void ContinueGame()
    {
        StaticData.Instance.dataPack = dataPack;

        SceneManager.LoadScene("02.InGameAndroid");
    }

    public void InitGame()
    {
        FileLoader.DeleteData();

        continueWindow.SetActive(false);
        hardnessWindow.SetActive(true);
    }

    public void StartPlay()
    {
        dataPack = FileLoader.GetDataPack();
        if (dataPack != null)
        {
            continueWindow.SetActive(true);
        }
        else
        {
            continueWindow.SetActive(false);
            hardnessWindow.SetActive(true);
        }
    }

    public void SetHardness(int hardness)
    {
        StaticData.Instance.gameHardness = (EGameHardness)hardness;

        SceneManager.LoadScene("02.InGameAndroid");
    }
}
