using Grintsys.EasyPOS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Localization;

namespace Grintsys.EasyPOS.Permissions
{
    public class EasyPOSPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(EasyPOSPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(EasyPOSPermissions.MyPermission1, L("Permission:MyPermission1"));

            var sync = context.AddGroup("Sync");

            sync.AddPermission("Ver/Modificar_Sincronizaciones");

            var conf = context.AddGroup("Config");
            var perm = conf.AddPermission("Conf_Management");
            perm.AddChild("Listar_Conf");

            context.GetPermissionOrNull(IdentityPermissions.Users.ManagePermissions).IsEnabled = true;
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EasyPOSResource>(name);
        }
    }
}
