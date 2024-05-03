﻿// ------------------------------------------------------------------------------
// <copyright file="UnitApiGenerator.cs" company="Drake53">
// Copyright (c) 2020 Drake53. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------

#pragma warning disable CA1303 // Do not pass literals as localized parameters

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using War3Api.Generator.Object.Extensions;
using War3Api.Generator.Object.Models;

namespace War3Api.Generator.Object
{
    internal static class UnitApiGenerator
    {
        private const bool IsUnitClassAbstract = false;

        private static Dictionary<string, TableModel> _dataTables;
        private static TableModel _metadataTable;

        private static EnumModel _enumModel;

        private static bool _initialized = false;

        internal static void InitializeGenerator(string inputFolder)
        {
            if (_initialized)
            {
                throw new InvalidOperationException("Already initialized.");
            }

            if (!ObjectApiGenerator.IsInitialized)
            {
                throw new InvalidOperationException("Must initialize ObjectApiGenerator first.");
            }

            _dataTables = new[]
            {
                new TableModel(Path.Combine(inputFolder, PathConstants.UnitAbilityDataPath), DataConstants.UnitAbilityDataKeyColumn, DataConstants.CommentOrCommentsColumn),
                new TableModel(Path.Combine(inputFolder, PathConstants.UnitBalanceDataPath), DataConstants.UnitBalanceDataKeyColumn, DataConstants.CommentOrCommentsColumn),
                new TableModel(Path.Combine(inputFolder, PathConstants.UnitDataPath), DataConstants.UnitDataKeyColumn, DataConstants.CommentOrCommentsColumn),
                new TableModel(Path.Combine(inputFolder, PathConstants.UnitUiDataPath), DataConstants.UnitUiDataKeyColumn, DataConstants.UnitDataNameColumn),
                new TableModel(Path.Combine(inputFolder, PathConstants.UnitWeaponDataPath), DataConstants.UnitWeaponDataKeyColumn, DataConstants.CommentOrCommentsColumn),
            }
            .ToDictionary(table => table.TableName, StringComparer.OrdinalIgnoreCase);

            _metadataTable = new TableModel(Path.Combine(inputFolder, PathConstants.UnitMetaDataPath));
            ObjectApiGenerator.Localize(_metadataTable.Table);

            _enumModel = new EnumModel(DataConstants.UnitTypeEnumName);

            var members = new Dictionary<string, string>(StringComparer.Ordinal);

            _dataTables["unitui"].AddValues(members);
            _dataTables["unitdata"].AddValues(members);

            _dataTables["unitabilities"].AddValues(members);
            _dataTables["unitbalance"].AddValues(members);
            _dataTables["unitweapons"].AddValues(members);

            foreach (var member in members)
            {
                _enumModel.Members.Add(ObjectApiGenerator.CreateEnumMemberModel(member.Value, member.Key));
            }

            _enumModel.EnsureMemberNamesUnique();

            _initialized = true;
        }

        internal static void Generate()
        {
            const int LimitSubclasses = 100;

            // MetaData columns
            var idColumn = _metadataTable.Table[DataConstants.MetaDataIdColumn].Single();
            var fieldColumn = _metadataTable.Table[DataConstants.MetaDataFieldColumn].Single();
            var dataSourceColumn = _metadataTable.Table[DataConstants.MetaDataSlkColumn].Single();
            var categoryColumn = _metadataTable.Table[DataConstants.MetaDataCategoryColumn].Single();
            var displayNameColumn = _metadataTable.Table[DataConstants.MetaDataDisplayNameColumn].Single();
            var typeColumn = _metadataTable.Table[DataConstants.MetaDataTypeColumn].Single();
            var minValColumn = _metadataTable.Table[DataConstants.MetaDataMinValColumn].Single();
            var maxValColumn = _metadataTable.Table[DataConstants.MetaDataMaxValColumn].Single();
            var useHeroColumn = _metadataTable.Table[DataConstants.MetaDataUseHeroColumn].Single();
            var useUnitColumn = _metadataTable.Table[DataConstants.MetaDataUseUnitColumn].Single();
            var useBuildingColumn = _metadataTable.Table[DataConstants.MetaDataUseBuildingColumn].Single();

            var properties = _metadataTable.Table
                .Skip(1)
                .Where(property => property[useHeroColumn].ParseBool() || property[useUnitColumn].ParseBool() || property[useBuildingColumn].ParseBool())
                .Select(property => new PropertyModel
                {
                    Rawcode = (string)property[idColumn],
                    DataName = (string)property[fieldColumn],
                    DataSource = (string)property[dataSourceColumn],
                    IdentifierName = ObjectApiGenerator.CreatePropertyIdentifierName(
                        (string)property[categoryColumn],
                        (string)property[displayNameColumn]),
                    Type = (string)property[typeColumn],
                    MinVal = property[minValColumn],
                    MaxVal = property[maxValColumn],
                    Specifics = ImmutableHashSet<int>.Empty,
                    SpecificUniqueNames = new(),
                })
                .ToDictionary(property => property.Rawcode);

            ObjectApiGenerator.EnsurePropertyNamesUnique(properties.Values);
            ObjectApiGenerator.PrecomputePropertyDataColumns(properties.Values, _dataTables);

            // Unit types (enum)
            ObjectApiGenerator.GenerateEnumFile(_enumModel);

            // Unit (class)
            var classMembers = new List<MemberDeclarationSyntax>();
            classMembers.AddRange(ObjectApiGenerator.GetConstructors(DataConstants.UnitClassName, DataConstants.UnitTypeEnumName));
            classMembers.AddRange(ObjectApiGenerator.GetProperties(DataConstants.UnitClassName, DataConstants.UnitTypeEnumName, properties.Values));

            ObjectApiGenerator.GenerateMember(SyntaxFactoryService.Class(DataConstants.UnitClassName, IsUnitClassAbstract, DataConstants.BaseClassName, classMembers));

            // UnitLoader
            var loaderMembers = ObjectApiGenerator.GetLoaderMethods(
                _enumModel.Members,
                properties.Values,
                _dataTables,
                DataConstants.UnitClassName,
                DataConstants.UnitTypeEnumName,
                IsUnitClassAbstract);

            ObjectApiGenerator.GenerateMember(
                SyntaxFactoryService.Class(
                    $"{DataConstants.UnitClassName}Loader",
                    new[] { SyntaxKind.InternalKeyword },
                    null,
                    loaderMembers));
        }
    }
}