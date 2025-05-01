using HarmonyLib;
using Miniscript;

[HarmonyPatch]
internal class GreyInterpreterPatch
{
    private static GreyMap _computerType;
    [HarmonyPatch(typeof(GreyInterpreter), "ComputerType")]
    static bool Prefix(GreyInterpreter __instance, ref GreyMap __result)
    {
        if (_computerType == null)
        {
            GreyMap greyMap = new GreyMap();
            greyMap["get_ports"] = Intrinsic.GetByName("get_ports").GetFunc();
            greyMap["File"] = Intrinsic.GetByName("File").GetFunc();
            greyMap["create_folder"] = Intrinsic.GetByName("create_folder").GetFunc();
            greyMap["is_network_active"] = Intrinsic.GetByName("is_network_active").GetFunc();
            greyMap["local_ip"] = Intrinsic.GetByName("lan_ip").GetFunc();
            greyMap["public_ip"] = Intrinsic.GetByName("public_ip_pc").GetFunc();
            greyMap["touch"] = Intrinsic.GetByName("touch").GetFunc();
            greyMap["show_procs"] = Intrinsic.GetByName("show_procs").GetFunc();
            greyMap["network_devices"] = Intrinsic.GetByName("network_devices").GetFunc();
            greyMap["change_password"] = Intrinsic.GetByName("change_password").GetFunc();
            greyMap["create_user"] = Intrinsic.GetByName("create_user").GetFunc();
            greyMap["delete_user"] = Intrinsic.GetByName("delete_user").GetFunc();
            greyMap["create_group"] = Intrinsic.GetByName("create_group").GetFunc();
            greyMap["delete_group"] = Intrinsic.GetByName("delete_group").GetFunc();
            greyMap["groups"] = Intrinsic.GetByName("groups").GetFunc();
            greyMap["close_program"] = Intrinsic.GetByName("close_program").GetFunc();
            greyMap["wifi_networks"] = Intrinsic.GetByName("wifi_networks").GetFunc();
            greyMap["connect_wifi"] = Intrinsic.GetByName("connect_wifi").GetFunc();
            greyMap["connect_ethernet"] = Intrinsic.GetByName("connect_ethernet").GetFunc();
            greyMap["disconnect_network"] = Intrinsic.GetByName("disconnect_network").GetFunc();
            greyMap["active_net_card"] = Intrinsic.GetByName("active_net_card").GetFunc();
            greyMap["network_gateway"] = Intrinsic.GetByName("network_gateway").GetFunc();
            greyMap["get_name"] = Intrinsic.GetByName("get_name").GetFunc();
            _computerType = greyMap;
        }
        __result = _computerType;
        return false;
    }
}