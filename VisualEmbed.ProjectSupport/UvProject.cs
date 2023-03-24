using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace VisualEmbed.ProjectSupport;

[XmlType("Project")]
public sealed class UvProject : BaseProject
{
	public sealed class UvUsrCmd
	{
		[XmlElement]
		public int RunUserProg1;

		[XmlElement]
		public int RunUserProg2;

		[XmlElement]
		public string UserProg1Name;

		[XmlElement]
		public string UserProg2Name;

		[XmlElement]
		public int UserProg1Dos16Mode;

		[XmlElement]
		public int UserProg2Dos16Mode;

		[XmlElement]
		public int nStopA1X;

		[XmlElement]
		public int nStopA2X;
	}

	[XmlType("TargetStatus")]
	public sealed class UvTargetStatus
	{
		[XmlElement]
		public int Error;

		[XmlElement]
		public int ExitCodeStop;

		[XmlElement]
		public int ButtonStop;

		[XmlElement]
		public int NotGenerated;

		[XmlElement]
		public int InvalidFlash = 1;
	}

	[XmlType("TargetCommonOption")]
	public sealed class UvTargetCommonOption
	{
		[XmlElement]
		public string Device;

		[XmlElement]
		public string Vendor;

		[XmlElement]
		public string PackID;

		[XmlElement]
		public string PackURL;

		[XmlElement]
		public string Cpu;

		[XmlElement]
		public string FlashUtilSpec;

		[XmlElement]
		public string StartupFile;

		[XmlElement]
		public string FlashDriverDll;

		[XmlElement]
		public string DeviceId = "0";

		[XmlElement]
		public string RegisterFile;

		[XmlElement]
		public string MemoryEnv;

		[XmlElement]
		public string Cmp;

		[XmlElement]
		public string Asm;

		[XmlElement]
		public string Linker;

		[XmlElement]
		public string OHString;

		[XmlElement]
		public string InfinionOptionDll;

		[XmlElement]
		public string SLE66CMisc;

		[XmlElement]
		public string SLE66AMisc;

		[XmlElement]
		public string SLE66LinkerMisc;

		[XmlElement]
		public string SFDFile;

		[XmlElement]
		public int bCustSvd;

		[XmlElement]
		public int UseEnv;

		[XmlElement]
		public string BinPath;

		[XmlElement]
		public string IncludePath;

		[XmlElement]
		public string LibPath;

		[XmlElement]
		public string RegisterFilePath;

		[XmlElement]
		public string DBRegisterFilePath;

		public UvTargetStatus TargetStatus = new UvTargetStatus();

		[XmlElement]
		public string OutputDirectory;

		[XmlElement]
		public string OutputName;

		[XmlElement]
		public int CreateExecutable = 1;

		[XmlElement]
		public int CreateLib;

		[XmlElement]
		public int CreateHexFile;

		[XmlElement]
		public int DebugInformation = 1;

		[XmlElement]
		public int BrowseInformation;

		[XmlElement]
		public string ListingPath;

		[XmlElement]
		public int HexFormatSelection = 1;

		[XmlElement]
		public int Merge32K;

		[XmlElement]
		public int CreateBatchFile;

		[XmlElement]
		public UvUsrCmd BeforeCompile = new UvUsrCmd();

		[XmlElement]
		public UvUsrCmd AfterCompile = new UvUsrCmd();

		[XmlElement]
		public UvUsrCmd BeforeMake = new UvUsrCmd();

		[XmlElement]
		public UvUsrCmd AfterMake = new UvUsrCmd();

		[XmlElement]
		public int SelectedForBatchBuild;

		[XmlElement]
		public string SVCSIdString;
	}

	[XmlType("CommonProperty")]
	public sealed class UvCommonProperty
	{
		[XmlElement]
		public int UseCPPCompiler;

		[XmlElement]
		public int RVCTCodeConst;

		[XmlElement]
		public int RVCTZI;

		[XmlElement]
		public int RVCTOtherData;

		[XmlElement]
		public int ModuleSelection;

		[XmlElement]
		public int IncludeInBuild = 1;

		[XmlElement]
		public int AlwaysBuild;

		[XmlElement]
		public int GenerateAssemblyFile;

		[XmlElement]
		public int AssembleAssemblyFile;

		[XmlElement]
		public int PublicsOnly;

		[XmlElement]
		public int StopOnExitCode = 3;

		[XmlElement]
		public string CustomArgument;

		[XmlElement]
		public string IncludeLibraryModules;

		[XmlElement]
		public int ComprImg = 1;
	}

	[XmlType("DllOption")]
	public sealed class UvDllOption
	{
		[XmlElement]
		public string SimDllName;

		[XmlElement]
		public string SimDllArguments;

		[XmlElement]
		public string SimDlgDll;

		[XmlElement]
		public string SimDlgDllArguments;

		[XmlElement]
		public string TargetDllName;

		[XmlElement]
		public string TargetDllArguments;

		[XmlElement]
		public string TargetDlgDll;

		[XmlElement]
		public string TargetDlgDllArguments;
	}

	[XmlType("OPTHX")]
	public sealed class UvDbgOPTHX
	{
		[XmlElement]
		public int HexSelection = 1;

		[XmlElement]
		public int HexRangeLowAddress;

		[XmlElement]
		public int HexRangeHighAddress;

		[XmlElement]
		public int HexOffset;

		[XmlElement]
		public int Oh166RecLen = 16;
	}

	[XmlType("Simulator")]
	public sealed class UvDbgSimulator
	{
		[XmlElement]
		public int UseSimulator = 1;

		[XmlElement]
		public int LoadApplicationAtStartup = 1;

		[XmlElement]
		public int RunToMain = 1;

		[XmlElement]
		public int RestoreBreakpoints = 1;

		[XmlElement]
		public int RestoreWatchpoints = 1;

		[XmlElement]
		public int RestoreMemoryDisplay = 1;

		[XmlElement]
		public int RestoreFunctions = 1;

		[XmlElement]
		public int RestoreToolbox = 1;

		[XmlElement]
		public int LimitSpeedToRealTime = 1;

		[XmlElement]
		public int RestoreSysVw = 1;
	}

	public sealed class UvDbgTarget
	{
		[XmlElement]
		public int UseTarget;

		[XmlElement]
		public int LoadApplicationAtStartup = 1;

		[XmlElement]
		public int RunToMain = 1;

		[XmlElement]
		public int RestoreBreakpoints = 1;

		[XmlElement]
		public int RestoreWatchpoints = 1;

		[XmlElement]
		public int RestoreMemoryDisplay = 1;

		[XmlElement]
		public int RestoreFunctions = 1;

		[XmlElement]
		public int RestoreToolbox = 1;

		[XmlElement]
		public int LimitSpeedToRealTime = 1;

		[XmlElement]
		public int RestoreSysVw = 1;
	}

	[XmlType("SimDlls")]
	public sealed class UvSimDlls
	{
		[XmlElement]
		public string CpuDll;

		[XmlElement]
		public string CpuDllArguments;

		[XmlElement]
		public string PeripheralDll;

		[XmlElement]
		public string PeripheralDllArguments;

		[XmlElement]
		public string InitializationFile;
	}

	[XmlType("TargetDlls")]
	public sealed class UvTargetDlls
	{
		[XmlElement]
		public string CpuDll;

		[XmlElement]
		public string CpuDllArguments;

		[XmlElement]
		public string PeripheralDll;

		[XmlElement]
		public string PeripheralDllArguments;

		[XmlElement]
		public string InitializationFile;

		[XmlElement]
		public string Driver;
	}

	[XmlType("DebugOption")]
	public sealed class UvDebugOption
	{
		public UvDbgOPTHX OPTHX = new UvDbgOPTHX();

		public UvDbgSimulator Simulator = new UvDbgSimulator();

		[XmlElement("Target")]
		public UvDbgTarget Target = new UvDbgTarget();

		[XmlElement]
		public int RunDebugAfterBuild;

		[XmlElement]
		public int TargetSelection;

		public UvSimDlls SimDlls = new UvSimDlls();

		public UvTargetDlls TargetDlls = new UvTargetDlls();
	}

	[XmlType("Flash1")]
	public sealed class UvUtlFlash1
	{
		[XmlElement]
		public int UseTargetDll = 1;

		[XmlElement]
		public int UseExternalTool;

		[XmlElement]
		public int RunIndependent;

		[XmlElement]
		public int UpdateFlashBeforeDebugging = 1;

		[XmlElement]
		public int Capability = 1;

		[XmlElement]
		public int DriverSelection = 4096;
	}

	[XmlType("Utilities")]
	public sealed class UvUtilities
	{
		public UvUtlFlash1 Flash1 = new UvUtlFlash1();

		[XmlElement]
		public int bUseTDR = 1;

		[XmlElement]
		public string Flash2;

		[XmlElement]
		public string Flash3;

		[XmlElement]
		public string Flash4;

		[XmlElement]
		public string pFcarmOut;

		[XmlElement]
		public string pFcarmGrp;

		[XmlElement]
		public string pFcArmRoot;

		[XmlElement]
		public string FcArmLst;
	}

	public sealed class UvMemDec
	{
		[XmlElement]
		public int Type;

		[XmlElement]
		public string StartAddress = "0x0";

		[XmlElement]
		public string Size = "0x0";
	}

	[XmlType("OnChipMemories")]
	public sealed class UvOnChipMemories
	{
		[XmlElement]
		public UvMemDec Ocm1 = new UvMemDec();

		[XmlElement]
		public UvMemDec Ocm2 = new UvMemDec();

		[XmlElement]
		public UvMemDec Ocm3 = new UvMemDec();

		[XmlElement]
		public UvMemDec Ocm4 = new UvMemDec();

		[XmlElement]
		public UvMemDec Ocm5 = new UvMemDec();

		[XmlElement]
		public UvMemDec Ocm6 = new UvMemDec();

		[XmlElement]
		public UvMemDec IRAM = new UvMemDec();

		[XmlElement]
		public UvMemDec IROM = new UvMemDec();

		[XmlElement]
		public UvMemDec XRAM = new UvMemDec();

		[XmlElement]
		public UvMemDec OCR_RVCT1 = new UvMemDec();

		[XmlElement]
		public UvMemDec OCR_RVCT2 = new UvMemDec();

		[XmlElement]
		public UvMemDec OCR_RVCT3 = new UvMemDec();

		[XmlElement]
		public UvMemDec OCR_RVCT4 = new UvMemDec();

		[XmlElement]
		public UvMemDec OCR_RVCT5 = new UvMemDec();

		[XmlElement]
		public UvMemDec OCR_RVCT6 = new UvMemDec();

		[XmlElement]
		public UvMemDec OCR_RVCT7 = new UvMemDec();

		[XmlElement]
		public UvMemDec OCR_RVCT8 = new UvMemDec();

		[XmlElement]
		public UvMemDec OCR_RVCT9 = new UvMemDec();

		[XmlElement]
		public UvMemDec OCR_RVCT10 = new UvMemDec();
	}

	[XmlType("ArmAdsMisc")]
	public sealed class UvArmAdsMisc
	{
		[XmlElement]
		public int GenerateListings;

		[XmlElement]
		public int asHll = 1;

		[XmlElement]
		public int asAsm = 1;

		[XmlElement]
		public int asMacX = 1;

		[XmlElement]
		public int asSyms = 1;

		[XmlElement]
		public int asFals = 1;

		[XmlElement]
		public int asDbgD = 1;

		[XmlElement]
		public int asForm = 1;

		[XmlElement]
		public int ldLst;

		[XmlElement]
		public int ldmm = 1;

		[XmlElement]
		public int ldXref = 1;

		[XmlElement]
		public int BigEnd;

		[XmlElement]
		public int AdsALst;

		[XmlElement]
		public int AdsACrf = 1;

		[XmlElement]
		public int AdsANop;

		[XmlElement]
		public int AdsANot;

		[XmlElement]
		public int AdsLLst;

		[XmlElement]
		public int AdsLmap = 1;

		[XmlElement]
		public int AdsLcgr = 1;

		[XmlElement]
		public int AdsLsym = 1;

		[XmlElement]
		public int AdsLszi = 1;

		[XmlElement]
		public int AdsLtoi = 1;

		[XmlElement]
		public int AdsLsun = 1;

		[XmlElement]
		public int AdsLven = 1;

		[XmlElement]
		public int AdsLsxf = 1;

		[XmlElement]
		public int RvctClst = 1;

		[XmlElement]
		public int GenPPlst;

		[XmlElement]
		public string AdsCpuType;

		[XmlElement]
		public string RvctDeviceName;

		[XmlElement]
		public int mOS;

		[XmlElement]
		public int uocRom;

		[XmlElement]
		public int uocRam;

		[XmlElement]
		public int hadIROM = 1;

		[XmlElement]
		public int hadIRAM = 1;

		[XmlElement]
		public int hadXRAM;

		[XmlElement]
		public int uocXRam;

		[XmlElement]
		public int RvdsVP;

		[XmlElement]
		public int hadIRAM2;

		[XmlElement]
		public int hadIROM2;

		[XmlElement]
		public int StupSel = 8;

		[XmlElement]
		public int useUlib;

		[XmlElement]
		public int EndSel;

		[XmlElement]
		public int uLtcg;

		[XmlElement]
		public int nSecure;

		[XmlElement]
		public int RoSelD = 3;

		[XmlElement]
		public int RwSelD = 3;

		[XmlElement]
		public int CodeSel;

		[XmlElement]
		public int OptFeed;

		[XmlElement]
		public int NoZi1;

		[XmlElement]
		public int NoZi2;

		[XmlElement]
		public int NoZi3;

		[XmlElement]
		public int NoZi4;

		[XmlElement]
		public int NoZi5;

		[XmlElement]
		public int Ro1Chk;

		[XmlElement]
		public int Ro2Chk;

		[XmlElement]
		public int Ro3Chk;

		[XmlElement]
		public int Ir1Chk = 1;

		[XmlElement]
		public int Ir2Chk;

		[XmlElement]
		public int Ra1Chk;

		[XmlElement]
		public int Ra2Chk;

		[XmlElement]
		public int Ra3Chk;

		[XmlElement]
		public int Im1Chk = 1;

		[XmlElement]
		public int Im2Chk;

		public UvOnChipMemories OnChipMemories = new UvOnChipMemories();

		[XmlElement]
		public string RvctStartVector;
	}

	[XmlType("VariousControls")]
	public sealed class UvVariousControls
	{
		[XmlElement("MiscControls")]
		public string MiscControls = string.Empty;

		[XmlElement("Define")]
		public string Define = string.Empty;

		[XmlElement("Undefine")]
		public string Undefine = string.Empty;

		[XmlElement("IncludePath")]
		public string IncludePath = string.Empty;
	}

	[XmlType("Cads")]
	public sealed class UvCads
	{
		[XmlElement]
		public int interw = 1;

		[XmlElement]
		public int Optim = 1;

		[XmlElement]
		public int oTime;

		[XmlElement]
		public int SplitLS;

		[XmlElement]
		public int OneElfS = 1;

		[XmlElement]
		public int Strict;

		[XmlElement]
		public int EnumInt;

		[XmlElement]
		public int PlainCh;

		[XmlElement]
		public int Ropi;

		[XmlElement]
		public int Rwpi;

		[XmlElement]
		public int wLevel = 2;

		[XmlElement]
		public int uThumb;

		[XmlElement]
		public int uSurpInc;

		[XmlElement]
		public int uC99;

		[XmlElement]
		public int useXO;

		[XmlElement]
		public int v6Lang;

		[XmlElement]
		public int v6LangP;

		[XmlElement]
		public int vShortEn;

		[XmlElement]
		public int vShortWch;

		[XmlElement]
		public UvVariousControls VariousControls = new UvVariousControls();
	}

	[XmlType("Aads")]
	public sealed class UvAads
	{
		[XmlElement]
		public int interw = 1;

		[XmlElement]
		public int Ropi;

		[XmlElement]
		public int Rwpi;

		[XmlElement]
		public int thumb;

		[XmlElement]
		public int SplitLS;

		[XmlElement]
		public int SwStkChk;

		[XmlElement]
		public int NoWarn;

		[XmlElement]
		public int uSurpInc;

		[XmlElement]
		public int useXO;

		[XmlElement]
		public UvVariousControls VariousControls = new UvVariousControls();
	}

	[XmlType("LDads")]
	public sealed class UvLDads
	{
		[XmlElement]
		public int umfTarg = 1;

		[XmlElement]
		public int Ropi;

		[XmlElement]
		public int Rwpi;

		[XmlElement]
		public int noStLib;

		[XmlElement]
		public int RepFail = 1;

		[XmlElement]
		public int useFile;

		[XmlElement]
		public string TextAddressRange;

		[XmlElement]
		public string DataAddressRange;

		[XmlElement]
		public string pXoBase;

		[XmlElement]
		public string ScatterFile;

		[XmlElement]
		public string IncludeLibs;

		[XmlElement]
		public string IncludeLibsPath;

		[XmlElement]
		public string Misc;

		[XmlElement]
		public string LinkerInputFile;

		[XmlElement]
		public string DisabledWarnings;
	}

	public struct UvArmAds
	{
		public UvArmAdsMisc ArmAdsMisc;

		public UvCads Cads;

		public UvAads Aads;

		public UvLDads LDads;
	}

	[XmlType("TargetOption")]
	public sealed class UvTargetOption
	{
		public UvTargetCommonOption TargetCommonOption = new UvTargetCommonOption();

		public UvCommonProperty CommonProperty = new UvCommonProperty();

		public UvDllOption DllOption = new UvDllOption();

		public UvDebugOption DebugOption = new UvDebugOption();

		public UvUtilities Utilities = new UvUtilities();

		[XmlElement("TargetArmAds")]
		public UvArmAds TargetArmAds;
	}

	[XmlType("FileOption")]
	public class UvFileOption
	{
		public UvCommonProperty CommonProperty;

		[XmlElement("FileArmAds")]
		public UvArmAds FileArmAds;
	}

	[XmlType("File")]
	public sealed class UvFile
	{
		[XmlElement]
		public string FileName;

		[XmlElement]
		public int FileType;

		[XmlElement]
		public string FilePath;

		public UvFileOption FileOption;

		[XmlIgnore]
		public bool IsProcessed;
	}

	[XmlType("Files")]
	public sealed class UvFileList : List<UvFile>
	{
	}

	[XmlType("Group")]
	public sealed class UvGroup
	{
		[XmlElement]
		public string GroupName;

		public UvFileList Files = new UvFileList();
	}

	[XmlType("Groups")]
	public sealed class UvGroupList : List<UvGroup>
	{
	}

	[XmlType("Target")]
	public sealed class UvTarget
	{
		[XmlElement]
		public string TargetName;

		[XmlElement]
		public string ToolsetNumber;

		[XmlElement]
		public string ToolsetName;

		[XmlElement]
		public string pCCUsed;

		public UvTargetOption TargetOption = new UvTargetOption();

		public UvGroupList Groups = new UvGroupList();
	}

	[XmlType("Targets")]
	public sealed class UvTargetList : List<UvTarget>
	{
	}

	[XmlType("package")]
	public sealed class UvRTEPackage
	{
		[XmlAttribute]
		public string name;

		[XmlAttribute]
		public string schemaVersion;

		[XmlAttribute]
		public string url;

		[XmlAttribute]
		public string vendor;

		[XmlAttribute]
		public string version;
	}

	[XmlType("targetInfo")]
	public sealed class UvRTETargetInfo
	{
		[XmlAttribute("name")]
		public string name;
	}

	[XmlType("targetInfos")]
	public sealed class UvRTETargetInfoList : List<UvRTETargetInfo>
	{
	}

	[XmlType("component")]
	public sealed class UvRTEComponent
	{
		[XmlAttribute]
		public string Cclass;

		[XmlAttribute]
		public string Cgroup;

		[XmlAttribute]
		public string Cvendor;

		[XmlAttribute]
		public string Cversion;

		[XmlAttribute]
		public string condition;

		[XmlElement]
		public UvRTEPackage package = new UvRTEPackage();

		public UvRTETargetInfoList targetInfos = new UvRTETargetInfoList();
	}

	[XmlType("components")]
	public sealed class UvRTEComponentList : List<UvRTEComponent>
	{
	}

	[XmlType("file")]
	public sealed class UvRTEFile
	{
		[XmlAttribute]
		public string attr;

		[XmlAttribute]
		public string category;

		[XmlAttribute]
		public string name;

		[XmlElement]
		public string instance;

		public UvRTEComponent component = new UvRTEComponent();

		public UvRTEPackage package = new UvRTEPackage();

		public UvRTETargetInfoList targetInfos = new UvRTETargetInfoList();
	}

	[XmlType("files")]
	public sealed class UvRTEFileList : List<UvRTEFile>
	{
	}

	[XmlType("RTE")]
	public sealed class UvRTE
	{
		public UvRTEComponentList components = new UvRTEComponentList();

		public UvRTEFileList files = new UvRTEFileList();
	}

	[XmlElement]
	public string SchemaVersion;

	[XmlElement]
	public string Header;

	public UvTargetList Targets = new UvTargetList();

	public UvRTE RTE = new UvRTE();

	public static UvProject Create(Stream theStream)
	{
		return (UvProject)BaseProject.Create(typeof(UvProject), theStream);
	}

	public static UvProject Create(string theXml)
	{
		return (UvProject)BaseProject.Create(typeof(UvProject), theXml);
	}

	public static UvProject CreateFromFile(string theUvProjxFile)
	{
		return (UvProject)BaseProject.CreateFromFile(typeof(UvProject), theUvProjxFile);
	}

	public bool FindFile(string FilePath, ref int TargetIdx, out UvTarget Target, out UvGroup Group, out UvFile theFile)
	{
		Target = null;
		Group = null;
		theFile = null;
		try
		{
			if (TargetIdx < 0)
			{
				TargetIdx = 0;
			}
			if (TargetIdx < Targets.Count)
			{
				string text = FilePath.Trim().ToLower();
				while (TargetIdx < Targets.Count)
				{
					foreach (UvGroup group in Targets[TargetIdx].Groups)
					{
						foreach (UvFile file in group.Files)
						{
							if (file.FilePath.Trim().ToLower() == text)
							{
								Target = Targets[TargetIdx];
								Group = group;
								theFile = file;
								return true;
							}
						}
					}
					TargetIdx++;
				}
			}
		}
		catch (Exception)
		{
		}
		return false;
	}

	public bool AddTarget(string TargetName, out UvTarget theTarget)
	{
		bool result = false;
		try
		{
			UvTarget uvTarget = new UvTarget();
			uvTarget.TargetName = TargetName;
			Targets.Add(uvTarget);
			theTarget = uvTarget;
			result = true;
			return result;
		}
		catch (Exception)
		{
			theTarget = null;
			return result;
		}
	}

	public bool AddGroup(UvTarget target, string GroupName, out UvGroup theGroup)
	{
		bool result = false;
		try
		{
			if (target != null)
			{
				UvGroup uvGroup = new UvGroup();
				uvGroup.GroupName = GroupName;
				target.Groups.Add(uvGroup);
				theGroup = uvGroup;
				result = true;
				return result;
			}
			theGroup = null;
			return result;
		}
		catch (Exception)
		{
			theGroup = null;
			return result;
		}
	}

	public bool AddFile(UvGroup group, string FilePathName, int FileType, out UvFile theFile)
	{
		bool result = false;
		try
		{
			if (group != null)
			{
				UvFile uvFile = new UvFile();
				uvFile.FileName = Path.GetFileName(FilePathName);
				uvFile.FilePath = FilePathName;
				uvFile.FileType = FileType;
				group.Files.Add(uvFile);
				theFile = uvFile;
				result = true;
				return result;
			}
			theFile = null;
			return result;
		}
		catch (Exception)
		{
			theFile = null;
			return result;
		}
	}
}
