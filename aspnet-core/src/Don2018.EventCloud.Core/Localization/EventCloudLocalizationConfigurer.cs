using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Don2018.EventCloud.Localization
{
    public static class EventCloudLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(EventCloudConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(EventCloudLocalizationConfigurer).GetAssembly(),
                        "Don2018.EventCloud.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
