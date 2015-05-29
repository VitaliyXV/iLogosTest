using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


public class XmlSerializator : ISerializer
{
	public string Serialize<T>(T value)
	{
		if (value == null)
		{
			return null;
		}

		var serializer = new XmlSerializer(typeof(T));

		var settings = new XmlWriterSettings();
		settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
		settings.Indent = false;
		settings.OmitXmlDeclaration = false;

		using (var textWriter = new StringWriter())
		{
			using (var xmlWriter = XmlWriter.Create(textWriter, settings))
			{
				serializer.Serialize(xmlWriter, value);
			}
			return textWriter.ToString();
		}
	}

	public T Deserialize<T>(string xml)
	{

		if (string.IsNullOrEmpty(xml))
		{
			return default(T);
		}

		var serializer = new XmlSerializer(typeof(T));

		var settings = new XmlReaderSettings();
		// No settings need modifying here

		using (var textReader = new StringReader(xml))
		{
			using (var xmlReader = XmlReader.Create(textReader, settings))
			{
				return (T)serializer.Deserialize(xmlReader);
			}
		}
	}
}

