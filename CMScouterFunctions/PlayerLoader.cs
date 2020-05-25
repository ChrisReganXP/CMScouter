using CMScouterFunctions.Converters;
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
            Stopwatch watch = new Stopwatch();

            Dictionary<int, Club_Comp> clubcomps = GetDataFileClubCompetitionDictionary(savegame);

            watch.Start();
            Dictionary<int, string> firstnames = GetDataFileStringsDictionary(savegame, DataFileType.First_Names);
            watch.Stop();
            string timings = $"{firstnames.Count} records in {watch.ElapsedMilliseconds / 100}";

            watch.Start();
            Dictionary<int, string> secondNames = GetDataFileStringsDictionary(savegame, DataFileType.Second_Names);
            watch.Stop();
            timings = $"{secondNames.Count} records in {watch.ElapsedMilliseconds / 100}";


            watch.Start();
            Dictionary<int, string> commonNames = GetDataFileStringsDictionary(savegame, DataFileType.Common_Names);
            watch.Stop();
            timings = $"{commonNames.Count} records in {watch.ElapsedMilliseconds / 100}";


            watch.Start();
            List<Nation> duplicateNations = new List<Nation>();
            Dictionary<int, Nation> nations = GetDataFileNationDictionary(savegame, out duplicateNations); 
            //Dictionary<int, Nation> nations = GetDataFileConvertedIdDictionary<Nation>(savegame, DataFileType.Nations, out duplicateNations);
            watch.Stop();
            timings = $"{nations.Count} records in {watch.ElapsedMilliseconds / 100}";


            watch.Start();
            List<Club> duplicateClubs = new List<Club>();
            Dictionary<int, Club> clubs = GetDataFileClubDictionary(savegame, out duplicateClubs);
            //Dictionary<int, Club> clubs = GetDataFileConvertedIdDictionary<Club>(new ClubTupleConverter(), savegame, DataFileType.Clubs, out duplicateClubs);

            var englishClubs = clubs.Where(c => c.Value.NationId == 61);
            var englishDivisions = englishClubs.Select(x => x.Value.DivisionId).Distinct().ToList();

            var ManU = clubs.Values.FirstOrDefault(x => x.Name.StartsWith("Manchester United"));
            var Everton = clubs.Values.FirstOrDefault(x => x.Name.StartsWith("Everton"));
            var Chelsea = clubs.Values.FirstOrDefault(x => x.Name.StartsWith("Chelsea"));

            watch.Stop();
            timings = $"{clubs.Count} records in {watch.ElapsedMilliseconds / 100}";


            watch.Start();
            List<Staff> duplicates = new List<Staff>();
            Dictionary<int, Staff> staffDic = GetDataFileStaffDictionary(savegame, saveData, out duplicates);
            //Dictionary<int, Staff> staffDic = GetDataFileConvertedIdDictionary<Staff>(savegame, DataFileType.Staff, out duplicates);
            watch.Stop();
            timings = $"{staffDic.Count} records in {watch.ElapsedMilliseconds / 100}";

            var kloppName = secondNames.FirstOrDefault(x => x.Value == "Klopp").Key;
            var rafaname = firstnames.FirstOrDefault(x => x.Value == "Rafa").Key;
            var gerrardName = secondNames.FirstOrDefault(x => x.Value == "Gerrard").Key;

            var jurgen = staffDic.Values.FirstOrDefault(x => x.SecondNameId == kloppName);
            var rafa = staffDic.Values.FirstOrDefault(x => x.FirstNameId == rafaname);
            var gerrard = staffDic.Values.FirstOrDefault(x => x.SecondNameId == gerrardName);

            var fergieName = firstnames.FirstOrDefault(x => x.Value == "Sir Alex").Key;
            var fergie = staffDic.Values.FirstOrDefault(x => x.SecondNameId.Equals("Klopp"));
            //var keane = staffDic.Values.FirstOrDefault(x => x.SecondNameId.Equals("Klopp"));
            //var neville = staffDic.Values.FirstOrDefault(x => x.SecondNameId.Equals("Klopp"));

            watch.Start();
            List<PlayerData> players = GetDataFilePlayerData(savegame);
            //List<PlayerData> players = GetDataFileConverted<PlayerData>(savegame, DataFileType.Players);
            watch.Stop();
            timings = $"{firstnames.Count} records in {watch.ElapsedMilliseconds / 100}";


            List<Player> searchablePlayers = ConstructSearchablePlayers(staffDic, players).ToList();

            saveData.GameDate = savegame.GameDate;
            saveData.FirstNames = firstnames;
            saveData.Surnames = secondNames;
            saveData.CommonNames = commonNames;
            saveData.Nations = nations;
            saveData.Clubs = clubs;
            saveData.Players = searchablePlayers;

            return saveData;
        }

        private static Dictionary<int, Staff> GetDataFileStaffDictionary(SaveGameFile savegame, SaveGameData gameData, out List<Staff> duplicateStaff)
        {
            Dictionary<int, Staff> dic = new Dictionary<int, Staff>();
            duplicateStaff = new List<Staff>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Staff);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            StaffConverter converter = new StaffConverter();

            foreach (var item in bytes)
            {
                var staff = converter.Convert(item);
                staff.Value = staff.Value * gameData.ValueMultiplier;
                staff.Wage = staff.Wage * gameData.ValueMultiplier;

                if (staff.StaffPlayerId != -1)
                {
                    if (dic.ContainsKey(staff.StaffPlayerId))
                    {
                        duplicateStaff.Add(staff);
                    }
                    else
                    {
                        dic.Add(staff.StaffPlayerId, staff);
                    }
                }
            }

            return dic;
        }

        private static Dictionary<int, Club> GetDataFileClubDictionary(SaveGameFile savegame, out List<Club> duplicateClub)
        {
            Dictionary<int, Club> dic = new Dictionary<int, Club>();
            duplicateClub = new List<Club>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Clubs);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            ClubConverter converter = new ClubConverter();

            foreach (var item in bytes)
            {
                var club = converter.Convert(item);

                if (club.ClubId != -1)
                {
                    if (dic.ContainsKey(club.ClubId))
                    {
                        duplicateClub.Add(club);
                    }
                    else
                    {
                        dic.Add(club.ClubId, club);
                    }
                }
            }

            return dic;
        }

        private static Dictionary<int, Club_Comp> GetDataFileClubCompetitionDictionary(SaveGameFile savegame)
        {
            Dictionary<int, Club_Comp> dic = new Dictionary<int, Club_Comp>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Club_Comps);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            var converter = new ClubCompConverter();

            foreach (var item in bytes)
            {
                var comp = converter.Convert(item);
                dic.Add(comp.Id, comp);
            }

            return dic;
        }

        private static Dictionary<int, Nation> GetDataFileNationDictionary(SaveGameFile savegame, out List<Nation> duplicateNations)
        {
            Dictionary<int, Nation> dic = new Dictionary<int, Nation>();
            duplicateNations = new List<Nation>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Nations);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            NationConverter converter = new NationConverter();

            foreach (var item in bytes)
            {
                var nation = converter.Convert(item);

                if (nation.Id != -1)
                {
                    if (dic.ContainsKey(nation.Id))
                    {
                        duplicateNations.Add(nation);
                    }
                    else
                    {
                        dic.Add(nation.Id, nation);
                    }
                }
            }

            return dic;
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
            var fileData = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            for (int i = 0; i < fileData.Count; i++)
            {
                fileContents.Add(i, (string)ByteHandler.GetObjectFromBytes(fileData[i], 0, typeof(string), fileFacts.StringLength));
            }

            return fileContents;
        }

        private static Dictionary<int, T> GetDataFileConvertedIdDictionary<T>(ITupleConverter<T> converter, SaveGameFile savegame, DataFileType type, out List<T> duplicates) where T : class
        {
            duplicates = new List<T>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == type);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            Dictionary<int, T> dic = new Dictionary<int, T>();
            List<T> invalidIds = new List<T>();

            foreach (var item in bytes)
            {
                var converted = converter.Convert(item);

                if (converted.Item1 == -1)
                {
                    invalidIds.Add(converted.Item2 as T);
                }
                else
                {
                    if (dic.ContainsKey(converted.Item1))
                    {
                        duplicates.Add(converted.Item2 as T);
                    }
                    else
                    {
                        dic.Add(converted.Item1, converted.Item2 as T);
                    }
                }
            }

            return dic;
        }

        /*
        private static Dictionary<int, T> GetDataFileConvertedIdDictionary<T>(SaveGameFile savegame, DataFileType type, out List<T> duplicates) where T : class
        {
            var converter = ConverterIdFactory.CreateTupleConverter<T>();
            return GetDataFileConvertedIdDictionary(converter, savegame, type, out duplicates);
        }*/

        private static List<PlayerData> GetDataFilePlayerData(SaveGameFile savegame)
        {
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Players);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);
            var converter = new PlayerDataConverter();
            var collect = new List<PlayerData>();

            foreach (var source in bytes)
            {
                collect.Add(converter.Convert(source));
            }

            return collect;
        }

        private static List<T> GetDataFileConverted<T>(SaveGameFile savegame, DataFileType type)
        {
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == type);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);
            var converter = ConverterFactory.CreateConverter<T>();
            var collect = ConvertToCMObject<T>(bytes, converter).ToList();
            return collect;
        }

        private static IEnumerable<TOutput> ConvertToCMObject<TOutput>(List<byte[]> bytes, ICMConverter<TOutput> converter)
        {
            foreach (var data in bytes)
            {
                yield return converter.Convert(data);
            }
        }

        private static List<byte[]> GetDataFileBytes(SaveGameFile savegame, DataFileType fileType, int sizeOfData)
        {
            DataFile dataFile = savegame.DataBlockNameList.First(x => x.FileType == fileType);
            return ByteHandler.GetAllDataFromFile(dataFile, savegame.FileName, sizeOfData);
        }
    }
}
