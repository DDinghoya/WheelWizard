using System.IO;
using System.Threading.Tasks;
using WheelWizard.Helpers;
using WheelWizard.Views.Popups.Generic;

namespace WheelWizard.Services.Launcher;

public static class MiiChannelLaunchHelper
{
    private static string MiiChannelPath => Path.Combine(PathManager.WheelWizardAppdataPath, "MiiChannel.wad");
    
    public static async Task LaunchMiiChannel()
    {
        var miiChannelExists = File.Exists(MiiChannelPath);;
        
        if (!miiChannelExists)
        {
            // TODO: If we do enable this again, we should also add translations support for the text here
            var downloadQuestion = new YesNoWindow()
                                .SetMainText("Install MiiChannel?")
                                .SetExtraText("Do you want to install the MiiChannel to launch it?");
            
            if (await downloadQuestion.AwaitAnswer())
            {
                miiChannelExists = true;
                await DownloadHelper.DownloadToLocationAsync(Endpoints.MiiChannelWAD, MiiChannelPath, "Downloading MiiChannel");
                //we wait to make sure the file is written to disk
                await Task.Delay(200);
            }
        }

        if(miiChannelExists)
            Launcher.LaunchDolphin($"-b \"{MiiChannelPath}\"");
    }
}
