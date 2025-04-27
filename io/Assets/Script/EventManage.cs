using System;
using UnityEngine;

public class EventManage : MonoBehaviour
{
    public static event Action Shoot;
    public static event Action adBool;
    public static event Action adScore;
    public static event Action Die;
    public static event Action UpLvl;
    public static event Action TakeLvl;
    public static void DoShoot()
    {
        Shoot?.Invoke();
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
    public static void DoTakeLvl()
    {
        TakeLvl?.Invoke();
    }
}
