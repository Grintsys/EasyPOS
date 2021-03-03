using Volo.Abp.Settings;

namespace Grintsys.EasyPOS.Settings
{
    public class EasyPOSSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(EasyPOSSettings.MySetting1));
        }
    }
}
