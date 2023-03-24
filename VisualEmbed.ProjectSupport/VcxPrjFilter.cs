using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace VisualEmbed.ProjectSupport;

[XmlRoot(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
[XmlType("Project")]
public class VcxPrjFilter : BaseProject
{
	[XmlType("Filter")]
	public class VcxFilter
	{
		[XmlAttribute]
		public string Include;

		[XmlElement]
		public string UniqueIdentifier;

		[XmlElement]
		public string Extensions;
	}

	public abstract class VcxFltFileItem
	{
		[XmlAttribute]
		public string Include;

		[XmlElement]
		public string Filter;
	}

	[XmlType("Text")]
	public class VcxFltText : VcxFltFileItem
	{
	}

	[XmlType("ClCompile")]
	public class VcxFltCItem : VcxFltFileItem
	{
	}

	[XmlType("ClInclude")]
	public class VcxFltInc : VcxFltFileItem
	{
	}

	[XmlType("AsmItem")]
	public class VcxFltAsmItem : VcxFltFileItem
	{
	}

	[XmlType("Object")]
	public class VcxFltObj : VcxFltFileItem
	{
	}

	[XmlType("Library")]
	public class VcxFltLib : VcxFltFileItem
	{
	}

	[XmlType("None")]
	public class VcxFltNone : VcxFltFileItem
	{
	}

	[XmlType("CustomItem")]
	public class VcxFltCustomItem : VcxFltFileItem
	{
	}

	[XmlAttribute]
	public string ToolsVersion;

	[XmlArrayItem(Type = typeof(VcxFilter))]
	[XmlArrayItem(Type = typeof(VcxFltText))]
	[XmlArrayItem(Type = typeof(VcxFltCItem))]
	[XmlArrayItem(Type = typeof(VcxFltInc))]
	[XmlArrayItem(Type = typeof(VcxFltAsmItem))]
	[XmlArrayItem(Type = typeof(VcxFltObj))]
	[XmlArrayItem(Type = typeof(VcxFltLib))]
	[XmlArrayItem(Type = typeof(VcxFltCustomItem))]
	[XmlArrayItem(Type = typeof(VcxFltNone))]
	public List<object> ItemGroup = new List<object>();

	private string CurFilter;

	public static VcxPrjFilter Create(Stream theStream)
	{
		return (VcxPrjFilter)BaseProject.Create(typeof(VcxPrjFilter), theStream);
	}

	public static VcxPrjFilter Create(string theXml)
	{
		return (VcxPrjFilter)BaseProject.Create(typeof(VcxPrjFilter), theXml);
	}

	public static VcxPrjFilter CreateFromFile(string theFile)
	{
		return (VcxPrjFilter)BaseProject.CreateFromFile(typeof(VcxPrjFilter), theFile);
	}

	public bool AddFilter(string Name, string theFileExt)
	{
		try
		{
			if (Name != null && Name != string.Empty)
			{
				bool flag = false;
				foreach (object item in ItemGroup)
				{
					if (item is VcxFilter && (item as VcxFilter).Include == Name)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					VcxFilter vcxFilter = new VcxFilter();
					vcxFilter.Include = Name;
					vcxFilter.UniqueIdentifier = "{" + Guid.NewGuid().ToString() + "}";
					vcxFilter.Extensions = theFileExt;
					ItemGroup.Add(vcxFilter);
				}
			}
			CurFilter = Name;
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool AddFileItem(string File, Type type)
	{
		try
		{
			bool result = false;
			if (type.BaseType == typeof(VcxFltFileItem))
			{
				bool flag = false;
				foreach (object item in ItemGroup)
				{
					if (item is VcxFltFileItem && (item as VcxFltFileItem).Include.ToLower() == File.ToLower())
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					object obj = type.Assembly.CreateInstance(type.FullName);
					(obj as VcxFltFileItem).Include = File.Trim();
					(obj as VcxFltFileItem).Filter = ((CurFilter == string.Empty) ? null : CurFilter);
					ItemGroup.Add(obj);
				}
				return true;
			}
			return result;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool AddFileItem(string theFilter, string theFile, Type type)
	{
		bool flag = AddFilter(theFilter, null);
		if (flag)
		{
			flag = AddFileItem(theFile, type);
		}
		return flag;
	}
}
