using System.Text;

namespace Axolotl.BDSEditor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                switch (args[0])
                {
                    case "bedrock_server":
                        break;
                    case "bedrock_server_mod":
                        break;
                }
                Console.WriteLine("Patching!");
                var readAllBytes = File.ReadAllBytes(args[0] + ".exe");
                var Textbytes = Encoding.UTF8.GetBytes(Keydata.old_Key);
                var arrayFindIndexOf = ArrayFindIndexOf(readAllBytes, Textbytes, 0);
                var bytes = new byte[Textbytes.Length];
                Array.Copy(readAllBytes, arrayFindIndexOf, bytes, 0, Textbytes.Length);
                var data = ArrayReplaceBytes(readAllBytes, Encoding.UTF8.GetBytes(Keydata.new_Key), arrayFindIndexOf);
                File.WriteAllBytes(args[0] + "_patch.exe", data);
                Console.WriteLine("Patched!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                        Console.WriteLine(e);
                        throw;
            }
           
        }

        public static byte[] ArrayReplaceBytes(byte[] pbtBytes, byte[] pBytes,int index)
        {
            var byt = new byte[pbtBytes.Length];
            MemoryStream memory = new MemoryStream(byt);
            int length = pBytes.Length;
            var bytes = new byte[index];
            var by = new byte[pbtBytes.Length - index -length];
            Array.Copy(pbtBytes,0,bytes,0, index);
            Array.Copy(pbtBytes, index+length ,by, 0, pbtBytes.Length - index - length);
            memory.Write(bytes);
            memory.Write(pBytes);
            memory.Write(by);
            return memory.ToArray();
        }
        public static int ArrayFindIndexOf(byte[] pBT, byte[] pMatch, int pStartIndex)
        {
            try
            {
                int mIndex = -1;

                while (mIndex < 0)
                {
                    mIndex = System.Array.IndexOf(pBT, pMatch[0], pStartIndex);
                    if (mIndex < pStartIndex)
                    {
                        return -1;
                    }
                    pStartIndex = mIndex + 1;

                    if (mIndex >= 0)
                    {
                        for (int i = 0; i < pMatch.Length; i++)
                        {
                            if (pBT[mIndex + i] != pMatch[i])
                            {
                                mIndex = -1;
                                break;
                            }
                        }
                    }
                }
                return mIndex;
            }
            catch (NullReferenceException NullEx)
            {
                throw NullEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

    }

    public struct Keydata
    {
        public static string old_Key = "MHYwEAYHKoZIzj0CAQYFK4EEACIDYgAE8ELkixyLcwlZryUQcu1TvPOmI2B7vX83ndnWRUaXm74wFfa5f/lwQNTfrLVHa2PmenpGI6JhIMUJaWZrjmMj90NoKNFSNBuKdm8rYiXsfaz3K36x/1U26HpG0ZxK/V1V";
        public static string new_Key = "MHYwEAYHKoZIzj0CAQYFK4EEACIDYgAECRXueJeTDqNRRgJi/vlRufByu/2G0i2Ebt6YMar5QX/R0DIIyrJMcUpruK4QveTfJSTp3Shlq4Gk34cD/4GUWwkv0DVuzeuB+tXija7HBxii03NHDbPAD0AKnLr2wdAp";
    }
}
