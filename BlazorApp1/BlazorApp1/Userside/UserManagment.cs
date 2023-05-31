using System.Management;

namespace BlazorApp1.Userside;

public class UserManagment
{
    public static string GetMotherboardId()
    {
        string result = string.Empty;
        try
        {
            var mos = new ManagementObjectSearcher("Select * FROM Win32_BaseBoard");
            foreach (var mo in mos.Get())
            {
                result = mo["SerialNumber"].ToString();
                break;
            }
        }
        catch
        {
            result = string.Empty;
        }
        return result;
    }
    public static string GetPCName()
    {
        string result = string.Empty;
        try
        {
            var mos = new ManagementObjectSearcher("Select * FROM Win32_ComputerSystem");
            foreach (var mo in mos.Get())
            {
                result = mo["Name"].ToString();
                break;
            }
        }
        catch
        {
            result = string.Empty;
        }
        return result;
    }
}
