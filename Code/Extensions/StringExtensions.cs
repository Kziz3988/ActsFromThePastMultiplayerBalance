using System.IO;
using ActsFromThePastMultiplayerBalance;
using Godot;

namespace WarframeMod.Code.Extensions;

//Mostly utilities to get asset paths.
public static class StringExtensions
{
    public static string ImagePath(this string path)
    {
        return Path.Join(ModEntry.ResPath, "images", path);
    }

    public static string PowerImagePath(this string path)
    {
        path = Path.Join(ModEntry.ResPath, "images", "powers", path);
        if (ResourceLoader.Exists(path)) return path;
        
        ModEntry.Logger.Info("Could not find power image path: " + path);
        return Path.Join(ModEntry.ResPath, "images", "powers", "power.png");
    }

    public static string BigPowerImagePath(this string path)
    {
        path = Path.Join(ModEntry.ResPath, "images", "powers", "big", path);
        if (ResourceLoader.Exists(path)) return path;
        
        ModEntry.Logger.Info("Could not find big power image path: " + path);
        return Path.Join(ModEntry.ResPath, "images", "powers", "big", "power.png");
    }
}