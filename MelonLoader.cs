#if MelonLoader
using MelonLoader;

[assembly: MelonInfo(typeof(ITRsI2Translator.Melon.MelonLoader), "ITR's I2 Translator", "1.0.0", "ITR")]
[assembly: MelonGame(null, null)]
namespace ITRsI2Translator.Melon;

public class MelonLoader : MelonMod
{
    public override void OnLateInitializeMelon()
    {
        Tools.Initialize(LoggerInstance.Msg);
    }
}
#endif