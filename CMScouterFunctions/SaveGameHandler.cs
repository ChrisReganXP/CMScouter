﻿using CMScouterFunctions.Converters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CMScouterFunctions
{

    public static class SaveGameHandler
    {
        const int ByteBlockSize = 268;
        const int BitSize = 4;

        public static SaveGameData OpenSaveGameIntoMemory(string fileName)
        {
            SaveGameFile savegame = new SaveGameFile();
            savegame.FileName = fileName;

            using (var sr = new StreamReader(fileName))
            {
                ReadFileHeaders(sr, savegame);
            }

            LoadGameData(savegame);

            return PlayerLoader.LoadPlayers(savegame);
        }

        private static void ReadFileHeaders(StreamReader sr, SaveGameFile savegame)
        {
            using (var br = new BinaryReader(sr.BaseStream))
            {
                if (br.ReadInt32() == 4)
                {
                    savegame.IsCompressed = true;
                }

                sr.BaseStream.Seek(4, SeekOrigin.Current);

                var blockCount = br.ReadInt32();
                for (int j = 0; j < blockCount; j++)
                {
                    byte[] fileHeader = new byte[ByteBlockSize];
                    br.Read(fileHeader, 0, ByteBlockSize);
                    var internalName = ByteHandler.GetStringFromBytes(fileHeader, 8);

                    savegame.DataBlockNameList.Add(new DataFile() 
                        {
                            InternalName = internalName,
                            FileType = DataFileFacts.GetDataFileFact(internalName).Type, 
                            Position = ByteHandler.GetIntFromBytes(fileHeader, 0), 
                            Length = ByteHandler.GetIntFromBytes(fileHeader, 4) 
                        });
                }
            }
        }

        private static void LoadGameData(SaveGameFile savegame)
        {
            var general = savegame.DataBlockNameList.First(x => x.FileType == DataFileType.General);
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.General);

            ByteHandler.GetAllDataFromFile(general, savegame.FileName, fileFacts.DataSize);

            var fileData = ByteHandler.GetAllDataFromFile(general, savegame.FileName, fileFacts.DataSize);

            var days = (byte)ByteHandler.GetObjectFromBytes(fileData[0], fileFacts.DataSize - 8, typeof(byte));
            var time = (byte)ByteHandler.GetObjectFromBytes(fileData[0], fileFacts.DataSize - 7, typeof(byte));
            var year = (short)ByteHandler.GetObjectFromBytes(fileData[0], fileFacts.DataSize - 6, typeof(short));
            savegame.GameDate = new DateTime(year, 1, 1).AddDays(days);
        }

        public static List<int> GetCountriesInRegion(Dictionary<int, Nation> nations, string regionName)
        {
            List<int> countryIds = new List<int>();
            if (string.IsNullOrWhiteSpace(regionName))
            {
                return countryIds;
            }

            switch (regionName.ToUpper())
            {
                case "UK":
                case "UK & Ireland":
                    countryIds = GetUKCountries(nations);
                    break;

                case "Scandinavia":
                    countryIds = GetScandiCountries(nations);
                    break;
            }

            return countryIds;
        }

        private static List<int> GetUKCountries(Dictionary<int, Nation> nations)
        {
            List<string> countryNames = new List<string>() { "ENGLAND", "SCOTLAND", "WALES", "IRELAND", "REPUBLIC OF IRELAND", "N.IRELAND", "NORTHERN IRELAND" };
            return PopulateNationIds(countryNames, nations);
        }

        private static List<int> GetScandiCountries(Dictionary<int, Nation> nations)
        {
            List<string> countryNames = new List<string>() { "ICELAND", "FINLAND", "NORWAY", "SWEDEN", "DENMARK" };
            return PopulateNationIds(countryNames, nations);
        }

        private static List<int> PopulateNationIds(List<string> nationNames, Dictionary<int, Nation> nations)
        { 
            List<int> countryIds = new List<int>();

            foreach (var country in nationNames)
            {
                var id = nations.Values.FirstOrDefault(n => n.Name.Equals(country, StringComparison.InvariantCultureIgnoreCase))?.Id;
                if (id != null)
                {
                    countryIds.Add(id.Value);
                }
            }

            return countryIds;
        }
    }
}