using System.Runtime.InteropServices;

namespace FSCSharp;

//Console Lib
public static class CLib
{
    #region Methods
    public static void Focus()
    {
        _previousWindow = GetForegroundWindow();

        // Call your existing FocusConsole() method
        FocusConsole();
    }
    public static bool Unfocus()
    {
        return _previousWindow != IntPtr.Zero &&
               SetForegroundWindow(_previousWindow);
    }

    private static void FocusConsole()
    {
        IntPtr hwnd = GetConsoleWindow();

        if (hwnd == IntPtr.Zero)
            return;

        ShowWindow(hwnd, 9); // SW_RESTORE
        SetForegroundWindow(hwnd);
    }
    #endregion

    #region Win32 exports
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    #endregion

    #region Properties
    private static IntPtr _previousWindow;
    #endregion
}
