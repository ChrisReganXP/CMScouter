using CMScouter.UI.Converters;
using CMScouterFunctions;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CMScouter.UI
{
    public class CMScouterUI
    {
        private SaveGameData _savegame;
        private PlayerDisplayHelper _displayHelper;
        private IPlayerRater _playerRater;

        public CMScouterUI(string fileName)
        {
            SaveGameData file = FileFunctions.LoadSaveGameFile(fileName);

            _savegame = file;

            ConstructLookups();
            SetFilter(string.Empty);
        }

        public void SetFilter(string rater)
        {
            if (string.IsNullOrEmpty(rater))
            {
                _playerRater = new DefaultRater();
            }
        }

        /*public List<PlayerView> GetAllPlayerData()
        {
            return _displayHelper.ConstructPlayers(_savegame.Players, _playerRater).ToList();
        }*/

        public List<PlayerView> GetPlayerByPlayerId(List<int> playerIds)
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => playerIds.Contains(x._player.PlayerId));
            return ConstructPlayerByFilter(filter);
        }

        public List<PlayerView> GetPlayersAtClub(string clubName)
        {
            List<int> clubIDs = _savegame.Clubs.Where(x => x.Value.Name.StartsWith(clubName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Key).ToList();
            Func<Player, bool> filter = new Func<Player, bool>(x => clubIDs.Contains(x._staff.ClubId));
            return ConstructPlayerByFilter(filter);
        }

        public List<PlayerView> GetPlayersBySecondName(string playerName)
        {
            List<int> surnameIds = _savegame.Surnames.Where(x => x.Value.StartsWith(playerName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Key).ToList();
            Func<Player, bool> filter = new Func<Player, bool>(x => surnameIds.Contains(x._staff.SecondNameId));
            return ConstructPlayerByFilter(filter);
        }

        public List<PlayerView> GetPlayersByNationality(string nationality)
        {
            List<int> nationIds = _savegame.Nations.Where(x => x.Value.Name.StartsWith(nationality, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Key).ToList();
            Func<Player, bool> filter = new Func<Player, bool>(x => nationIds.Contains(x._staff.NationId) || nationIds.Contains(x._staff.SecondaryNationId));
            return ConstructPlayerByFilter(filter);
        }

        public List<PlayerView> GetHighestAbilityGKs(short numberOfResults)
        {
            var keepers = ApplyFilterToPlayerList(new Func<Player, bool>(x => x._player.GK == 20));
            return ConstructPlayerByPlayerValueDesc(typeof(PlayerData).GetProperty("CurrentAbility"), numberOfResults, keepers.ToList());
        }

        public List<PlayerView> GetBestFreeTransfers(short numberOfResults)
        {
            var players = ApplyFilterToPlayerList(new Func<Player, bool>(x => x._staff.ClubId == -1));
            return ConstructPlayerByPlayerValueDesc(typeof(PlayerData).GetProperty("CurrentAbility"), numberOfResults, players.ToList());
        }

        public List<PlayerView> GetYouthProspects(short numberOfResults, bool EUNation = false)
        {
            var players = ApplyFilterToPlayerList(new Func<Player, bool>(x => x._staff.ClubId == -1 && GetAge(x._staff.DOB) <= 21
                && (!EUNation || IsEUNationality(x._staff))));
            return ConstructPlayerByPlayerValueDesc(typeof(PlayerData).GetProperty("CurrentAbility"), numberOfResults, players.ToList());
        }

        public List<PlayerView> GetScoutResults(PlayerType type, int maxValue, short numberOfResults, bool EUNation = false)
        {
            var positionalPlayers = ApplyFilterToPlayerList(new Func<Player, bool>(x => _playerRater.PlaysPosition(type, x._player))).ToList();
            var valuedPlayers = ApplyFilterToPlayerList(new Func<Player, bool>(x => x._staff.IsUnderValue(maxValue, _savegame.ValueMultiplier)), positionalPlayers).ToList();
            var nationalityPlayers = ApplyFilterToPlayerList(new Func<Player, bool>(x => !EUNation || IsEUNationality(x._staff)), valuedPlayers);
            return ConstructPlayerByScoutingValueDesc(type, numberOfResults, nationalityPlayers.ToList());
        }

        private void CreatePlayerBasedFilter(ScoutingRequest request, List<Func<Player, bool>> filters)
        {
            List<int> countryClubs = null;
            Func<Player, bool> filter = null;

            if (!string.IsNullOrWhiteSpace(request.PlaysInRegion))
            {
                List<int> regionCountryIds = SaveGameHandler.GetCountriesInRegion(_savegame.Nations, request.PlaysInRegion);
                if (regionCountryIds?.Count > 0)
                {
                    countryClubs = _savegame.Clubs.Where(x => regionCountryIds.Contains(x.Value.NationId)).Select(x => x.Key).ToList();

                    filter = x => countryClubs.Contains(x._staff.ClubId) || (x._staff.ClubId == -1 && regionCountryIds.Contains(x._staff.NationId));
                    filters.Add(filter);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(request.PlaysInCountry))
            {
                int? playsInNationId = _savegame.Nations.Values.FirstOrDefault(x => x.Name.Equals(request.PlaysInCountry, StringComparison.InvariantCultureIgnoreCase))?.Id;

                if (playsInNationId != null)
                {
                    countryClubs = _savegame.Clubs.Where(x => x.Value.NationId == playsInNationId.Value).Select(x => x.Key).ToList();

                    if (countryClubs?.Count > 0)
                    {
                        filter = x => countryClubs.Contains(x._staff.ClubId) || (x._staff.ClubId == -1 && x._staff.NationId == playsInNationId.Value);
                        filters.Add(filter);
                        return;
                    }
                }
            }
        }

        public List<PlayerView> GetScoutResults(ScoutingRequest request)
        {
            List<Func<Player, bool>> filters = new List<Func<Player, bool>>();

            int? clubId = null;

            if (!string.IsNullOrWhiteSpace(request.ClubName))
            {
                clubId = _savegame.Clubs.Values.FirstOrDefault(x => x.Name.Equals(request.ClubName, StringComparison.OrdinalIgnoreCase))?.ClubId;
                if (clubId == null)
                {
                    clubId = -2;
                }
            }

            filters.Add(x => (clubId == null || x._staff.ClubId == clubId));

            CreatePlayerBasedFilter(request, filters);
            filters.Add(x => (request.MinAge == 0 || GetAge(x._staff.DOB) >= request.MinAge) && (request.MaxAge == 0 || GetAge(x._staff.DOB) <= request.MaxAge));
            filters.Add(x => !request.EUNationalityOnly || IsEUNationality(x._staff));
            filters.Add(x => (request.MinValue == 0 || x._staff.IsOverValue(request.MinValue, _savegame.ValueMultiplier)) && (request.MaxValue == 0 || x._staff.IsUnderValue(request.MaxValue, _savegame.ValueMultiplier)));
            filters.Add(x => (request.PlayerType == null || _playerRater.PlaysPosition(request.PlayerType.Value, x._player)));

            var players = _savegame.Players;
            foreach (var filter in filters)
            {
                players = players.Where(x => filter(x)).ToList();
            }

            return ConstructPlayerByScoutingValueDesc(request.PlayerType, request.NumberOfResults, players);
        }

        private byte GetAge(DateTime date)
        {
            var age = _savegame.GameDate.Year - date.Year;

            // leap years
            if (date.Date > _savegame.GameDate.AddYears(-age)) age--;

            return (byte)Math.Min(byte.MaxValue, age);
        }

        private bool IsEUNationality(Staff staff)
        {
            List<int> EUNations = _savegame.Nations.Where(x => x.Value.EUNation).Select(x => x.Key).ToList();
            return EUNations.Contains(staff.NationId) || EUNations.Contains(staff.SecondaryNationId);
        }

        private int GetNationId(string nationName)
        {
            var nation = _savegame.Nations.FirstOrDefault(x => x.Value.Name.Equals(nationName, StringComparison.InvariantCultureIgnoreCase));
            if (nation.Value == null)
            {
                return -1;
            }

            return nation.Key;
        }

        private List<PlayerView> ConstructPlayerByFilter(Func<Player, bool> filter)
        {
            return _displayHelper.ConstructPlayers(ApplyFilterToPlayerList(filter), _playerRater).ToList();
        }

        private IEnumerable<Player> ApplyFilterToPlayerList(Func<Player, bool> filter, List<Player> specificPlayerList = null)
        {
            if (specificPlayerList == null)
            {
                specificPlayerList = _savegame.Players;
            }

            return specificPlayerList.Where(x => filter(x));
        }

        private List<PlayerView> ConstructPlayerByPlayerValueDesc(PropertyInfo prop, short numberOfResults, List<Player> preFilteredPlayers)
        {
            var playersToConstruct = preFilteredPlayers ?? _savegame.Players;
            return _displayHelper.ConstructPlayers(playersToConstruct.OrderByDescending(x => prop.GetValue(x._player)).Take(numberOfResults), _playerRater).ToList();
        }

        private List<PlayerView> ConstructPlayerByScoutingValueDesc(PlayerType? type, short numberOfResults, List<Player> preFilteredPlayers)
        {
            if (numberOfResults == 0)
            {
                numberOfResults = (short)Math.Min(5000, preFilteredPlayers.Count);
            }

            var playersToConstruct = preFilteredPlayers ?? _savegame.Players;
            var list = _displayHelper.ConstructPlayers(playersToConstruct, _playerRater).ToList();
            return ScoutingOrdering(list, type).Take(numberOfResults).ToList();
        }

        private IEnumerable<PlayerView> ScoutingOrdering(IEnumerable<PlayerView> list, PlayerType? type)
        {
            if (type == null)
            {
                return list.OrderByDescending(x => x.ScoutRatings.BestPosition.BestRole().Rating);
            }
            
            switch (type)
            {
                case PlayerType.GoalKeeper:
                    return list.OrderByDescending(x => x.ScoutRatings.Goalkeeper.BestRole().Rating);

                case PlayerType.RightBack:
                    return list.OrderByDescending(x => x.ScoutRatings.RightBack.BestRole().Rating);

                case PlayerType.LeftBack:
                    return list.OrderByDescending(x => x.ScoutRatings.LeftBack.BestRole().Rating);

                case PlayerType.CentreHalf:
                    return list.OrderByDescending(x => x.ScoutRatings.CentreHalf.BestRole().Rating);

                case PlayerType.RightWingBack:
                    return list.OrderByDescending(x => x.ScoutRatings.RightWingBack.BestRole().Rating);

                case PlayerType.DefensiveMidfielder:
                    return list.OrderByDescending(x => x.ScoutRatings.DefensiveMidfielder.BestRole().Rating);

                case PlayerType.LeftWingBack:
                    return list.OrderByDescending(x => x.ScoutRatings.LeftWingBack.BestRole().Rating);

                case PlayerType.RightMidfielder:
                    return list.OrderByDescending(x => x.ScoutRatings.RightMidfielder.BestRole().Rating);

                case PlayerType.CentralMidfielder:
                    return list.OrderByDescending(x => x.ScoutRatings.CentreMidfielder.BestRole().Rating);

                case PlayerType.LeftMidfielder:
                    return list.OrderByDescending(x => x.ScoutRatings.LeftMidfielder.BestRole().Rating);

                case PlayerType.RightWinger:
                    return list.OrderByDescending(x => x.ScoutRatings.RightWinger.BestRole().Rating);

                case PlayerType.AttackingMidfielder:
                    return list.OrderByDescending(x => x.ScoutRatings.AttackingMidfielder.BestRole().Rating);

                case PlayerType.LeftWinger:
                    return list.OrderByDescending(x => x.ScoutRatings.LeftWinger.BestRole().Rating);

                case PlayerType.CentreForward:
                    return list.OrderByDescending(x => x.ScoutRatings.CentreForward.BestRole().Rating);

                default:
                    return list;
            }
        }

        private void ConstructLookups()
        {
            Lookups lookups = new Lookups();
            lookups.clubNames = _savegame.Clubs.Values.ToDictionary(x => x.ClubId, x => x.Name);
            lookups.firstNames = _savegame.FirstNames;
            lookups.secondNames = _savegame.Surnames;
            lookups.commonNames = _savegame.CommonNames;
            lookups.nations = NationConverter.ConvertNations(_savegame.Nations);
            _displayHelper = new PlayerDisplayHelper(lookups, _savegame.GameDate);
        }
    }
}
