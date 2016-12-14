﻿using GovITHub.Auth.Common.Services.DeviceDetection.DataContracts;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.Regexes;
using GovITHub.Auth.Common.Services.DeviceDetection.DeviceInfoBuilders.YamlSchema;
using GovITHub.Auth.Common.Tests.DeviceDetection.DeviceInfoBuilders.Regexes;
using Xunit;

namespace GovITHub.Auth.Common.Tests.Services.DeviceDetection.DeviceInfoBuilders
{
    public class BrowserInfoBuilderTests
    {
        private readonly BrowserInfoBuilder _builder;

        public BrowserInfoBuilderTests()
        {
            var reader = new RegexResourceFileStreamReader("GovITHub.Auth.Common.Tests.browsers.yml");
            _builder = new BrowserInfoBuilder(new SimpleResourceFileRegexLoader<BrowserRegex>(reader));
        }

        [Theory]
        [InlineData(
            @"Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.87 Safari/537.36 OPR/41.0.2353.56",
            "Opera 41.0.2353.56")]
        [InlineData(
            @"Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36",
            "Chrome 54.0.2840.99")]
        [InlineData(
            @"Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko",
            "Internet Explorer 11.0")]
        public void ShoulProperlyParseClientInfo(string userAgent, string expectedOutput)
        {
            var deviceInfo = new DeviceInfo
            {
                UserAgent = userAgent
            };
            _builder.Build(deviceInfo, userAgent);
            Assert.Equal(expectedOutput, deviceInfo.Browser);
        }
    }
}