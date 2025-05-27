using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public partial class Tester : MonoBehaviour
{
    Thread task;

    [SerializeField] float timeShow;

    bool set;

    private void Start()
    {
        task = new(MultiTest);

        set = true;

        //task.Start();

        //StartCoroutine(Delay());
    }

    private void OnApplicationQuit()
    {
        set = false;
    }

    void MultiTest()
    {
        while (set)
        {
            //timeShow = GameManager.Instance.deltaTime;
            Thread.Sleep(100);
        }
    }

    IEnumerator Delay()
    {
        float preDelta;

        while (true)
        {
            preDelta = Time.deltaTime;

            yield return new WaitForEndOfFrame();

            Debug.Log($"{preDelta} | {Time.deltaTime}");
        }
    }
}
