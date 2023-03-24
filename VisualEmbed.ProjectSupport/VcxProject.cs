using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace VisualEmbed.ProjectSupport;

[XmlRoot(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
[XmlType("Project")]
public class VcxProject : BaseProject
{
	public class VcxProps : List<XmlElement>
	{
		[XmlIgnore]
		private XmlElement _selected;

		[XmlIgnore]
		public XmlElement CurItem => _selected;

		[XmlIgnore]
		public string Type
		{
			get
			{
				if (_selected != null)
				{
					return _selected.Name;
				}
				return null;
			}
		}

		[XmlIgnore]
		public string Value
		{
			get
			{
				if (_selected != null)
				{
					return _selected.InnerText;
				}
				return null;
			}
			set
			{
				if (_selected != null)
				{
					_selected.InnerText = value;
				}
			}
		}

		public string GetAttribute(string theName)
		{
			if (_selected != null)
			{
				return _selected.GetAttribute(theName);
			}
			return null;
		}

		private void SetAttribute(XmlElement theElm, string theName, object theValue, bool IsAllowEmpty = false)
		{
			if (theElm == null || theName == null || !(theName != string.Empty))
			{
				return;
			}
			string text;
			if (theValue == null)
			{
				text = null;
			}
			else
			{
				text = theValue.ToString();
				if (theValue.GetType() == typeof(bool))
				{
					text = text.ToLower();
				}
			}
			if (text != null)
			{
				if (IsAllowEmpty || text != string.Empty)
				{
					theElm.SetAttribute(theName, text);
				}
				else
				{
					theElm.RemoveAttribute(theName);
				}
			}
			else
			{
				theElm.RemoveAttribute(theName);
			}
		}

		public void SetAttribute(string theName, object theValue, bool IsAllowEmpty = false)
		{
			SetAttribute(_selected, theName, theValue, IsAllowEmpty);
		}

		public void RemoveAttribute(string theName)
		{
			if (_selected != null)
			{
				_selected.RemoveAttribute(theName);
			}
		}

		public bool AddItem(string theType, object theValue, bool IsValuePrefer, params object[] theAttrs)
		{
			try
			{
				string text;
				if (theValue == null)
				{
					text = null;
				}
				else
				{
					text = theValue.ToString();
					if (theValue.GetType() == typeof(bool))
					{
						text = text.ToLower();
					}
				}
				bool flag;
				if (IsValuePrefer)
				{
					flag = text != null && text.Trim() != string.Empty;
				}
				else if (theAttrs != null && (theAttrs.Length & 1) == 0)
				{
					flag = false;
					for (int i = 0; i < theAttrs.Length; i += 2)
					{
						if (theAttrs[i] != null && theAttrs[i].ToString() != string.Empty && theAttrs[i + 1] != null && theAttrs[i + 1].ToString() != string.Empty)
						{
							flag = true;
						}
					}
				}
				else
				{
					flag = false;
				}
				if (flag)
				{
					XmlElement xmlElement = _doc.CreateElement(string.Empty, theType, _xmlns);
					xmlElement.InnerText = ((text == null) ? text : text.Trim());
					if (theAttrs != null && (theAttrs.Length & 1) == 0)
					{
						for (int j = 0; j < theAttrs.Length; j += 2)
						{
							SetAttribute(xmlElement, theAttrs[j].ToString(), theAttrs[j + 1]);
						}
					}
					Add(xmlElement);
					_selected = xmlElement;
					return flag;
				}
				return flag;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool AddItem(string theType, object theValue, params object[] theAttrs)
		{
			return AddItem(theType, theValue, IsValuePrefer: true, theAttrs);
		}

		public void RemoveItem(XmlElement Item)
		{
			if (_selected == Item)
			{
				Walkthrough(IsContinue: true);
			}
			Remove(Item);
		}

		public void RemoveItem()
		{
			if (_selected != null)
			{
				RemoveItem(_selected);
			}
		}

		public bool SelectItem(XmlElement Item)
		{
			bool num = IndexOf(Item) >= 0;
			if (num)
			{
				_selected = Item;
				return num;
			}
			_selected = null;
			return num;
		}

		public bool Walkthrough(bool IsContinue)
		{
			int num;
			if (!IsContinue)
			{
				_selected = null;
				num = 0;
			}
			else
			{
				num = IndexOf(_selected);
				num = ((num >= 0) ? (num + 1) : 0);
			}
			_selected = ((base.Count > num) ? base[num] : null);
			return _selected != null;
		}
	}

	[XmlType("PropertyGroup")]
	public class VcxPropertyGroup
	{
		[XmlAttribute]
		public string Label;

		[XmlAttribute]
		public string Condition;

		[XmlAnyElement]
		public VcxProps Props = new VcxProps();
	}

	[XmlType("ProjectConfiguration")]
	public class VcxProjectConfiguration
	{
		[XmlAttribute]
		public string Include;

		[XmlElement]
		public string Configuration;

		[XmlElement]
		public string Platform;
	}

	public class VcxFileItem
	{
		[XmlAttribute("Include")]
		public string Include;

		[XmlAnyElement]
		public VcxProps Metadatas = new VcxProps();
	}

	[XmlType("Text")]
	public class VcxText : VcxFileItem
	{
	}

	[XmlType("ClCompile")]
	public class VcxCItem : VcxFileItem
	{
	}

	[XmlType("ClInclude")]
	public class VcxInc : VcxFileItem
	{
	}

	[XmlType("AsmItem")]
	public class VcxAsmItem : VcxFileItem
	{
	}

	[XmlType("Object")]
	public class VcxObj : VcxFileItem
	{
	}

	[XmlType("Library")]
	public class VcxLib : VcxFileItem
	{
	}

	[XmlType("None")]
	public class VcxNone : VcxFileItem
	{
	}

	[XmlType("CustomItem")]
	public class VcxCustomItem : VcxFileItem
	{
	}

	[XmlIgnore]
	public static readonly string AttrCond = "Condition";

	[XmlIgnore]
	private static XmlDocument _doc = new XmlDocument();

	[XmlIgnore]
	private static readonly string _xmlns = "http://schemas.microsoft.com/developer/msbuild/2003";

	[XmlAttribute]
	public string DefaultTargets;

	[XmlAttribute]
	public string ToolsVersion;

	[XmlElement(Type = typeof(VcxPropertyGroup), ElementName = "PropertyGroup")]
	public List<VcxPropertyGroup> PropertyGroups = new List<VcxPropertyGroup>();

	[XmlArrayItem(Type = typeof(VcxText))]
	[XmlArrayItem(Type = typeof(VcxCItem))]
	[XmlArrayItem(Type = typeof(VcxInc))]
	[XmlArrayItem(Type = typeof(VcxAsmItem))]
	[XmlArrayItem(Type = typeof(VcxObj))]
	[XmlArrayItem(Type = typeof(VcxLib))]
	[XmlArrayItem(Type = typeof(VcxNone))]
	[XmlArrayItem(Type = typeof(VcxCustomItem))]
	[XmlArrayItem(Type = typeof(VcxProjectConfiguration))]
	public List<object> ItemGroup = new List<object>();

	[XmlIgnore]
	public List<VcxProjectConfiguration> PrjCfgs = new List<VcxProjectConfiguration>();

	public static VcxProject Create(Stream theStream)
	{
		return (VcxProject)BaseProject.Create(typeof(VcxProject), theStream);
	}

	public static VcxProject Create(string theXml)
	{
		return (VcxProject)BaseProject.Create(typeof(VcxProject), theXml);
	}

	public static VcxProject CreateFromFile(string theFile)
	{
		return (VcxProject)BaseProject.CreateFromFile(typeof(VcxProject), theFile);
	}

	public object AddFileItem(string File, Type type)
	{
		try
		{
			object result = null;
			if (type.BaseType == typeof(VcxFileItem))
			{
				result = type.Assembly.CreateInstance(type.FullName);
				(result as VcxFileItem).Include = File;
				ItemGroup.Add(result);
				return result;
			}
			return result;
		}
		catch (Exception)
		{
			return null;
		}
	}

	private new bool Save(Stream theStream)
	{
		return true;
	}

	private new bool Save(out string theXml)
	{
		theXml = string.Empty;
		return true;
	}

	private new bool Save(string theFile)
	{
		return true;
	}

	public bool Save(string theFile, string theTemplate)
	{
		try
		{
			FileStream fileStream = new FileStream(theTemplate, FileMode.Open, FileAccess.Read);
			StreamReader streamReader = new StreamReader(fileStream);
			string text = streamReader.ReadToEnd();
			streamReader.Dispose();
			fileStream.Dispose();
			string text2 = "  <ItemGroup Label=\"ProjectConfigurations\">\r\n";
			foreach (VcxProjectConfiguration prjCfg in PrjCfgs)
			{
				text2 += $"    <ProjectConfiguration Include=\"{prjCfg.Include}\">\r\n      <Configuration>{prjCfg.Configuration}</Configuration>\r\n      <Platform>{prjCfg.Platform}</Platform>\r\n    </ProjectConfiguration>\r\n";
			}
			text2 += "  </ItemGroup>\r\n";
			text = text.Replace("$$$ProjectConfigurations$$$", text2);
			bool flag = base.Save(out text2);
			if (flag)
			{
				int startIndex = text2.IndexOf("<Project");
				startIndex = text2.IndexOf(">", startIndex);
				text2 = text2.Substring(startIndex + 1);
				text2 = text2.Replace("</Project>", string.Empty);
				text = text.Replace("$$$PropertyAndFiles$$$", text2);
				FileStream fileStream2 = new FileStream(theFile, FileMode.Create, FileAccess.ReadWrite);
				StreamWriter streamWriter = new StreamWriter(fileStream2);
				streamWriter.Write(text);
				streamWriter.Dispose();
				fileStream2.Dispose();
				return flag;
			}
			return flag;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
