﻿using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CMScouter.UI
{
    internal class InvestigationRater : IPlayerRater
    {
        /*
            GK: Anticipation, Decisions, Handling, Heading, Positioning, Reflexes, Tackling, One on Ones
            Full backs: Anticipation, Crossing, Decisions, Dribbling, Marking, Positioning, Tackling, Team work
            Centre Backs: Anticipation, Decisions, Heading, Marking, Positioning, Tackling, Strength
            Wingers: Agility, Balance, Crossing, Dribbling, Flair(If you please), Set pieces(If you please), Passing, Teamwork
            Centre Mids: Have pretty much everything, but can vary if you want a more defensive or more attacking player
            Strikers: Decisions, Finishing, Heading (depending on the striker), Movement (Depending on the striker) Natural Fitness, Strength (depending on the striker).
        */

        byte[][] weightings;

        private byte[] MentalAttributes = new byte[] {  (byte)DP.Aggression, (byte)DP.Bravery, (byte)DP.Consistency, (byte)DP.ImportantMatches, (byte)DP.Influence, (byte)DP.Teamwork, (byte)DP.WorkRate};
        private byte[] PhysicalAttributes = new byte[] { (byte)DP.Acceleration, (byte)DP.Agility, (byte)DP.Balance, (byte)DP.Jumping, (byte)DP.Pace, (byte)DP.Stamina, (byte)DP.Strength };
        private byte[] TechnicalAttributes = new byte[] { (byte)DP.Anticipation, (byte)DP.Creativity, (byte)DP.Crossing, (byte)DP.Decisions, (byte)DP.Dribbling, (byte)DP.Handling, (byte)DP.LongShots, (byte)DP.Marking, (byte)DP.OffTheBall, (byte)DP.OneOnOnes, (byte)DP.Passing, (byte)DP.Positioning, (byte)DP.Reflexes, (byte)DP.Tackling };
        private byte[] OffFieldAttributes = new byte[] { (byte)DP.Adaptability, (byte)DP.Ambition, (byte)DP.Determination, (byte)DP.Loyalty, (byte)DP.Pressure, (byte)DP.Professionalism, (byte)DP.Sportsmanship, (byte)DP.Temperament, (byte)DP.Versatility };

        private IIntrinsicMasker masker;

        public InvestigationRater(IIntrinsicMasker Masker)
        {
            masker = Masker;

            // last one is the off field
            weightings = new byte[Enum.GetNames(typeof(Roles)).Count() + 1][];
            AddGK();
            AddDFB();
            AddAFB();
            AddCB();
            AddDM();
            AddWB();
            AddCM();
            AddWM();
            AddAM();
            AddWG();
            AddPO();
            AddTM();
            AddST();
            AddCF();
            AddOffField();
        }

        public bool PlaysPosition(PlayerType type, PlayerData player)
        {
            switch (type)
            {
                case PlayerType.GoalKeeper:
                    return player.GK >= 19;

                case PlayerType.RightBack:
                    return player.DF >= 15 && player.Right >= 15;

                case PlayerType.CentreHalf:
                    return player.DF >= 15 && player.Centre >= 15;

                case PlayerType.LeftBack:
                    return player.DF >= 15 && player.Left >= 15;

                case PlayerType.RightWingBack:
                    return (player.WingBack >= 15 && player.Right >= 15) || (player.DF == 20 && player.Right == 20);

                case PlayerType.DefensiveMidfielder:
                    return (player.DM >= 15 && player.Centre >= 15) || (player.Centre == 20 && (player.DF == 20 || player.MF == 20));

                case PlayerType.LeftWingBack:
                    return (player.WingBack >= 15 && player.Left >= 15) || (player.DF == 20 && player.Left == 20);

                case PlayerType.RightMidfielder:
                    return (player.MF >= 15 && player.Right >= 15) || (player.Right == 20 && player.AM == 20);

                case PlayerType.CentralMidfielder:
                    return (player.MF >= 15 && player.Centre >= 15) ||
                        (player.DM == 20 && player.Centre >= 15) || (player.AM == 20 && player.Centre == 20);

                case PlayerType.LeftMidfielder:
                    return (player.MF >= 15 && player.Left >= 15) || (player.Left == 20 && player.AM == 20);

                case PlayerType.RightWinger:
                    return (player.AM >= 15 && player.Right >= 15) || (player.Right == 20 && (player.MF == 20 || player.ST == 20));

                case PlayerType.AttackingMidfielder:
                    return (player.AM >= 15 && player.Centre >= 15) || (player.Centre == 20 && (player.MF == 20 || player.ST == 20));

                case PlayerType.LeftWinger:
                    return (player.AM >= 15 && player.Left >= 15) || (player.Left == 20 && (player.MF == 20 || player.ST == 20));

                case PlayerType.CentreForward:
                    return (player.ST >= 15 && player.Centre >= 15) || (player.ST == 20) || (player.AM == 20 && player.Centre == 20);

                default:
                    return false;
            }
        }

        public RatingResults GetRatings(Player player)
        {
            byte offFieldRating = GetRatingsForPersonality(player);
            decimal offFieldAdjustment = GetAdjustmentByOffFieldRating(offFieldRating);

            RatingResults results = new RatingResults();
            results.Goalkeeper = GetRatingsForPosition(player, PlayerType.GoalKeeper, offFieldAdjustment);
            results.RightBack = GetRatingsForPosition(player, PlayerType.RightBack, offFieldAdjustment);
            results.CentreHalf = GetRatingsForPosition(player, PlayerType.CentreHalf, offFieldAdjustment);
            results.LeftBack = GetRatingsForPosition(player, PlayerType.LeftBack, offFieldAdjustment);
            results.RightWingBack = GetRatingsForPosition(player, PlayerType.RightWingBack, offFieldAdjustment);
            results.DefensiveMidfielder = GetRatingsForPosition(player, PlayerType.DefensiveMidfielder, offFieldAdjustment);
            results.LeftWingBack = GetRatingsForPosition(player, PlayerType.LeftWingBack, offFieldAdjustment);
            results.RightMidfielder = GetRatingsForPosition(player, PlayerType.RightMidfielder, offFieldAdjustment);
            results.CentreMidfielder = GetRatingsForPosition(player, PlayerType.CentralMidfielder, offFieldAdjustment);
            results.LeftMidfielder = GetRatingsForPosition(player, PlayerType.LeftMidfielder, offFieldAdjustment);
            results.RightWinger = GetRatingsForPosition(player, PlayerType.RightWinger, offFieldAdjustment);
            results.AttackingMidfielder = GetRatingsForPosition(player, PlayerType.AttackingMidfielder, offFieldAdjustment);
            results.LeftWinger = GetRatingsForPosition(player, PlayerType.LeftWinger, offFieldAdjustment);
            results.CentreForward = GetRatingsForPosition(player, PlayerType.CentreForward, offFieldAdjustment);

            return results;
        }

        private byte[] GetValues(Player player)
        {
            byte[] values = new byte[Enum.GetNames(typeof(DP)).Length];
            values.AW(DP.Acceleration, player._player.Acceleration);
            values.AW(DP.Aggression, player._player.Aggression);
            values.AW(DP.Agility, player._player.Agility);
            values.AW(DP.Anticipation, player._player.Anticipation);
            values.AW(DP.Balance, player._player.Balance);
            values.AW(DP.Bravery, player._player.Bravery);
            values.AW(DP.Consistency, player._player.Consistency);
            values.AW(DP.Corners, player._player.Corners);
            values.AW(DP.Creativity, player._player.Creativity);
            values.AW(DP.Crossing, player._player.Crossing);
            values.AW(DP.Decisions, player._player.Decisions);
            values.AW(DP.Dirtiness, player._player.Dirtiness);
            values.AW(DP.Dribbling, player._player.Dribbling);
            values.AW(DP.Finishing, player._player.Finishing);
            values.AW(DP.FreeKicks, player._player.FreeKicks);
            values.AW(DP.Flair, player._player.Flair);
            values.AW(DP.Handling, player._player.Handling);
            values.AW(DP.Heading, player._player.Heading);
            values.AW(DP.ImportantMatches, player._player.ImportantMatches);
            values.AW(DP.Influence, player._player.Influence);
            values.AW(DP.InjuryProneness, player._player.InjuryProneness);
            values.AW(DP.Jumping, player._player.Jumping);
            values.AW(DP.LongShots, player._player.LongShots);
            values.AW(DP.Marking, player._player.Marking);
            values.AW(DP.OffTheBall, player._player.OffTheBall);
            values.AW(DP.OneOnOnes, player._player.OneOnOnes);
            values.AW(DP.Pace, player._player.Pace);
            values.AW(DP.Passing, player._player.Passing);
            values.AW(DP.Penalties, player._player.Penalties);
            values.AW(DP.Positioning, player._player.Positioning);
            values.AW(DP.Reflexes, player._player.Reflexes);
            values.AW(DP.Stamina, player._player.Stamina);
            values.AW(DP.Strength, player._player.Strength);
            values.AW(DP.Tackling, player._player.Tackling);
            values.AW(DP.Teamwork, player._player.Teamwork);
            values.AW(DP.Technique, player._player.Technique);
            values.AW(DP.ThrowIns, player._player.ThrowIns);
            values.AW(DP.Versatility, player._player.Versatility);
            values.AW(DP.WorkRate, player._player.WorkRate);
            values.AW(DP.LeftFoot, player._player.Left);
            values.AW(DP.RightFoot, player._player.Right);

            values.AW(DP.Adaptability, player._staff.Adaptability);
            values.AW(DP.Ambition, player._staff.Ambition);
            values.AW(DP.Determination, player._staff.Determination);
            values.AW(DP.Loyalty, player._staff.Loyalty);
            values.AW(DP.Pressure, player._staff.Pressure);
            values.AW(DP.Professionalism, player._staff.Professionalism);
            values.AW(DP.Sportsmanship, player._staff.Sportsmanship);
            values.AW(DP.Temperament, player._staff.Temperament);

            return values;
        }

        private PositionRatings GetRatingsForPosition(Player player, PlayerType type, decimal offFieldRating)
        {
            PositionRatings ratings = new PositionRatings() { Position = type } ;

            List<Roles> roles = type.GetAttributeValue<LinkedRoles, List<Roles>>(x => x.Roles);
            foreach (var role in roles)
            {
                ratings.Ratings.Add(GetRatingForTypeAndRole(player, type, role, offFieldRating));
            }

            return ratings;
        }

        private PositionRating GetRatingForTypeAndRole(Player player, PlayerType type, Roles role, decimal offFieldAdjustment)
        {
            RatingRoleDebug roleDebug = new RatingRoleDebug();
            var attributeRating = CalculateRating(player, type, role, ref roleDebug);
            var adjustedForPosition = AdjustScoreForPosition(player, type, attributeRating, roleDebug);
            var adjustedForOffField = AdjustScoreForOffField(adjustedForPosition, offFieldAdjustment, roleDebug);

            roleDebug.OffField = offFieldAdjustment.ToString("0.00");

            var rating = adjustedForOffField;
            return new PositionRating() { Rating = rating, Role = role, Debug = roleDebug, };
        }

        private byte GetRatingsForPersonality(Player player)
        {
            string mentalDebugString;
            byte[] values = GetValues(player);

            byte offField = GetGroupingScore(OffFieldAttributes, values, weightings[weightings.Length - 1], false, out mentalDebugString);

            return offField;
        }

        private decimal GetAdjustmentByOffFieldRating(byte offFieldRating)
        {
            return -5 + (((decimal)offFieldRating) / 10);
        }

        private byte CalculateRating(Player player, PlayerType type, Roles role, ref RatingRoleDebug debug)
        {
            RatingRoleDebug roleDebug;
            var weights = GetWeights(role);

            //var values = GetValues(player);
            decimal result = RatePlayerInRole(player, type, role, weights, out roleDebug);

            debug = roleDebug;

            decimal BestRating = 90;
            decimal WorstRating = 35;
            decimal spreadForNewRatings = 98;

            result = Math.Max(result, WorstRating);
            result = Math.Min(result, BestRating);

            // function to reset to 1-99 from above expected range
            var proportion = (((decimal)result - WorstRating) * spreadForNewRatings) / (BestRating - WorstRating);
            proportion += 1;

            return (byte)proportion;
        }

        private byte RatePlayerInRole(Player player, PlayerType type, Roles role, byte[] weights, out RatingRoleDebug debug)
        {
            string mentalDebugString, physicalDebugString, technicalDebugString;

            byte mentalWeight = weights.GW(DP.MentalityWeight);
            byte physicalWeight = weights.GW(DP.PhysicalityWeight);
            byte technicalWeight = weights.GW(DP.TechnicalWeight);

            byte[] values = GetValues(player);

            var mental = GetGroupingScore(MentalAttributes, values, weights, false, out mentalDebugString);
            var physical = GetGroupingScore(PhysicalAttributes, values, weights, false, out physicalDebugString);
            var technical = GetGroupingScore(TechnicalAttributes, values, weights, true, out technicalDebugString);

            decimal mentalScore = Weight(mental, mentalWeight);
            decimal physicalScore = Weight(physical, physicalWeight, weights.GW(DP.PhysicalInflation));
            decimal technicalScore = Weight(technical, technicalWeight, weights.GW(DP.TechnicalInflation));

            decimal adjust = (decimal)(mentalWeight + physicalWeight + technicalWeight) / 100;

            debug = new RatingRoleDebug()
            {
                Position = type.ToString(),
                Role = role,
                Mental = $"{mentalScore} / {mentalWeight}",
                MentalDetail = mentalDebugString,
                Physical = $"{physicalScore} / {physicalWeight}",
                PhysicalDetail = physicalDebugString,
                Technical = $"{technicalScore} / {technicalWeight}",
                TechnicalDetail = technicalDebugString,
            };

            return (byte)((mentalScore + physicalScore + technicalScore) / adjust);
        }

        private byte AdjustScoreForPosition(Player player, PlayerType type, decimal unadjustedScore, RatingRoleDebug debug)
        {
            decimal positionModifier = (decimal)PositionalFamiliarity(type, player) / 100;
            debug.Position = positionModifier.ToString("0.00");

            return (byte)(unadjustedScore * positionModifier);
        }

        private byte AdjustScoreForOffField(byte unadjustedScore, decimal offFieldRating, RatingRoleDebug debug)
        {
            return (byte)Math.Min(99, Math.Max(0, (unadjustedScore + offFieldRating)));
        }

        private byte GetGroupingScore(byte[] attributes, byte[] values, byte[] weights, bool isIntrinsic, out string debugString)
        {
            decimal rating = 0;
            int combinedWeights = 0;
            byte realValue;
            debugString = string.Empty;

            if (values.Length != weights.Length || Enum.GetNames(typeof(DP)).Length != values.Length)
            {
                throw new ApplicationException("Unbalanced");
            }

            //for (int i = 0; i < weights.Length; i++)
            foreach (var i in attributes)
            {
                byte weight = weights[i];

                if (weight == 0)
                {
                    continue;
                }

                combinedWeights += weight;
                realValue = values[i];
                string attribute = Enum.GetNames(typeof(DP))[i];

                decimal value = Adj(realValue, isIntrinsic);

                decimal cappedValue = Math.Min(20, value);
                if (value > 20)
                {
                    decimal remainder = (value - 20) / 2;
                    cappedValue += remainder;
                }

                decimal weightedValue = cappedValue * weight;

                rating += weightedValue;

                debugString += $"{attribute} : {value}-{weight}({realValue}) ";
            }

            int maxScore = 20 * combinedWeights;

            var result = (byte)((rating / maxScore) * 100);
            return Math.Min((byte)99, result);
        }

        private byte PositionalFamiliarity(PlayerType type, Player player)
        {
            byte modifierForPosition = 100;
            byte modifierForVersitility = GetVersitilityModifier(player._player.Versatility);

            switch (type)
            {
                case PlayerType.GoalKeeper:
                    modifierForPosition = GetFamiliarity(player._player.GK, player._player.GK); // double down on GK position, not side
                    break;

                case PlayerType.RightBack:
                    modifierForPosition = GetFamiliarity(player._player.DF, player._player.Right);
                    break;

                case PlayerType.CentreHalf:
                    modifierForPosition = GetFamiliarity(player._player.DF, player._player.Centre);
                    break;

                case PlayerType.LeftBack:
                    modifierForPosition = GetFamiliarity(player._player.DF, player._player.Left);
                    break;

                case PlayerType.RightWingBack:
                    modifierForPosition = GetFamiliarity(player._player.WingBack, player._player.Right);
                    break;

                case PlayerType.DefensiveMidfielder:
                    modifierForPosition = GetFamiliarity(player._player.DM, player._player.Centre);
                    break;

                case PlayerType.LeftWingBack:
                    modifierForPosition = GetFamiliarity(player._player.WingBack, player._player.Left);
                    break;

                case PlayerType.RightMidfielder:
                    modifierForPosition = GetFamiliarity(player._player.MF, player._player.Right);
                    break;

                case PlayerType.CentralMidfielder:
                    modifierForPosition = GetFamiliarity(player._player.MF, player._player.Centre);
                    break;

                case PlayerType.LeftMidfielder:
                    modifierForPosition = GetFamiliarity(player._player.MF, player._player.Left);
                    break;

                case PlayerType.RightWinger:
                    modifierForPosition = GetFamiliarity(player._player.AM, player._player.Right);
                    break;

                case PlayerType.AttackingMidfielder:
                    modifierForPosition = GetFamiliarity(player._player.AM, player._player.Centre);
                    break;

                case PlayerType.LeftWinger:
                    modifierForPosition = GetFamiliarity(player._player.AM, player._player.Left);
                    break;

                case PlayerType.CentreForward:
                    modifierForPosition = GetFamiliarity(player._player.ST, player._player.Centre);
                    break;

                default:
                    modifierForPosition = 1;
                    break;
            }

            return (byte)(((decimal)modifierForPosition + modifierForVersitility) / 200 * 100);
        }

        private byte GetFamiliarity(byte position, byte channel = 20)
        {
            return (byte)((position + channel) * 2.5);
        }

        private byte GetVersitilityModifier(byte versatility)
        {
            return (byte)100;
        }

        private decimal Weight(byte score, short importance, short inflationPercentage = 100)
        {
            if (inflationPercentage == 0)
            {
                inflationPercentage = 100;
            }

            score = Math.Min(score, (byte)99);

            decimal inflatedValue = score * ((decimal)inflationPercentage / 100);
            var inflatedScore = Math.Min(99, Math.Max(1, (int)Math.Round(inflatedValue)));

            return (decimal)inflatedScore / 100 * importance;
        }

        private decimal Adj(byte val, bool isIntrinsic)
        {
            if (!isIntrinsic)
            {
                return val;
            }

            return masker.GetIntrinsicMask(val);
        }

        private byte[] GetWeights(Roles role)
        {
            return weightings[(int)role];
        }

        private void AddBaselineMentality(byte[] role)
        {
            role.AW(DP.Aggression, 1);
            role.AW(DP.Anticipation, 1);
            role.AW(DP.Bravery, 1);
            role.AW(DP.Consistency, 4);
            role.AW(DP.Decisions, 4);
            role.AW(DP.Dirtiness, 0);
            role.AW(DP.ImportantMatches, 1);
            role.AW(DP.Influence, 0);
            role.AW(DP.Teamwork, 1);
            role.AW(DP.WorkRate, 1);
        }

        private void AddGK()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];

            role.AW(DP.Aggression, 0);
            role.AW(DP.Anticipation, 3);
            role.AW(DP.Bravery, 1);
            role.AW(DP.Consistency, 3);
            role.AW(DP.Dirtiness, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.ImportantMatches, 2);
            role.AW(DP.Influence, 0);
            role.AW(DP.Teamwork, 0);
            role.AW(DP.WorkRate, 0);

            role.AW(DP.Acceleration, 0);
            role.AW(DP.Agility, 3);
            role.AW(DP.Balance, 0);
            role.AW(DP.Jumping, 3);
            role.AW(DP.Pace, 0);
            role.AW(DP.Stamina, 0);
            role.AW(DP.Strength, 1);

            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 0);
            role.AW(DP.Decisions, 3);
            role.AW(DP.Dribbling, 0);
            role.AW(DP.Finishing, 0);
            role.AW(DP.Heading, 0);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.LongShots, 0);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 0);
            role.AW(DP.Passing, 1);
            role.AW(DP.Positioning, 3);
            role.AW(DP.Tackling, 1);
            role.AW(DP.Technique, 0);
            role.AW(DP.Versatility, 0);

            role.AW(DP.Handling, 6);
            role.AW(DP.OneOnOnes, 4);
            role.AW(DP.Reflexes, 4);

            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Penalties, 0);
            role.AW(DP.ThrowIns, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 60);
            role.AW(DP.PhysicalityWeight, 60);
            role.AW(DP.TechnicalWeight, 100);

            role.AW(DP.TechnicalInflation, 90);

            weightings[(int)Roles.GK] = role;
        }

        private void AddDFB()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);

            role.AW(DP.Aggression, 3);
            role.AW(DP.Anticipation, 3);
            role.AW(DP.Bravery, 2);
            role.AW(DP.Teamwork, 2);

            role.AW(DP.Acceleration, 5);
            role.AW(DP.Agility, 2);
            role.AW(DP.Balance, 1);
            role.AW(DP.Jumping, 3);
            role.AW(DP.Pace, 3);
            role.AW(DP.Stamina, 1);
            role.AW(DP.Strength, 3);

            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 2);
            role.AW(DP.Decisions, 3);
            role.AW(DP.Dribbling, 1);
            role.AW(DP.Finishing, 0);
            role.AW(DP.Heading, 3);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.LongShots, 0);
            role.AW(DP.Marking, 6);
            role.AW(DP.OffTheBall, 1);
            role.AW(DP.Passing, 2);
            role.AW(DP.Positioning, 7);
            role.AW(DP.Tackling, 7);
            role.AW(DP.Technique, 1);
            role.AW(DP.Versatility, 0);

            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);

            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Penalties, 0);
            role.AW(DP.ThrowIns, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 80);
            
            role.AW(DP.PhysicalityWeight, 90);
            role.AW(DP.PhysicalInflation, 115);

            role.AW(DP.TechnicalWeight, 100);
            role.AW(DP.TechnicalInflation, 110);

            weightings[(int)Roles.DFB] = role;
        }

        private void AddAFB()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);

            role.AW(DP.Aggression, 3);
            role.AW(DP.Anticipation, 3);
            role.AW(DP.Bravery, 2);
            role.AW(DP.Teamwork, 2);

            role.AW(DP.Acceleration, 5);
            role.AW(DP.Agility, 2);
            role.AW(DP.Balance, 1);
            role.AW(DP.Jumping, 2);
            role.AW(DP.Pace, 4);
            role.AW(DP.Stamina, 1);
            role.AW(DP.Strength, 2);

            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 4);
            role.AW(DP.Decisions, 3);
            role.AW(DP.Dribbling, 2);
            role.AW(DP.Finishing, 0);
            role.AW(DP.Heading, 2);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.LongShots, 0);
            role.AW(DP.Marking, 4);
            role.AW(DP.OffTheBall, 3);
            role.AW(DP.Passing, 3);
            role.AW(DP.Positioning, 5);
            role.AW(DP.Tackling, 6);
            role.AW(DP.Technique, 2);
            role.AW(DP.Versatility, 0);

            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);

            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Penalties, 0);
            role.AW(DP.ThrowIns, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 80);
            
            role.AW(DP.PhysicalityWeight, 90);
            role.AW(DP.PhysicalInflation, 115);

            role.AW(DP.TechnicalWeight, 100);
            role.AW(DP.TechnicalInflation, 110);

            weightings[(int)Roles.AFB] = role;
        }

        private void AddCB()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);

            role.AW(DP.Aggression, 5);
            role.AW(DP.Anticipation, 3);
            role.AW(DP.Bravery, 5);


            role.AW(DP.Acceleration, 2);
            role.AW(DP.Agility, 1);
            role.AW(DP.Balance, 1);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 6);
            role.AW(DP.Pace, 2);
            role.AW(DP.Stamina, 1);
            role.AW(DP.Strength, 4);


            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Penalties, 0);
            role.AW(DP.ThrowIns, 0);


            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 0);
            role.AW(DP.Dribbling, 0);
            role.AW(DP.Finishing, 0);
            role.AW(DP.Heading, 3);
            role.AW(DP.LongShots, 0);
            role.AW(DP.Marking, 6);
            role.AW(DP.OffTheBall, 0);
            role.AW(DP.Passing, 2);
            role.AW(DP.Positioning, 8);
            role.AW(DP.Tackling, 7);
            role.AW(DP.Technique, 1);


            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);

            role.AW(DP.Versatility, 0);


            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 85);
            role.AW(DP.PhysicalityWeight, 100);
            role.AW(DP.TechnicalWeight, 100);

            role.AW(DP.TechnicalInflation, 100);

            weightings[(int)Roles.CB] = role;
        }

        private void AddWB()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);
            role.AW(DP.Acceleration, 5);
            role.AW(DP.Agility, 1);
            role.AW(DP.Balance, 1);
            role.AW(DP.Bravery, 2);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 1);
            role.AW(DP.Crossing, 7);
            role.AW(DP.Dirtiness, 0);
            role.AW(DP.Dribbling, 3);
            role.AW(DP.Finishing, 1);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 0);
            role.AW(DP.Heading, 1);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 2);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 3);
            role.AW(DP.OffTheBall, 3);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 5);
            role.AW(DP.Passing, 5);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 5);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 3);
            role.AW(DP.Strength, 2);
            role.AW(DP.Tackling, 3);
            role.AW(DP.Technique, 3);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);
            role.AW(DP.WorkRate, 4);
            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);
            role.AW(DP.MentalityWeight, 80);
            role.AW(DP.PhysicalityWeight, 80);
            role.AW(DP.TechnicalWeight, 100);

            role.AW(DP.TechnicalInflation, 100);

            weightings[(int)Roles.WB] = role;
        }

        private void AddDM()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);

            role.AW(DP.Aggression, 4);
            role.AW(DP.Anticipation, 2);
            role.AW(DP.Bravery, 3);
            role.AW(DP.WorkRate, 4);
            role.AW(DP.Teamwork, 3);

            role.AW(DP.Acceleration, 4);
            role.AW(DP.Agility, 3);
            role.AW(DP.Balance, 3);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 5);
            role.AW(DP.Pace, 2);
            role.AW(DP.Stamina, 2);
            role.AW(DP.Strength, 4);


            role.AW(DP.Creativity, 2);
            role.AW(DP.Crossing, 0);
            role.AW(DP.Dribbling, 0);
            role.AW(DP.Finishing, 0);
            role.AW(DP.Heading, 3);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 6);
            role.AW(DP.OffTheBall, 2);
            role.AW(DP.Passing, 5);
            role.AW(DP.Positioning, 6);
            role.AW(DP.Tackling, 5);
            role.AW(DP.Technique, 2);


            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);

            role.AW(DP.Versatility, 0);

            role.AW(DP.Corners, 0);
            role.AW(DP.Penalties, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.ThrowIns, 0);


            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 70);

            role.AW(DP.PhysicalityWeight, 70);
            role.AW(DP.PhysicalInflation, 110);

            role.AW(DP.TechnicalWeight, 100);
            role.AW(DP.TechnicalInflation, 110);

            weightings[(int)Roles.HM] = role;
        }

        private void AddCM()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);

            role.AW(DP.Aggression, 2);
            role.AW(DP.Bravery, 2);
            role.AW(DP.Teamwork, 2);
            role.AW(DP.WorkRate, 4);

            role.AW(DP.Acceleration, 3);
            role.AW(DP.Agility, 3);
            role.AW(DP.Balance, 3);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 2);
            role.AW(DP.Pace, 3);
            role.AW(DP.Stamina, 5);
            role.AW(DP.Strength, 6);


            role.AW(DP.Creativity, 8);
            role.AW(DP.Crossing, 1);
            role.AW(DP.Dribbling, 1);
            role.AW(DP.Finishing, 2);
            role.AW(DP.Heading, 1);
            role.AW(DP.LongShots, 5);
            role.AW(DP.Marking, 4);
            role.AW(DP.OffTheBall, 4);
            role.AW(DP.Passing, 8);
            role.AW(DP.Positioning, 5);
            role.AW(DP.Tackling, 4);
            role.AW(DP.Technique, 5);


            role.AW(DP.Versatility, 0);


            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);


            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Penalties, 0);


            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 70);

            role.AW(DP.PhysicalityWeight, 70);
            role.AW(DP.PhysicalInflation, 115);

            role.AW(DP.TechnicalWeight, 100);
            role.AW(DP.TechnicalInflation, 110);

            weightings[(int)Roles.CM] = role;
        }

        private void AddWM()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);

            role.AW(DP.Flair, 2);
            role.AW(DP.Teamwork, 2);
            role.AW(DP.WorkRate, 2);

            role.AW(DP.Acceleration, 4);
            role.AW(DP.Agility, 3);
            role.AW(DP.Balance, 3);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 1);
            role.AW(DP.Pace, 3);
            role.AW(DP.Stamina, 2);
            role.AW(DP.Strength, 1);


            role.AW(DP.Creativity, 3);
            role.AW(DP.Crossing, 5);
            role.AW(DP.Dribbling, 5);
            role.AW(DP.Finishing, 2);
            role.AW(DP.Heading, 1);
            role.AW(DP.LongShots, 3);
            role.AW(DP.Marking, 2);
            role.AW(DP.OffTheBall, 5);
            role.AW(DP.Passing, 4);
            role.AW(DP.Positioning, 2);
            role.AW(DP.Tackling, 1);
            role.AW(DP.Technique, 4);


            role.AW(DP.Versatility, 0);


            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);


            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Penalties, 0);


            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 80);
            role.AW(DP.PhysicalityWeight, 90);
            role.AW(DP.PhysicalInflation, 110);

            role.AW(DP.TechnicalWeight, 100);

            role.AW(DP.TechnicalInflation, 100);

            weightings[(int)Roles.WM] = role;
        }

        private void AddAM()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);

            role.AW(DP.Flair, 4);

            role.AW(DP.Acceleration, 3);
            role.AW(DP.Agility, 4);
            role.AW(DP.Balance, 3);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 0);
            role.AW(DP.Pace, 3);
            role.AW(DP.Stamina, 1);
            role.AW(DP.Strength, 0);


            role.AW(DP.Creativity, 5);
            role.AW(DP.Crossing, 3);
            role.AW(DP.Dribbling, 4);
            role.AW(DP.Finishing, 4);
            role.AW(DP.Heading, 0);
            role.AW(DP.LongShots, 4);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 5);
            role.AW(DP.Passing, 5);
            role.AW(DP.Positioning, 1);
            role.AW(DP.Tackling, 1);
            role.AW(DP.Technique, 5);


            role.AW(DP.Versatility, 0);


            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);


            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Penalties, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 90);
            role.AW(DP.PhysicalityWeight, 50);
            role.AW(DP.TechnicalWeight, 100);

            role.AW(DP.TechnicalInflation, 115);

            weightings[(int)Roles.AM] = role;
        }

        private void AddWG()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);

            role.AW(DP.Flair, 3);

            role.AW(DP.Acceleration, 4);
            role.AW(DP.Agility, 3);
            role.AW(DP.Balance, 3);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 1);
            role.AW(DP.Pace, 3);
            role.AW(DP.Stamina, 2);
            role.AW(DP.Strength, 1);


            role.AW(DP.Creativity, 4);
            role.AW(DP.Crossing, 5);
            role.AW(DP.Dribbling, 5);
            role.AW(DP.Finishing, 3);
            role.AW(DP.Heading, 1);
            role.AW(DP.LongShots, 3);
            role.AW(DP.Marking, 1);
            role.AW(DP.OffTheBall, 5);
            role.AW(DP.Passing, 4);
            role.AW(DP.Positioning, 2);
            role.AW(DP.Tackling, 1);
            role.AW(DP.Technique, 4);


            role.AW(DP.Versatility, 0);


            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);


            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Penalties, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 70);
            role.AW(DP.PhysicalityWeight, 90);
            role.AW(DP.PhysicalInflation, 110);

            role.AW(DP.TechnicalWeight, 100);
            role.AW(DP.TechnicalInflation, 110);

            weightings[(int)Roles.WG] = role;
        }

        private void AddST()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);
            role.AW(DP.Consistency, 0);
            role.AW(DP.Decisions, 2);
            role.AW(DP.Flair, 3);

            role.AW(DP.Acceleration, 10);
            role.AW(DP.Agility, 7);
            role.AW(DP.Balance, 1);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 3);
            role.AW(DP.Pace, 7);
            role.AW(DP.Stamina, 7);
            role.AW(DP.Strength, 7);


            role.AW(DP.Creativity, 1);
            role.AW(DP.Crossing, 1);
            role.AW(DP.Dribbling, 1);
            role.AW(DP.Finishing, 10);
            role.AW(DP.Heading, 1);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 10);
            role.AW(DP.Passing, 1);
            role.AW(DP.Positioning, 0);
            role.AW(DP.Tackling, 0);
            role.AW(DP.Technique, 7);


            role.AW(DP.Versatility, 0);


            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);


            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Penalties, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 50);

            role.AW(DP.PhysicalityWeight, 80);
            role.AW(DP.PhysicalInflation, 100);

            role.AW(DP.TechnicalWeight, 100);
            role.AW(DP.TechnicalInflation, 100);

            weightings[(int)Roles.ST] = role;
        }

        private void AddPO()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);
            role.AW(DP.Consistency, 1);
            role.AW(DP.Decisions,0);
            role.AW(DP.Flair, 1);

            role.AW(DP.Acceleration, 7);
            role.AW(DP.Agility, 7);
            role.AW(DP.Balance, 1);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 1);
            role.AW(DP.Pace, 10);
            role.AW(DP.Stamina, 7);
            role.AW(DP.Strength, 3);


            role.AW(DP.Creativity, 1);
            role.AW(DP.Crossing, 1);
            role.AW(DP.Dribbling, 7);
            role.AW(DP.Finishing, 10);
            role.AW(DP.Heading, 1);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 7);
            role.AW(DP.Passing, 1);
            role.AW(DP.Positioning, 0);
            role.AW(DP.Tackling, 0);
            role.AW(DP.Technique, 3);


            role.AW(DP.Versatility, 0);


            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);


            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Penalties, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 40);

            role.AW(DP.PhysicalityWeight, 70);
            role.AW(DP.PhysicalInflation, 100);

            role.AW(DP.TechnicalWeight, 100);
            role.AW(DP.TechnicalInflation, 100);

            weightings[(int)Roles.PO] = role;
        }

        private void AddTM()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);
            role.AW(DP.Consistency, 0);
            role.AW(DP.Decisions, 0);
            role.AW(DP.Flair, 0);

            role.AW(DP.Acceleration, 3);
            role.AW(DP.Agility, 1);
            role.AW(DP.Balance, 1);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 10);
            role.AW(DP.Pace, 6);
            role.AW(DP.Stamina, 8);
            role.AW(DP.Strength, 8);


            role.AW(DP.Creativity, 1);
            role.AW(DP.Crossing, 1);
            role.AW(DP.Dribbling, 3);
            role.AW(DP.Finishing, 7);
            role.AW(DP.Heading, 10);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 7);
            role.AW(DP.Passing, 3);
            role.AW(DP.Positioning, 3);
            role.AW(DP.Tackling, 0);
            role.AW(DP.Technique, 3);


            role.AW(DP.Versatility, 0);


            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);


            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Penalties, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 30);

            role.AW(DP.PhysicalityWeight, 100);
            role.AW(DP.PhysicalInflation, 100);

            role.AW(DP.TechnicalWeight, 100);
            role.AW(DP.TechnicalInflation, 100);

            weightings[(int)Roles.TM] = role;
        }

        private void AddCF()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            AddBaselineMentality(role);
            role.AW(DP.Aggression, 2);
            role.AW(DP.Decisions, 2);
            role.AW(DP.Flair, 2);
            role.AW(DP.WorkRate, 2);

            role.AW(DP.Acceleration, 10);
            role.AW(DP.Agility, 7);
            role.AW(DP.Balance, 1);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 10);
            role.AW(DP.Pace, 10);
            role.AW(DP.Stamina, 10);
            role.AW(DP.Strength, 7);


            role.AW(DP.Creativity, 1);
            role.AW(DP.Crossing, 1);
            role.AW(DP.Dribbling, 1);
            role.AW(DP.Finishing, 10);
            role.AW(DP.Heading, 3);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 7);
            role.AW(DP.Passing, 3);
            role.AW(DP.Positioning, 0);
            role.AW(DP.Tackling, 0);
            role.AW(DP.Technique, 3);


            role.AW(DP.Versatility, 0);


            role.AW(DP.Handling, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Reflexes, 0);


            role.AW(DP.Corners, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Penalties, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 30);

            role.AW(DP.PhysicalityWeight, 70);
            role.AW(DP.PhysicalInflation, 105);

            role.AW(DP.TechnicalWeight, 100);
            role.AW(DP.TechnicalInflation, 105);

            weightings[(int)Roles.CF] = role;
        }

        private void AddOffField()
        {
            byte[] person = new byte[Enum.GetNames(typeof(DP)).Length];
            person.AW(DP.Adaptability, 0);
            person.AW(DP.Ambition, 0);
            person.AW(DP.Determination, 20);
            person.AW(DP.Loyalty, 4);
            person.AW(DP.Pressure, 6);
            person.AW(DP.Professionalism, 4);
            person.AW(DP.Sportsmanship, 0);
            person.AW(DP.Temperament, 3);
            weightings[weightings.Length - 1] = person;
        }
    }
}
