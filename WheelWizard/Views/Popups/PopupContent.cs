using Avalonia;
using Avalonia.Controls;
using System.Threading.Tasks;

namespace WheelWizard.Views.Popups;

public abstract class PopupContent : UserControl
{
    public PopupWindow Window { get; private set; }
    
    protected PopupContent(bool allowClose, bool allowLayoutInteraction, bool isTopMost, string title = "", Vector? size = null)
    {
        Window = new PopupWindow(allowClose, allowLayoutInteraction, isTopMost, title, size)
        {
            PopupContent = { Content = this },
            BeforeClose = BeforeClose,
            BeforeOpen = BeforeOpen
        };
        
    }

    protected virtual void BeforeClose() { } // Meant to be overwritten if needed
    protected virtual void BeforeOpen() { }  // Meant to be overwritten if needed
    
    public void Show() => Window.Show();
    public Task ShowDialog() => Window.ShowDialog(ViewUtils.GetLayout());
    public Task<T> ShowDialog<T>() => Window.ShowDialog<T>(ViewUtils.GetLayout());
    public void Close() => Window.Close();
    public void Minimize() => Window.WindowState = WindowState.Minimized;
    public void Focus() => Window.Focus();
}
