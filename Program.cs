using System.Text;

namespace pradosh_arduino
{
  class Chiefwood
  {
    string version = "v1.0.0";

    string simple_version = "1";

    static void Main(string[] args)
    {
      Chiefwood cw = new Chiefwood();
      for (int i = 0; i < args.Length; i++)
      {
        if (args[i].Contains("-create"))
        {
          Console.WriteLine("Creating .cw" + cw.simple_version + " file.");
          cw.CreateCWF(args[i + 1]);
        }
        else if (args[i].Contains("-load"))
        {
          Console.WriteLine("Reading .cw" + cw.simple_version + " file.");
          cw.ReadCWF(args[i + 1]);
        }
      }
    }

    public void CreateCWF(string name)
    {
      // Create Chiefwood file
      using (
        BinaryWriter binaryWriter =
          new BinaryWriter(File
              .Open(name + ".cw" + simple_version, FileMode.Create))
      )
      {
        binaryWriter.Write("Chiefwood " + version);
        binaryWriter.Write(Directory.GetFiles("./contents").Length);
        foreach (string fileName in Directory.GetFiles("./contents"))
        {
          binaryWriter.Write(EncryptString(fileName));
          binaryWriter.Write(EncryptString(File.ReadAllText(fileName)));
        }
      }
    }

    public void ReadCWF(string name)
    {
      using (
        BinaryReader binaryReader =
          new BinaryReader(File
              .Open(name + ".cw" + simple_version, FileMode.Open))
      )
      {
        string header = binaryReader.ReadString();
        if (header.Contains("Chiefwood"))
        {
          if (header.Contains(version))
          {
            int totalFiles = binaryReader.ReadInt32();
            for (int i = 0; i < totalFiles; i++)
            {
              string filename = binaryReader.ReadString();
              Console
                .WriteLine("Found file " +
                DecryptString(filename).Replace("./contents\\", "") +
                " Decrypting...");
              File
                .Create(DecryptString(filename).Replace("./contents\\", ""))
                .Close();
              File
                .WriteAllText(DecryptString(filename)
                  .Replace("./contents\\", ""),
                DecryptString(binaryReader.ReadString()));
            }
          }
          else
          {
            Console.WriteLine("Invalid Version!");
            return;
          }
        }
        else
        {
          Console.WriteLine("Invalid Signature!");
          return;
        }
      }
    }

    public static string DecryptString(string base64EncodedData)
    {
      var base64EncodedBytes =
        System.Convert.FromBase64String(base64EncodedData);
      return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string EncryptString(string plainText)
    {
      var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
      return System.Convert.ToBase64String(plainTextBytes);
    }
  }
}
