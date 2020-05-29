using CMScouter.UI.Converters;
using CMScouterFunctions;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
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
            SetRater(string.Empty);
        }

        public void SetRater(string rater)
        {
            if (string.IsNullOrEmpty(rater))
            {
                _playerRater = new DefaultRater();
            }
        }

        public List<Club> GetClubs()
        {
            return _savegame.Clubs.Values.ToList();
        }

        public List<Nation> GetAllNations()
        {
            return _savegame.Nations.Values.ToList();
        }

        public List<PlayerView> GetPlayerByPlayerId(List<int> playerIds)
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => playerIds.Contains(x._player.PlayerId));
            return ConstructPlayerByFilter(filter);
        }

        public List<PlayerView> GetPlayersBySecondName(string playerName)
        {
            List<int> surnameIds = _savegame.Surnames.Where(x => x.Value.StartsWith(playerName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Key).ToList();
            Func<Player, bool> filter = new Func<Player, bool>(x => surnameIds.Contains(x._staff.SecondNameId));
            return ConstructPlayerByFilter(filter);
        }

        public List<PlayerView> GetScoutResults(ScoutingRequest request)
        {
            List<Func<Player, bool>> filters = new List<Func<Player, bool>>();
            SearchFilterHelper filterHelper = new SearchFilterHelper(_savegame, _playerRater);

            filterHelper.CreateClubFilter(request, filters);
            filterHelper.CreatePositionFilter(request, filters);
            filterHelper.CreatePlayerBasedFilter(request, filters);
            filterHelper.CreateNationalityFilter(request, filters);
            filterHelper.CreateEUNationalityFilter(request, filters);
            filterHelper.CreateValueFilter(request, filters);
            filterHelper.CreateContractStatusFilter(request, filters);
            filterHelper.CreateAgeFilter(request, filters);

            var players = _savegame.Players;
            foreach (var filter in filters)
            {
                players = players.Where(x => filter(x)).ToList();
            }

            return ConstructPlayerByScoutingValueDesc(request.PlayerType, request.NumberOfResults, players);
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

        private List<PlayerView> ConstructPlayerByScoutingValueDesc(PlayerType? type, short numberOfResults, List<Player> preFilteredPlayers)
        {
            if (numberOfResults == 0)
            {
                numberOfResults = (short)Math.Min(100, preFilteredPlayers.Count);
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
