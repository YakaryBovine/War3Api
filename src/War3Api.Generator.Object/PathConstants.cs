// ------------------------------------------------------------------------------
// <copyright file="PathConstants.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace War3Api.Generator.Object
{
    internal static class PathConstants
    {
        // Base
        internal const string WorldEditGameStringsPath = @"_locales\enus.w3mod\ui\worldeditgamestrings.txt";
        internal const string WorldEditStringsPath = @"_locales\enus.w3mod\ui\worldeditstrings.txt";
        internal const string EnumDataFilePath = @"ui\uniteditordata.txt";
        internal const string WorldEditDataPath = @"ui\worldeditdata.txt";

        // Unit
        internal const string UnitDataPath = @"units\unitdata.slk";
        internal const string UnitAbilityDataPath = @"units\unitabilities.slk";
        internal const string UnitBalanceDataPath = @"units\unitbalance.slk";
        internal const string UnitUiDataPath = @"units\unitui.slk";
        internal const string UnitWeaponDataPath = @"units\unitweapons.slk";

        // Item
        internal const string ItemDataPath = @"units\itemdata.slk";

        // Destructable
        internal const string DestructableDataPath = @"units\destructabledata.slk";

        // Doodad
        internal const string DoodadDataPath = @"doodads\doodads.slk";

        // Ability
        internal const string AbilityDataPath = @"units\abilitydata.slk";

        // Buff
        internal const string BuffDataPath = @"units\abilitybuffdata.slk";

        // Upgrade
        internal const string UpgradeDataPath = @"units\upgradedata.slk";

        // Metadata
        internal const string UnitMetaDataPath = @"units\unitmetadata.slk";
        internal const string ItemMetaDataPath = UnitMetaDataPath;
        internal const string DestructableMetaDataPath = @"units\destructablemetadata.slk";
        internal const string DoodadMetaDataPath = @"doodads\doodadmetadata.slk";
        internal const string AbilityMetaDataPath = @"units\abilitymetadata.slk";
        internal const string BuffMetaDataPath = @"units\abilitybuffmetadata.slk";
        internal const string UpgradeMetaDataPath = @"units\upgrademetadata.slk";

        // Localized skins
        private const string CampaignAbilityStringsPath = @"_locales\enus.w3mod\units\campaignabilitystrings.txt";
        private const string CampaignUnitStringsPath = @"_locales\enus.w3mod\units\campaignunitstrings.txt";
        private const string CampaignUpgradesStringsPath = @"_locales\enus.w3mod\units\campaignupgradestrings.txt";
        private const string CommonAbilityStringsPath = @"_locales\enus.w3mod\units\commonabilitystrings.txt";
        private const string DestructableSkinStringsPath = @"_locales\enus.w3mod\units\destructableskinstrings.txt";
        private const string HumanAbilityStringsPath = @"_locales\enus.w3mod\units\humanabilitystrings.txt";
        private const string HumanUnitStringsPath = @"_locales\enus.w3mod\units\humanunitstrings.txt";
        private const string HumanUpgradeStringsPath = @"_locales\enus.w3mod\units\humanupgradestrings.txt";
        private const string ItemAbilityStringsPath = @"_locales\enus.w3mod\units\itemabilitystrings.txt";
        private const string ItemSkinStringsPath = @"_locales\enus.w3mod\units\itemskinstrings.txt";
        private const string ItemtStringsPath = @"_locales\enus.w3mod\units\itemstrings.txt";
        private const string NeutralAbilityStringsPath = @"_locales\enus.w3mod\units\neutralabilitystrings.txt";
        private const string NeutralUnitStringsPath = @"_locales\enus.w3mod\units\neutralunitstrings.txt";
        private const string NeutralUpgradeStringsPath = @"_locales\enus.w3mod\units\neutralupgradestrings.txt";
        private const string NightElfAbilityStringsPath = @"_locales\enus.w3mod\units\nightelfabilitystrings.txt";
        private const string NightElfUnitStringsPath = @"_locales\enus.w3mod\units\nightelfunitstrings.txt";
        private const string NightElfUpgradeStringsPath = @"_locales\enus.w3mod\units\nightelfupgradestrings.txt";
        private const string OrcAbilityStringsPath = @"_locales\enus.w3mod\units\orcabilitystrings.txt";
        private const string OrcUnitStringsPath = @"_locales\enus.w3mod\units\orcunitstrings.txt";
        private const string OrcUpgradeStringsPath = @"_locales\enus.w3mod\units\orcupgradestrings.txt";
        private const string UndeadAbilityStringsPath = @"_locales\enus.w3mod\units\undeadabilitystrings.txt";
        private const string UndeadUnitStringsPath = @"_locales\enus.w3mod\units\undeadunitstrings.txt";
        private const string UndeadUpgradeStringsPath = @"_locales\enus.w3mod\units\undeadupgradestrings.txt";
        private const string UnitGlobalStringsPath = @"_locales\enus.w3mod\units\unitglobalstrings.txt";
        private const string UnitSkinStringsPath = @"_locales\enus.w3mod\units\unitskinstrings.txt";
        private const string UpgradeSkinStringsPath = @"_locales\enus.w3mod\units\upgradeskinstrings.txt";

        // Non-localized skins
        private const string DestructableSkinPath = @"units\destructableskin.txt";
        private const string DoodadSkinsPath = @"doodads\doodadskins.txt";

        /// <summary>
        /// Gets the paths to all strings.txt files, which store skin-based information about objects.
        /// </summary>
        internal static IEnumerable<string> GetSkinStringsPaths()
        {
            yield return CampaignAbilityStringsPath;
            yield return CampaignUnitStringsPath;
            yield return CampaignUpgradesStringsPath;
            yield return CommonAbilityStringsPath;
            yield return DestructableSkinStringsPath;
            yield return HumanAbilityStringsPath;
            yield return HumanUnitStringsPath;
            yield return HumanUpgradeStringsPath;
            yield return ItemAbilityStringsPath;
            yield return ItemSkinStringsPath;
            yield return ItemtStringsPath;
            yield return NeutralAbilityStringsPath;
            yield return NeutralUnitStringsPath;
            yield return NeutralUpgradeStringsPath;
            yield return NightElfAbilityStringsPath;
            yield return NightElfUnitStringsPath;
            yield return NightElfUpgradeStringsPath;
            yield return OrcAbilityStringsPath;
            yield return OrcUnitStringsPath;
            yield return OrcUpgradeStringsPath;
            yield return UndeadAbilityStringsPath;
            yield return UndeadUnitStringsPath;
            yield return UndeadUpgradeStringsPath;
            yield return UnitGlobalStringsPath;
            yield return UnitSkinStringsPath;
            yield return UpgradeSkinStringsPath;

            yield return DestructableSkinPath;
            yield return DoodadSkinsPath;
            yield return @"units\unitskin.txt";
            yield return @"units\abilityskin.txt";
            yield return @"units\itemskin.txt";
            yield return @"units\upgradeskin.txt";

            yield return @"units\campaignabilityfunc.txt";
            yield return @"units\campaignunitfunc.txt";
            yield return @"units\campaignupgradefunc.txt";
            yield return @"units\commonabilityfunc.txt";
            yield return @"units\humanabilityfunc.txt";
            yield return @"units\humanunitfunc.txt";
            yield return @"units\humanupgradefunc.txt";
            yield return @"units\itemabilityfunc.txt";
            yield return @"units\itemfunc.txt";
            yield return @"units\neutralabilityfunc.txt";
            yield return @"units\neutralunitfunc.txt";
            yield return @"units\neutralupgradefunc.txt";
            yield return @"units\nightelfabilityfunc.txt";
            yield return @"units\nightelfunitfunc.txt";
            yield return @"units\nightelfupgradefunc.txt";
            yield return @"units\orcabilityfunc.txt";
            yield return @"units\orcunitfunc.txt";
            yield return @"units\orcupgradefunc.txt";
            yield return @"units\undeadabilityfunc.txt";
            yield return @"units\undeadunitfunc.txt";
            yield return @"units\undeadupgradefunc.txt";
            yield return @"units\unitweaponsfunc.txt";
            yield return @"units\orcupgradefunc.txt";
        }
    }
}