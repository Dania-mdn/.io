using System;
using UnityEngine;

public class EventManage : MonoBehaviour
{
    public static event Action<int> MobilUpdate;
    public static event Action Shoot;
    public static event Action adBool;
    public static event Action adScore;
    public static event Action Die;
    public static event Action AdNot;
    public static event Action UpLvl;
    public static event Action<int> TakeLvl;
    public static event Action<Vector3> DestroyItem;
    public static void DoMobilUpdate(int i)
    {
        MobilUpdate?.Invoke(i);
    }
    public static void DoShoot()
    {
        Shoot?.Invoke();
    }
    public static void DoAdNot()
    {
        AdNot?.Invoke();
    }
    public static void DoadBool()
    {
        adBool?.Invoke();
    }
    public static void DoadScore()
    {
        adScore?.Invoke();
    }
    public static void DoDie()
    {
        Die?.Invoke();
    }
    public static void DoUpLvl()
    {
        UpLvl?.Invoke();
    }
    public static void DoTakeLvl(int i)
    {
        TakeLvl?.Invoke(i);
    }
    public static void DuDestroyItem(Vector3 position)
    {
        DestroyItem?.Invoke(position);
    }
}
