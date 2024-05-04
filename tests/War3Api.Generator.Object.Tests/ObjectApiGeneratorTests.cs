using System.IO;

using FluentAssertions;

using NUnit.Framework;

using War3Net.Build.Object;

namespace War3Api.Generator.Object.Tests
{
    public sealed class Tests
    {
        private const string Version = "1.36.1";
        private const string InputFolder = @"..\..\..\..\..\src\War3Api.Generator.Object\API";
        private const string OutputFolder = @"..\..\..\..\War3Api.Object\Generated";

        [SetUp]
        public void Setup()
        {
            var inputFolder = Path.Combine(InputFolder, Version);
            var outputFolder = Path.Combine(OutputFolder, Version);
            ObjectApiGenerator.InitializeGenerator(inputFolder, outputFolder);
        }

        [TestCase(ObjectDataType.String,
            "\"Drops a Sentry Ward to spy upon an area for <AIsw,Dur1> seconds. |nContains <wswd,uses> charges.\"",
            "\"Drops a Sentry Ward to spy upon an area for <AIsw,Dur1> seconds. |nContains <wswd,uses> charges.\"")]
        public void GetPropertyValueReturnsExpectedValue(ObjectDataType objectDataType, string value, string expectedOutput)
        {
            ObjectApiGenerator.GetPropertyValue(objectDataType, value).Should().Be(expectedOutput);
        }
    }
}