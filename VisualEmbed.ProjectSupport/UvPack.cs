using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace VisualEmbed.ProjectSupport;

[XmlType("package")]
public class UvPack : BaseProject
{
	[XmlType("release")]
	public struct release
	{
		[XmlAttribute]
		public string version;

		[XmlAttribute]
		public string date;

		[XmlText]
		public string description;
	}

	[XmlType("keyword")]
	public struct keyword
	{
		[XmlText]
		public string word;
	}

	[XmlType("processor")]
	public struct ProcessorType
	{
		[XmlAttribute]
		public string Pname;

		[XmlAttribute]
		public string Dcore;

		[XmlAttribute]
		public string Dfpu;

		[XmlAttribute]
		public string Dmpu;

		[XmlAttribute]
		public string Dendian;

		[XmlAttribute]
		public uint Dclock;

		[XmlAttribute]
		public string DcoreVersion;
	}

	[XmlType("book")]
	public struct book
	{
		[XmlAttribute]
		public string Pname;

		[XmlAttribute]
		public string name;

		[XmlAttribute]
		public string title;
	}

	[XmlType("description")]
	public struct DescriptionType
	{
		[XmlAttribute]
		public string Pname;

		[XmlText]
		public string value;
	}

	[XmlType("compile")]
	public class CompileType
	{
		[XmlAttribute]
		public string Pname;

		[XmlAttribute]
		public string header;

		[XmlAttribute]
		public string define;
	}

	public class DataPatchType
	{
		[XmlAttribute]
		public string type;

		[XmlAttribute]
		public string address;

		[XmlAttribute]
		public uint __dp;

		[XmlAttribute]
		public uint __ap;

		[XmlAttribute]
		public string value;

		[XmlAttribute]
		public string mask;

		[XmlAttribute]
		public string info;

		[XmlAnyAttribute]
		public List<XmlNode> lax = new List<XmlNode>();
	}

	public class JtagType
	{
		[XmlAttribute]
		public string tapindex;

		[XmlAttribute]
		public string idcode;

		[XmlAttribute]
		public string targetsel;

		[XmlAttribute]
		public uint irlen;

		[XmlAnyAttribute]
		public List<XmlNode> lax = new List<XmlNode>();
	}

	public class SwdType
	{
		[XmlAttribute]
		public string idcode;

		[XmlAttribute]
		public string targetsel;

		[XmlAnyAttribute]
		public List<XmlNode> lax = new List<XmlNode>();
	}

	[XmlType("debugport")]
	public class DebugPortType
	{
		[XmlElement]
		public JtagType jtag;

		[XmlElement]
		public SwdType swd;

		[XmlAttribute]
		public uint __dp;

		[XmlAnyAttribute]
		public List<XmlNode> lax = new List<XmlNode>();
	}

	[XmlType("environment")]
	public class EnvironmentType
	{
		[XmlAttribute]
		public string name;

		[XmlAttribute]
		public string Pname;

		[XmlAnyAttribute]
		public List<XmlNode> lax = new List<XmlNode>();
	}

	public class SerialWireType
	{
		[XmlAnyAttribute]
		public List<XmlNode> lax = new List<XmlNode>();

		[XmlText]
		public string _value;
	}

	public class TracePortType
	{
		[XmlAttribute]
		public string width;

		[XmlAnyAttribute]
		public List<XmlNode> skip = new List<XmlNode>();

		[XmlText]
		public string _value;
	}

	public class TraceBufferType
	{
		[XmlAttribute]
		public string start;

		[XmlAttribute]
		public string size;

		[XmlAnyAttribute]
		public List<XmlNode> skip = new List<XmlNode>();

		[XmlText]
		public string _value;
	}

	[XmlType("trace")]
	public struct TraceType
	{
		[XmlElement]
		public SerialWireType serialwire;

		[XmlElement]
		public TracePortType traceport;

		[XmlElement]
		public TraceBufferType tracebuffer;

		[XmlAttribute]
		public string Pname;

		[XmlAnyAttribute]
		public List<XmlNode> lax;
	}

	[XmlType("debugvars")]
	public class DebugVarsType
	{
		[XmlAttribute]
		public string configfile;

		[XmlAttribute]
		public string version;

		[XmlAttribute]
		public string Pname;

		[XmlAnyAttribute]
		public List<XmlNode> lax = new List<XmlNode>();

		[XmlText]
		public string _value;
	}

	[XmlType("debug")]
	public class DebugType
	{
		[XmlElement]
		public DataPatchType datapatch;

		[XmlAttribute]
		public uint __dp;

		[XmlAttribute]
		public uint __ap;

		[XmlAttribute]
		public string svd;

		[XmlAttribute]
		public string Pname;

		[XmlAnyAttribute]
		public List<XmlNode> lax = new List<XmlNode>();
	}

	[XmlType("memory")]
	public struct MemoryType
	{
		[XmlAttribute]
		public string Pname;

		[XmlAttribute]
		public string id;

		[XmlAttribute]
		public string start;

		[XmlAttribute]
		public string size;

		[XmlAttribute]
		public uint init;

		[XmlAttribute("default")]
		public uint _default;

		[XmlAttribute]
		public uint startup;
	}

	[XmlType("algorithm")]
	public struct AlgorithmType
	{
		[XmlAttribute]
		public string Pname;

		[XmlAttribute]
		public string name;

		[XmlAttribute]
		public string start;

		[XmlAttribute]
		public string size;

		[XmlAttribute]
		public string RAMstart;

		[XmlAttribute]
		public string RAMsize;

		[XmlAttribute("default")]
		public uint _default;
	}

	public class DebugConfigType
	{
		[XmlAttribute("default")]
		public string _default;

		[XmlAttribute]
		public uint clock;

		[XmlAttribute]
		public uint swj;

		[XmlAnyAttribute]
		public List<XmlNode> lax = new List<XmlNode>();
	}

	[XmlType("feature")]
	public struct DeviceFeatureType
	{
		[XmlAttribute]
		public string Pname;

		[XmlAttribute]
		public string type;

		[XmlAttribute]
		public decimal n;

		[XmlAttribute]
		public decimal m;

		[XmlAttribute]
		public string name;

		[XmlAttribute]
		public int count;
	}

	public abstract class BaseInfoSet
	{
		[XmlElement]
		public ProcessorType processor;

		[XmlElement]
		public DebugConfigType debugconfig;

		[XmlElement(Type = typeof(CompileType))]
		[XmlElement(Type = typeof(MemoryType))]
		[XmlElement(Type = typeof(AlgorithmType))]
		[XmlElement(Type = typeof(DescriptionType))]
		[XmlElement(Type = typeof(DeviceFeatureType))]
		[XmlElement(Type = typeof(EnvironmentType))]
		[XmlElement(Type = typeof(DebugPortType))]
		[XmlElement(Type = typeof(DebugType))]
		[XmlElement(Type = typeof(TraceType))]
		[XmlElement(Type = typeof(DebugVarsType))]
		public List<object> props;

		[XmlIgnore]
		public BaseInfoSet _parent;
	}

	[XmlType("device")]
	public class DeviceType : BaseInfoSet
	{
		[XmlAttribute]
		public string Dname;
	}

	[XmlType("subFamily")]
	public class subFamilyType : BaseInfoSet
	{
		[XmlElement("device")]
		public List<DeviceType> _devices;

		[XmlAttribute]
		public string DsubFamily;
	}

	[XmlType("family")]
	public class FamilyType : BaseInfoSet
	{
		[XmlElement("device")]
		public List<DeviceType> _devices;

		[XmlElement("subFamily")]
		public List<subFamilyType> _subFamilys;

		[XmlAttribute]
		public string Dfamily;

		[XmlAttribute]
		public string Dvendor;
	}

	[XmlElement]
	public string vendor;

	[XmlElement]
	public string url;

	[XmlElement]
	public string name;

	[XmlElement]
	public string description;

	[XmlArray]
	public List<release> releases = new List<release>();

	[XmlArray]
	public List<keyword> keywords = new List<keyword>();

	[XmlArray]
	public List<FamilyType> devices = new List<FamilyType>();

	[XmlIgnore]
	private string _PackFile;

	[XmlIgnore]
	public string PackFile => _PackFile;

	[XmlIgnore]
	public string PackRelativeRoot
	{
		get
		{
			string text = IDEPath.MdkPath.ToLower();
			if (_PackFile.ToLower().StartsWith(text))
			{
				return Path.GetDirectoryName(_PackFile.Substring(text.Length + 1));
			}
			return Path.GetDirectoryName(_PackFile);
		}
	}

	private new static object Create(Type type, Stream theStream)
	{
		return null;
	}

	private new static object Create(Type type, string theXml)
	{
		return null;
	}

	private void BuildDeviceLookBackInfo(BaseInfoSet theParent, List<DeviceType> DeviceSet)
	{
		if (DeviceSet == null)
		{
			return;
		}
		foreach (DeviceType item in DeviceSet)
		{
			item._parent = theParent;
		}
	}

	private void BuildSubFamilyLookBackInfo(BaseInfoSet theParent, List<subFamilyType> subFamilySet)
	{
		if (subFamilySet == null)
		{
			return;
		}
		foreach (subFamilyType item in subFamilySet)
		{
			item._parent = theParent;
			BuildDeviceLookBackInfo(item, item._devices);
		}
	}

	private void BuildLookBackInfo()
	{
		try
		{
			foreach (FamilyType device in devices)
			{
				device._parent = null;
				BuildDeviceLookBackInfo(device, device._devices);
			}
		}
		catch (Exception)
		{
		}
	}

	public static UvPack CreateFromFile(string theUvPackFile)
	{
		UvPack uvPack = (UvPack)BaseProject.CreateFromFile(typeof(UvPack), theUvPackFile);
		if (uvPack != null)
		{
			uvPack._PackFile = theUvPackFile;
			uvPack.BuildLookBackInfo();
		}
		return uvPack;
	}

	public static UvPack CreateFromID(string theUvPackID)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		int num = theUvPackID.IndexOf('.');
		if (num > 0)
		{
			text = theUvPackID.Substring(0, num);
			int num2 = theUvPackID.IndexOf('.', num + 1);
			if (num2 > 0)
			{
				text2 = theUvPackID.Substring(num + 1, num2 - num - 1);
				text3 = theUvPackID.Substring(num2 + 1);
			}
		}
		return CreateFromFile(string.Format("{0}\\Pack\\{1}\\{2}\\{3}\\{1}.{2}.pdsc", IDEPath.MdkPath, text, text2, text3));
	}

	public DeviceType FindDevice(string DeviceName, List<DeviceType> DeviceSet)
	{
		DeviceType result = null;
		if (DeviceSet != null)
		{
			foreach (DeviceType item in DeviceSet)
			{
				if (DeviceName.Equals(item.Dname, StringComparison.CurrentCultureIgnoreCase))
				{
					return item;
				}
			}
			return result;
		}
		return result;
	}

	public DeviceType FindDevice(string DeviceName)
	{
		DeviceType deviceType = null;
		try
		{
			foreach (FamilyType device in devices)
			{
				deviceType = FindDevice(DeviceName, device._devices);
				if (deviceType == null)
				{
					foreach (subFamilyType subFamily in device._subFamilys)
					{
						deviceType = FindDevice(DeviceName, subFamily._devices);
						if (deviceType != null)
						{
							break;
						}
					}
					if (deviceType != null)
					{
						return deviceType;
					}
					continue;
				}
				return deviceType;
			}
			return deviceType;
		}
		catch (Exception)
		{
			return deviceType;
		}
	}
}
