using System.IO;
using Microsoft.Win32;

namespace VisualEmbed.ProjectSupport;

public class IDEPath
{
	public static string MdkPath
	{
		get
		{
			string result = string.Empty;
			RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
			if (registryKey != null)
			{
				RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Keil\\Products\\MDK");
				if (registryKey2 == null)
				{
					registryKey2 = registryKey.OpenSubKey("SOFTWARE\\WOW6432Node\\Keil\\Products\\MDK");
				}
				if (registryKey2 != null)
				{
					result = (string)registryKey2.GetValue("Path", string.Empty);
					registryKey2.Close();
				}
				registryKey.Close();
			}
			return result;
		}
	}

	private static int CompareStr(string str1, string str2)
	{
		char[] array = str1.ToLower().ToCharArray();
		char[] array2 = str2.ToLower().ToCharArray();
		int num = array.Length;
		bool flag = num == array2.Length;
		if (num > array2.Length)
		{
			num = array2.Length;
		}
		int i;
		for (i = 0; i < num && array[i] == array2[i]; i++)
		{
		}
		if (i == num && flag)
		{
			i = int.MaxValue;
		}
		return i;
	}

	public static string GetValidDir(string BasePath, string SubPath)
	{
		string text = ((BasePath != null && !(BasePath == string.Empty)) ? (BasePath + "\\" + SubPath) : SubPath);
		if (!Directory.Exists(text))
		{
			string[] directories = Directory.GetDirectories(BasePath);
			int num = -1;
			string[] array = directories;
			foreach (string text2 in array)
			{
				int num2 = CompareStr(SubPath, Path.GetFileName(text2));
				if (num2 > num)
				{
					num = num2;
					text = text2;
				}
			}
		}
		return text;
	}

	public static string GetValidDir(string BasePath, params object[] Args)
	{
		string text = BasePath;
		foreach (object obj in Args)
		{
			if (obj != null)
			{
				text = GetValidDir(text, obj.ToString());
			}
		}
		return text;
	}
}
