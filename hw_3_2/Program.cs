using System.Text;

interface IDataSource
{
	public string? Filename { get; set; }
	void WriteData(string data);
	string ReadData();
}

class FileDataSource : IDataSource
{
	public string? Filename { get; set; }
	public FileDataSource(string path)
    {
		Filename = path;
    }

	public void WriteData(string data)
	{
		using FileStream fs = new FileStream(Filename, FileMode.Open);

		using StreamWriter sw = new StreamWriter(fs);
		sw.Write(data);
	}

	public string ReadData()
	{
		using StreamReader sr = new StreamReader(Filename);
		string data = sr.ReadToEnd();

		return data;
	}
}

class Application
{
	private readonly IDataSource _datasource;

	public Application(IDataSource datasource)
    {
		 _datasource = datasource;
    }

	public void WriteData(string data)
    {
		 _datasource.WriteData(data);
    }

	public string ReadData(string path)
    {
		 return _datasource.ReadData();
    }
}

abstract class DataSourceDecorator : IDataSource
{
	protected IDataSource _wrappe { get; set; }
	public string? Filename { get; set; }
	public DataSourceDecorator(IDataSource datasource, string file)
	{
		_wrappe = datasource;
		Filename = file;
	}

	public virtual void WriteData(string message)
	{
	}

	public virtual string ReadData()
	{

		return "";
	}
}


class EncryptionDecorator : DataSourceDecorator
{
	char key = 'M';
	public EncryptionDecorator(string FileName, IDataSource? datasource = null) : base(datasource, FileName) { }

	public override string ReadData()
	{
		using StreamReader sr = new StreamReader(Filename);
		string data = sr.ReadToEnd();
		var decrypted = new StringBuilder();
		string encr = "";

		for (int i = 0; i < data.Length; i++)
        {
			encr = encr + char.ToString((char)(data[i] ^ key));

        }

		return encr.ToString();
	}

	public override void WriteData(string data)
	{
		int len = data.Length;
		string encr = "";
		for (int i = 0; i < len; i++)
        {
			encr = encr + char.ToString((char)(data[i] ^ key));
        }

		using FileStream fs = new FileStream(Filename, FileMode.OpenOrCreate);
		using StreamWriter sw = new StreamWriter(fs);
		sw.Write(encr);
	}
}

class CompressionDecorator : DataSourceDecorator
{
	public CompressionDecorator(IDataSource datasource, string FileName) : base(datasource, FileName) { }

	public override string ReadData()
	{
		return "";
	}

	public override void WriteData(string message)
	{
	}
}

class Program
{
	static void Main()
	{
		string file = "myfile.txt";
		string path = @$"C:\Users\{Environment.UserName}\Desktop\{file}";
		IDataSource data = new FileDataSource(path);
		data = new EncryptionDecorator(path);
		data.WriteData("CRISTIANO RONALDO");
		Console.WriteLine(data.ReadData());
	}
}