using HarmonyLib;
using Miniscript;
using System.Reflection;

[HarmonyPatch]
public class GreyInterpreterPatch
{
    [HarmonyPatch(typeof(GreyInterpreter), "ComputerType")]
    class ComputerTypePatch
    {
        static void Postfix(GreyInterpreter __instance, ref GreyMap __result)
        {
            FieldInfo fieldInfo = AccessTools.Field(typeof(GreyInterpreter), "_computerType");
            GreyMap _computerType = fieldInfo.GetValue(__instance) as GreyMap;
            if (!_computerType.ContainsKey("disconnect_network");
            {
                _computerType["disconnect_network"] = Intrinsic.GetByName("disconnect_network").GetFunc();
            }
            fieldInfo.SetValue(__instance, _computerType);
            __result = _computerType;
        }
    }
}