using System.Text;
using System.IO.Compression;

namespace pradosh_arduino
{
    class Chiefwood
    {
        string version = "v1.0.0";

        string simple_version = "1";

        static int compressLevel = 2; // Defaults to none


        static void Main(string[] args)
        {
            Chiefwood cw = new Chiefwood();
            if (args.Length == 0)
            {
                Console.WriteLine("Please pass some arguments.");
                return;
            }
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains("-create"))
                {
                    Console.WriteLine("Creating " + args[i + 1] + ".cw" + cw.simple_version + " file.");
                    cw.CreateCWF(args[i + 1]);
                }
                else if (args[i].Contains("-load"))
                {
                    Console.WriteLine("Reading " + args[i + 1] + ".cw" + cw.simple_version + " file.");
                    cw.ReadCWF(args[i + 1]);
                }
                else if (args[0].Contains("-compression"))
                {
                    if (args[1] == "none")
                    {
                        compressLevel = 2;
                        Console.WriteLine("Compression is disabled.");
                    }
                    else if (args[1] == "fast")
                    {
                        compressLevel = 1;
                        Console.WriteLine("Compression is set to fast.");
                    }
                    else if (args[1] == "optimal")
                    {
                        compressLevel = 0;
                        Console.WriteLine("Compression is set to optimal.");
                    }
                    else if (args[1] == "best")
                    {
                        compressLevel = 3;
                        Console.WriteLine("Compression is set to best.");
                    }
                    else
                    {
                        Console.WriteLine("Only 'none, fast, optimal, best' is allowed for compression.");
                        Environment.Exit(-1);
                    }
                }
                else if (args[i].Contains("-help"))
                {
                    Console.WriteLine("Welcome to Chiefwood Compressor - Here is the help\n\t-create <name>\t        This argument is used to create a Chiefwood file (no file extension)\n\t-load <name>\t        Loads a Chiefwood file (no file extension)\n\t-compression <level>\tself explainable, \'none, fast, optimal, best\'");
                }
            }
        }

        public void CreateCWF(string name)
        {
            try
            {
                // Create Chiefwood file
                using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(name + ".cw" + simple_version, FileMode.Create)))
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
            catch (IOException)
            {
                Console.WriteLine("File is being used by another process. (I/O Exception)");
            }
        }

        public void ReadCWF(string name)
        {
            using (BinaryReader binaryReader = new BinaryReader(File.Open(name + ".cw" + simple_version, FileMode.Open)))
            {
                string header = binaryReader.ReadString();
                if (header.Contains("Chiefwood"))
                {
                    if (header.Contains(version))
                    {
                        int totalFiles = binaryReader.ReadInt32();
                        for (int i = 0; i < totalFiles; i++)
                        {
                            string dec_filename = DecryptString(binaryReader.ReadString()).Replace("./contents\\", "");
                            Console.WriteLine("Found file " + dec_filename + " Decrypting...");
                            File.Create(dec_filename).Close();
                            File.WriteAllText(dec_filename, DecryptString(binaryReader.ReadString()));
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

        public string DecryptString(string base64EncodedData)
        {
            var base64EncodedBytes = Decompress(System.Convert.FromBase64String(base64EncodedData));
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string EncryptString(string plainText)
        {

            var plainTextBytes = Compress(Encoding.UTF8.GetBytes(plainText));
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public byte[] Compress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var brotliStream = new BrotliStream(memoryStream, (CompressionLevel)Enum.ToObject(typeof(CompressionLevel), compressLevel)))
                {
                    brotliStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        public byte[] Decompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var decompressStream = new BrotliStream(memoryStream, CompressionMode.Decompress))
                    {
                        decompressStream.CopyTo(outputStream);
                    }
                    return outputStream.ToArray();
                }
            }
        }
    }
}
