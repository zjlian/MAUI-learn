using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Notes.ViewModels;

internal class AboutVM
{
    public string Title => AppInfo.Name;
    public string Version => AppInfo.VersionString;
    public string MoreInfoUrl => "https://baidu.com";
    public string Message => "哈哈 傻逼";
    // 提供 command 给 View 调用
    public ICommand ShowMoreInfoCommand { get; }

    public AboutVM()
    {
        ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
    }

    async Task ShowMoreInfo()
    {
        await Launcher.Default.OpenAsync(MoreInfoUrl);
    }

}