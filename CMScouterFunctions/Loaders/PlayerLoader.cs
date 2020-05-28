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
    internal static class PlayerLoader
    {
        public static SaveGameData LoadPlayers(SaveGameFile savegame)
        {
            SaveGameData saveData = new SaveGameData();

            Dictionary<int, Club_Comp> clubcomps = DataFileLoaders.GetDataFileClubCompetitionDictionary(savegame);

            Dictionary<int, string> firstnames = GetDataFileStringsDictionary(savegame, DataFileType.First_Names);

            Dictionary<int, string> secondNames = GetDataFileStringsDictionary(savegame, DataFileType.Second_Names);

            Dictionary<int, string> commonNames = GetDataFileStringsDictionary(savegame, DataFileType.Common_Names);

            Dictionary<int, Nation> nations = DataFileLoaders.GetDataFileNationDictionary(savegame); 
            
            Dictionary<int, Club> clubs = DataFileLoaders.GetDataFileClubDictionary(savegame);

            List<Staff> duplicates = new List<Staff>();
            Dictionary<int, Staff> staffDic = DataFileLoaders.GetDataFileStaffDictionary(savegame, saveData, out duplicates);

            List<PlayerData> players = GetDataFilePlayerData(savegame);

            List<Player> searchablePlayers = ConstructSearchablePlayers(staffDic, players).ToList();

            saveData.GameDate = savegame.GameDate;
            saveData.FirstNames = firstnames;
            saveData.Surnames = secondNames;
            saveData.CommonNames = commonNames;
            saveData.Nations = nations;
            saveData.Clubs = clubs;
            saveData.Players = searchablePlayers;
            saveData.ClubComps = clubcomps;

            return saveData;
        }


        private static IEnumerable<Player> ConstructSearchablePlayers(Dictionary<int, Staff> staffDic, List<PlayerData> players)
        {
            foreach (var player in players)
            {
                if (staffDic.ContainsKey(player.PlayerId))
                {
                    yield return new Player(player, staffDic[player.PlayerId]);
                }
            }
        }

        private static Dictionary<int, string> GetDataFileStringsDictionary(SaveGameFile savegame, DataFileType type)
        {
            Dictionary<int, string> fileContents = new Dictionary<int, string>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == type);
            var fileData = DataFileLoaders.GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            for (int i = 0; i < fileData.Count; i++)
            {
                fileContents.Add(i, ByteHandler.GetStringFromBytes(fileData[i], 0, fileFacts.StringLength));
            }

            return fileContents;
        }

        private static List<PlayerData> GetDataFilePlayerData(SaveGameFile savegame)
        {
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Players);
            var bytes = DataFileLoaders.GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);
            var converter = new PlayerDataConverter();
            var collect = new List<PlayerData>();

            foreach (var source in bytes)
            {
                collect.Add(converter.Convert(source));
            }

            return collect;
        }
    }
}
