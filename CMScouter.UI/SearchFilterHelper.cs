﻿using CMScouterFunctions;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMScouter.UI
{
    internal class SearchFilterHelper
    {
        private SaveGameData _savegame;
        private IPlayerRater _playerRater;

        public SearchFilterHelper(SaveGameData SaveGame, IPlayerRater PlayerRater)
        {
            _savegame = SaveGame;
            _playerRater = PlayerRater;
        }

        public void CreatePlayerBasedFilter(ScoutingRequest request, List<Func<Player, bool>> filters)
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

        public List<Player> OrderByDataPoint(DP dataPoint)
        {
            switch (dataPoint)
            {
                case DP.Tackling:
                    return _savegame.Players.OrderByDescending(x => x._player.Tackling).ToList();

                case DP.Passing:
                    return _savegame.Players.OrderByDescending(x => x._player.Passing).ToList();

                case DP.OffTheBall:
                    return _savegame.Players.OrderByDescending(x => x._player.OffTheBall).ToList();

                case DP.Finishing:
                    return _savegame.Players.OrderByDescending(x => x._player.Finishing).ToList();

                default:
                    return new List<Player>();
            }
        }

        public void CreateClubFilter(ScoutingRequest request, List<Func<Player, bool>> filters)
        {
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
        }

        public void CreateNationalityFilter(ScoutingRequest request, List<Func<Player, bool>> filters)
        {
            if (request.Nationality == null)
            {
                return;
            }

            Func<Player, bool> filter = x => x._staff.NationId == request.Nationality;
            filters.Add(filter);
        }

        public void CreateAgeFilter(ScoutingRequest request, List<Func<Player, bool>> filters)
        {
            filters.Add(x => (request.MinAge == 0 || GetAge(x._staff.DOB) >= request.MinAge) && (request.MaxAge == 0 || GetAge(x._staff.DOB) <= request.MaxAge));
        }

        public void CreateEUNationalityFilter(ScoutingRequest request, List<Func<Player, bool>> filters)
        {
            filters.Add(x => !request.EUNationalityOnly || IsEUNationality(x._staff));
        }

        public void CreateValueFilter(ScoutingRequest request, List<Func<Player, bool>> filters)
        {
            filters.Add(x => (request.MinValue == null || x._staff.IsOverValue(request.MinValue.Value, _savegame.ValueMultiplier)) && (request.MaxValue == null || x._staff.IsUnderValue(request.MaxValue.Value, _savegame.ValueMultiplier)));
        }

        public void CreateContractStatusFilter(ScoutingRequest request, List<Func<Player, bool>> filters)
        {
            if (request.ContractStatus == null || request.ContractStatus.Value < 0)
            {
                return;
            }

            Func<Player, bool> filter = x => x._staff.ContractExpiryDate <= _savegame.GameDate.AddDays(30 * request.ContractStatus.Value);
            filters.Add(filter);
        }

        public void CreatePositionFilter(ScoutingRequest request, List<Func<Player, bool>> filters)
        {
            filters.Add(x => (request.PlayerType == null || _playerRater.PlaysPosition(request.PlayerType.Value, x._player)));
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
    }
}
