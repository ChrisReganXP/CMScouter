﻿using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CMScouterFunctions.Converters
{

    internal class NationConverter
    {
        private static readonly List<string> EUNations = new List<string>() {
            "AUSTRIA", "BELGIUM", "BULGARIA", "CROATIA", "CYRPUS", "CZECH REPUBLIC", "DENMARK", "ENGLAND", "ESTONIA", "SPAIN", "FINLAND", "FRANCE",
            "GERMANY", "GREECE", "HUNGARY", "REPUBLIC OF IRELAND", "ICELAND", "ITALY", "LATVIA", "LITHUANIA", "LUXEMBOURG", "MALTA", "NETHERLANDS", "NORTHERN IRELAND",
            "NORWAY", "POLAND", "PORTUGAL", "ROMANIA", "SCOTLAND", "SLOVAKIA", "SWITZERLAND", "SLOVENIA", "SWEDEN", "WALES"
        };

        public Nation Convert(byte[] source)
        {
            var nation = new Nation();

            nation.Id = ByteHandler.GetIntFromBytes(source, 0);
            nation.Name = ByteHandler.GetStringFromBytes(source, 4, 50);
            nation.EUNation = EUNations.Contains(nation.Name, StringComparer.InvariantCultureIgnoreCase);

            return nation;
        }
    }

    internal class StaffConverter
    {
        public Staff Convert(byte[] source)
        {
            var staff = new Staff();

            staff.StaffId = ByteHandler.GetIntFromBytes(source, 0);
            staff.StaffPlayerId = ByteHandler.GetIntFromBytes(source, 97);
            staff.FirstNameId = ByteHandler.GetIntFromBytes(source, 4);
            staff.SecondNameId = ByteHandler.GetIntFromBytes(source, 8);
            staff.CommonNameId = ByteHandler.GetIntFromBytes(source, 12);
            staff.DOB = ByteHandler.GetDateFromBytes(source, 16) ?? new DateTime(1985, 1, 1);
            staff.NationId = ByteHandler.GetIntFromBytes(source, 26);
            staff.SecondaryNationId = ByteHandler.GetIntFromBytes(source, 30);
            staff.InternationalCaps = ByteHandler.GetByteFromBytes(source, 24);
            staff.InternationalGoals = ByteHandler.GetByteFromBytes(source, 25);
            staff.ContractExpiryDate = ByteHandler.GetDateFromBytes(source, 68);
            staff.Wage = ByteHandler.GetIntFromBytes(source, 78);
            staff.Value = ByteHandler.GetIntFromBytes(source, 82);
            staff.ClubId = ByteHandler.GetIntFromBytes(source, 57);
            
            staff.Adaptability = ByteHandler.GetByteFromBytes(source, 86);
            staff.Ambition = ByteHandler.GetByteFromBytes(source, 87);
            staff.Determination = ByteHandler.GetByteFromBytes(source, 88);
            staff.Loyalty = ByteHandler.GetByteFromBytes(source, 89);
            staff.Pressure = ByteHandler.GetByteFromBytes(source, 90);
            staff.Professionalism = ByteHandler.GetByteFromBytes(source, 91);
            staff.Sportsmanship = ByteHandler.GetByteFromBytes(source, 92);
            staff.Temperament = ByteHandler.GetByteFromBytes(source, 93);
            
            return staff;
        }
    }

    internal class ClubConverter
    {
        public Club Convert(byte[] source)
        {
            var club = new Club();

            club.ClubId = ByteHandler.GetIntFromBytes(source, 0);
            club.LongName = ByteHandler.GetStringFromBytes(source, 4, 50);
            club.Name = ByteHandler.GetStringFromBytes(source, 56, 25);
            club.NationId = ByteHandler.GetIntFromBytes(source, 83);
            club.DivisionId = ByteHandler.GetIntFromBytes(source, 87);

            return club;
        }
    }

    internal class ClubCompConverter
    {
        public Club_Comp Convert(byte[] source)
        {
            var comp = new Club_Comp();

            comp.Id = ByteHandler.GetIntFromBytes(source, 0);
            comp.LongName = ByteHandler.GetStringFromBytes(source, 4, 50);
            comp.Name = ByteHandler.GetStringFromBytes(source, 56, 25);
            //comp.Reputation = ByteHandler.GetByteFromBytes(source, 82);
            comp.NationId = ByteHandler.GetIntFromBytes(source, 92);
            comp.Abbreviation = ByteHandler.GetStringFromBytes(source, 83, 3);

            return comp;
        }
    }

    internal class PlayerDataConverter
    {
        public PlayerData Convert(byte[] source)
        {
            var player = new PlayerData();

            player.PlayerId = ByteHandler.GetIntFromBytes(source, 0);
            player.CurrentAbility = ByteHandler.GetShortFromBytes(source, 5);
            player.PotentialAbility = ByteHandler.GetShortFromBytes(source, 7);
            player.GK = ByteHandler.GetByteFromBytes(source, 15);
            player.SW = ByteHandler.GetByteFromBytes(source, 16);
            player.DF = ByteHandler.GetByteFromBytes(source, 17);
            player.DM = ByteHandler.GetByteFromBytes(source, 18);
            player.MF = ByteHandler.GetByteFromBytes(source, 19);
            player.AM = ByteHandler.GetByteFromBytes(source, 20);
            player.ST = ByteHandler.GetByteFromBytes(source, 21);
            player.WingBack = ByteHandler.GetByteFromBytes(source, 22);
            player.Left = ByteHandler.GetByteFromBytes(source, 24);
            player.Right = ByteHandler.GetByteFromBytes(source, 23);
            player.Centre = ByteHandler.GetByteFromBytes(source, 25);
            player.FreeRole = ByteHandler.GetByteFromBytes(source, 26);
            player.Acceleration = ByteHandler.GetByteFromBytes(source, 27);
            player.Aggression = ByteHandler.GetByteFromBytes(source, 28);
            player.Agility = ByteHandler.GetByteFromBytes(source, 29);
            player.Anticipation = ByteHandler.GetByteFromBytes(source, 30, true);
            player.Balance = ByteHandler.GetByteFromBytes(source, 31);
            player.Bravery = ByteHandler.GetByteFromBytes(source, 32);
            player.Consistency = ByteHandler.GetByteFromBytes(source, 33);
            player.Corners = ByteHandler.GetByteFromBytes(source, 34);
            player.Creativity = ByteHandler.GetByteFromBytes(source, 67, true);
            player.Crossing = ByteHandler.GetByteFromBytes(source, 35, true);
            player.Decisions = ByteHandler.GetByteFromBytes(source, 36, true);
            player.Dirtiness = ByteHandler.GetByteFromBytes(source, 37);
            player.Dribbling = ByteHandler.GetByteFromBytes(source, 38, true);
            player.Finishing = ByteHandler.GetByteFromBytes(source, 39, true);
            player.Flair = ByteHandler.GetByteFromBytes(source, 40);
            player.FreeKicks = ByteHandler.GetByteFromBytes(source, 41);
            player.Handling = ByteHandler.GetByteFromBytes(source, 42, true);
            player.Heading = ByteHandler.GetByteFromBytes(source, 43, true);
            player.ImportantMatches = ByteHandler.GetByteFromBytes(source, 44);
            player.Influence = ByteHandler.GetByteFromBytes(source, 47);
            player.InjuryProneness = ByteHandler.GetByteFromBytes(source, 45);
            player.Jumping = ByteHandler.GetByteFromBytes(source, 46);
            player.LongShots = ByteHandler.GetByteFromBytes(source, 49, true);
            player.Marking = ByteHandler.GetByteFromBytes(source, 50, true);
            player.NaturalFitness = ByteHandler.GetByteFromBytes(source, 52);
            player.OffTheBall = ByteHandler.GetByteFromBytes(source, 51, true);
            player.OneOnOnes = ByteHandler.GetByteFromBytes(source, 53, true);
            player.Pace = ByteHandler.GetByteFromBytes(source, 54);
            player.Passing = ByteHandler.GetByteFromBytes(source, 55, true);
            player.Penalties = ByteHandler.GetByteFromBytes(source, 56, true);
            player.Positioning = ByteHandler.GetByteFromBytes(source, 57, true);
            player.Reflexes = ByteHandler.GetByteFromBytes(source, 58, true);
            player.Stamina = ByteHandler.GetByteFromBytes(source, 60);
            player.Strength = ByteHandler.GetByteFromBytes(source, 61);
            player.Tackling = ByteHandler.GetByteFromBytes(source, 62, true);
            player.Teamwork = ByteHandler.GetByteFromBytes(source, 63);
            player.Technique = ByteHandler.GetByteFromBytes(source, 64);
            player.ThrowIns = ByteHandler.GetByteFromBytes(source, 65, true);
            player.Versatility = ByteHandler.GetByteFromBytes(source, 66);
            player.WorkRate = ByteHandler.GetByteFromBytes(source, 68);

            return player;
        }
    }

    internal class PlayerConverter : ICMConverter<PlayerData>
    {
        public PlayerData Convert(byte[] source)
        {
            var player = new PlayerData();
            ConverterReflection.SetConversionProperties(player, source);
            return player;
        }
    }
    internal static class ConverterReflection
    {
        public static void SetConversionProperties(object target, byte[] source)
        {
            foreach (var prop in target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var positionAttribute = (DataFileInfoAttribute)prop.GetCustomAttributes(typeof(DataFileInfoAttribute), true).FirstOrDefault();

                if (positionAttribute != null)
                {
                    prop.SetValue(target, ByteHandler.GetObjectFromBytes(source, positionAttribute.DataFilePosition, prop.PropertyType, positionAttribute.Length, positionAttribute.IsIntrinsic));
                }
            }
        }
        public static void SetConversionProperties(object target, PropertyInfo[] props, DataFileInfoAttribute[] attribs, byte[] source)
        {
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var positionAttribute = attribs[i];

                if (prop != null && positionAttribute != null)
                {
                    prop.SetValue(target, ByteHandler.GetObjectFromBytes(source, positionAttribute.DataFilePosition, prop.PropertyType, positionAttribute.Length, positionAttribute.IsIntrinsic));
                }
            }
        }
    }
}
