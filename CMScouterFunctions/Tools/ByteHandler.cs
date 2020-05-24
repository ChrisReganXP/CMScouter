using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CMScouterFunctions
{
    internal static class ByteHandler
    {
        private static readonly Encoding textEncoding = Encoding.GetEncoding("ISO-8859-1");

        public static short GetShortFromBytes(byte[] bytes, int start)
        {
            return BitConverter.ToInt16(bytes.Skip(start).Take(2).ToArray(), 0);
        }

        public static int GetIntFromBytes(byte[] bytes, int start)
        {
            return BitConverter.ToInt32(bytes.Skip(start).Take(4).ToArray(), 0);
        }

        public static string GetStringFromBytes(byte[] bytes, int start, int length = 0)
        {
            length = length > 0 ? length : bytes.Length;
            return GetStringFromBytesTerminated(bytes, start, length);
        }

        public static DateTime? GetDateFromBytes(byte[] bytes, int start)
        {
            return ConvertToDate(bytes.Skip(start).Take(4).ToArray());
        }

        public static byte GetByteFromBytes(byte[] bytes, int start, bool isIntrinsicValue = false)
        {
            return isIntrinsicValue ? ConvertIntrinsic(bytes[start]) : bytes[start];
        }

        public static object GetObjectFromBytes(byte[] source, int dataFilePosition, Type type, int length = 0, bool isIntrinsicValue = false)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return GetStringFromBytes(source, dataFilePosition, length);

                case TypeCode.Int32:
                    return GetIntFromBytes(source, dataFilePosition);

                case TypeCode.Int16:
                    return GetShortFromBytes(source, dataFilePosition);

                case TypeCode.DateTime:
                    return GetDateFromBytes(source, dataFilePosition);

                case TypeCode.Byte:
                    return GetByteFromBytes(source, dataFilePosition, isIntrinsicValue);

                case TypeCode.SByte:
                    return (sbyte)source[dataFilePosition];
            }

            return null;
        }

        public static List<byte[]> GetAllDataFromFile(DataFile dataFile, string fileName, int sizeOfData)
        {
            int numberOfRecords = dataFile.Length / sizeOfData;

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            br.BaseStream.Seek(dataFile.Position, SeekOrigin.Begin);

            List<byte[]> records = new List<byte[]>();

            for (int i = 0; i < numberOfRecords; i++)
            {
                byte[] buffer = new byte[sizeOfData];
                br.BaseStream.Read(buffer, 0, sizeOfData);
                records.Add(buffer);
            }

            return records;
        }

        private static byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }

        private static string GetStringFromBytesTerminated(byte[] bytes, int start, int length)
        {
            var result = textEncoding.GetString(TrimEnd(bytes.Skip(start).Take(length).ToArray()));
            return result;
        }

        private static byte ConvertIntrinsic(byte b)
        {
            if (b < 128)
            {
                return (byte)(128 + b);
            }
            else
            {
                return (byte)(b - 128);
            }
        }

        private static DateTime? ConvertToDate(byte[] bytes)
        {
            // are days starting from 0?
            var day = BitConverter.ToInt16(bytes, 0) + 1;
            var year = BitConverter.ToInt16(bytes, 2);
            if (year == 0 || day == 0)
            {
                return null;
            }

            var date = new DateTime(year, 1, 1).AddDays(day);
            return date;
        }
    }
}
