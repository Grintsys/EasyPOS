using Grintsys.EasyPOS.Localization;
using Volo.Abp.Authorization.Permissions;
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
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EasyPOSResource>(name);
        }
    }
}
