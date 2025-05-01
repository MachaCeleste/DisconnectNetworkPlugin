using HarmonyLib;
using Miniscript;
using NetworkMessages;
using System.Collections.Generic;

[HarmonyPatch]
class ComputerIntrinsicsPatch
{
    private static bool intrinsicsAdded;

    [HarmonyPatch(typeof(ComputerIntrinsics), "AddInstrinsics")]
    static void Postfix()
    {
        if (intrinsicsAdded == true)
            return;
        intrinsicsAdded = true;
        Intrinsic intrinsic = Intrinsic.Create("disconnect_network");
        intrinsic.AddParam("self");
        intrinsic.code = delegate(TAC.Context context, Intrinsic.Result partialResult)
        {
            GreyMap greyMap = context.GetVar("self") as GreyMap;
            if (greyMap != null)
            {
                GreyInterpreter greyInterpreter = (GreyInterpreter)context.interpreter;
                var computer = greyInterpreter.hostData.GetComputer(greyMap).GetComputer();
                if (computer != null)
                {
                    var router = computer.GetMainRouter();
                    if (router != null)
                    {
                        var networkLan = router.GetMainRouter().GetNetworkLan();
                        if (networkLan != null)
                            networkLan.DisconnectComputer(computer);
                        MessageClient messageClient = new MessageClient(IdClient.EnableEthernetClientRpc);
                        messageClient.AddString(new List<string> { "Network Disconnected", "" });
                        messageClient.AddBool(false);
                        greyInterpreter.SendData(messageClient);
                        return Intrinsic.Result.True;
                    }
                    return new Intrinsic.Result("Unable to get router");
                }
                return new Intrinsic.Result("Unable to get computer");
            }
            return Intrinsic.Result.Null;
        };
    }
}