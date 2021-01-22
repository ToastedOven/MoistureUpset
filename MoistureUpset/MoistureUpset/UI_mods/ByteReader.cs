using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MoistureUpset
{
    public static class ByteReader
    {
        public static byte[] readbytes(string path)
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                using (BinaryReader br = new BinaryReader(assetStream))
                {
                    return br.ReadBytes((int)assetStream.Length);
                }
            }
        }
    }
}
