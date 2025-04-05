using System.Globalization;
using Avalonia.Controls;
using WheelWizard.Services.Settings;

namespace WheelWizard.Views;

public static class NavigationManager
{
    public static void NavigateTo(Type pageType, params object?[] args)
    {
        // TODO: Fix the language bug. for some reason when changing the language, it changes itself back to the language before
        //  SO as a quick and dirty fix in the navigate to page we just set the language pack when its out of sync, but this solution
        //  still makes it so that the first page you enter after changing the language setting will always be the old language instead of the new one
        //  when working on the translations again, this should be fixed. and in a solid way instead of this
        var itCurrentlyIs = CultureInfo.CurrentCulture.ToString();
        var itsSupposeToBe = (string)SettingsManager.WW_LANGUAGE.Get();
        if (itCurrentlyIs != itsSupposeToBe)
        {
            SettingsManager.WW_LANGUAGE.Set(itCurrentlyIs);
            SettingsManager.WW_LANGUAGE.Set(itsSupposeToBe);
        }

        if (Activator.CreateInstance(pageType, args) is not UserControl instance)
            throw new InvalidOperationException($"Failed to create an instance of {pageType.FullName}");

        Layout.Instance.NavigateToPage(instance);
    }

    public static void NavigateTo<T>(params object?[] args) where T : UserControlBase => NavigateTo(typeof(T), args);
}
