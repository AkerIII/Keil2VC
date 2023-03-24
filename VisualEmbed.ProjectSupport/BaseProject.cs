using System;
using System.IO;
using System.Xml.Serialization;

namespace VisualEmbed.ProjectSupport;

public abstract class BaseProject
{
	public static object Create(Type type, Stream theStream)
	{
		try
		{
			return new XmlSerializer(type).Deserialize(theStream);
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static object Create(Type type, string theXml)
	{
		object result = null;
		try
		{
			Stream stream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.Write(theXml);
			streamWriter.Flush();
			stream.Seek(0L, SeekOrigin.Begin);
			result = Create(type, stream);
			streamWriter.Dispose();
			stream.Dispose();
			return result;
		}
		catch (Exception)
		{
			return result;
		}
	}

	public static object CreateFromFile(Type type, string theFile)
	{
		object result = null;
		try
		{
			Stream stream = new FileStream(theFile, FileMode.Open, FileAccess.Read);
			result = Create(type, stream);
			stream.Dispose();
			return result;
		}
		catch (Exception)
		{
			return result;
		}
	}

	public bool Save(Stream theStream)
	{
		try
		{
			new XmlSerializer(GetType()).Serialize(theStream, this);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool Save(out string theXml)
	{
		try
		{
			Stream stream = new MemoryStream();
			bool result = Save(stream);
			stream.Seek(0L, SeekOrigin.Begin);
			StreamReader streamReader = new StreamReader(stream);
			theXml = streamReader.ReadToEnd();
			streamReader.Dispose();
			stream.Dispose();
			return result;
		}
		catch (Exception)
		{
			theXml = string.Empty;
			return false;
		}
	}

	public bool Save(string thePrjFile)
	{
		try
		{
			Stream stream = new FileStream(thePrjFile, FileMode.Create, FileAccess.ReadWrite);
			bool result = Save(stream);
			stream.Dispose();
			return result;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
