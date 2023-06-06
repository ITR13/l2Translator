#if BepInEx
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using UnityEngine;

namespace ITRsI2Translator;

[BepInPlugin("itr13.com.github.itrsi2translator", "ITR's I2 Translator", "1.0.0")]
public class BepInEx : BasePlugin
{
    public static Action<string>? Msg { get; private set; }
    public override void Load()
    {
        Msg = Log.LogInfo;
        Msg?.Invoke("Mod Loaded");
        AddComponent<RunTranslate>();
    }
}

public class RunTranslate : MonoBehaviour
{
    private void Start()
    {
        Tools.Initialize(BepInEx.Msg);
    }
}

#endif