using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYYServerLightPlugin.API
{
    public static class MyAPIStatic
    {
        //扩展类API必须放到static class里
        public static bool IsScpItem(this ItemType type) => type == ItemType.SCP018 || type == ItemType.SCP500 || type == ItemType.SCP268 || type == ItemType.SCP207 || type == ItemType.SCP244a || type == ItemType.SCP244b || type == ItemType.SCP2176 || type == ItemType.SCP1853;
        //就不自己打了
        public static bool IsKeycard(this ItemType type) => type == ItemType.KeycardJanitor || type == ItemType.KeycardScientist || type == ItemType.KeycardResearchCoordinator || type == ItemType.KeycardZoneManager || type == ItemType.KeycardGuard || type == ItemType.KeycardNTFOfficer || type == ItemType.KeycardContainmentEngineer || type == ItemType.KeycardNTFLieutenant || type == ItemType.KeycardNTFCommander || type == ItemType.KeycardFacilityManager || type == ItemType.KeycardChaosInsurgency || type == ItemType.KeycardO5;

    }
}
