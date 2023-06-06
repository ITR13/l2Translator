#if MelonLoader
using Il2CppI2.Loc;
#elif BepInEx
using I2.Loc;
#endif


using System.Text;
using UnityEngine;

namespace ITRsI2Translator;

public static class Tools
{
    private static Action<string>? _logMsg;

    private static string GetLocalizationFolder()
    {
        return Path.Combine(Application.persistentDataPath, "ITRsLocalization");
    }

    public static void Initialize(Action<string>? logMsg)
    {
        _logMsg = logMsg;
        ExportAndImportAll();
    }

    private static void ExportAndImportAll()
    {
        var rootPath = GetLocalizationFolder();
        _logMsg?.Invoke($"Starting export/import to {rootPath}");

        var sourceNum = 0;
        var sourceNumWithNull = 0;
        var exported = false;
        foreach (var source in LocalizationManager.Sources)
        {
            _logMsg?.Invoke($"Exporting source {sourceNumWithNull++} (isNull: {source == null})");
            if (source == null) continue;
            var sourcePath = sourceNum++ == 0 ? rootPath : Path.Combine(rootPath, $"Alt_{sourceNum}");
            Directory.CreateDirectory(sourcePath);

            exported |= ExportAndImportSource(exported, source, sourcePath);
        }

        _logMsg?.Invoke($"Found and processed {sourceNum} sources");
        if (exported)
        {
            Application.OpenURL($"file:///{rootPath}");
        }
    }

    private static bool ExportAndImportSource(bool exported, LanguageSourceData source, string sourcePath)
    {
        var categories = source.GetCategories();
        foreach (var category in categories)
        {
            var categoryPath = Path.Combine(sourcePath, $"Category_{category}.csv");
            if (File.Exists(categoryPath))
            {
                _logMsg?.Invoke($"Loading category '{category}'");
                LoadCsv(source, category, categoryPath);
            }
            else
            {
                _logMsg?.Invoke($"Exporting category '{category}'");
                ExportCsv(source, category, categoryPath);
                exported = true;
            }
        }

        return exported;
    }

    private static void ExportCsv(LanguageSourceData source, string category, string categoryPath)
    {
        try
        {
            var csv = source.Export_CSV(category, specializationsAsRows: false);
            File.WriteAllText(categoryPath, csv, Encoding.UTF8);
        }
        catch (Exception e)
        {
            _logMsg?.Invoke($"Failed to save: {e}");
        }
    }

    private static void LoadCsv(LanguageSourceData source, string category, string categoryPath)
    {
        try
        {
            var csv = File.ReadAllText(categoryPath, Encoding.UTF8);
            source.Import_CSV(category, csv, eSpreadsheetUpdateMode.Merge);
        }
        catch (Exception e)
        {
            _logMsg?.Invoke($"Failed to load: {e}");
        }
    }
}