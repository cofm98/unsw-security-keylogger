
class KeyReader
{
    public static void Main (string[] args)
    {
        Console.WriteLine("Connected....");
        if(args.Length > 0)
            Console.Write(ReadFile(args[0]));
        else
            Console.WriteLine("Failed to open file. No file specified");
    }

    static bool ReadFile (string fileURL)
    {
        try
        {
            List<char> chars = new List<char>();
            FileStream file = File.Open(fileURL, FileMode.Open, FileAccess.Read);
            Console.WriteLine("Reading file " + fileURL + "...");
            using (BinaryReader reader = new BinaryReader(file))
            {
                int point = 0;
                while(point < file.Length)
                {
                    ushort bit = reader.ReadUInt16();
                    int code = (byte)bit ^ (bit >> 8);
                    chars.Add((char)code);
                    point += sizeof(char);
                }
                reader.Close();
            }
            if(File.Exists("output.bin"))
                File.Delete("output.bin");
            using(FileStream writefile = File.Open("output.bin", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using(BinaryWriter writer = new BinaryWriter(writefile))
                {
                    foreach(char eachar in chars)
                    {
                        writer.Write(eachar);
                    }
                }
            }

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }
}