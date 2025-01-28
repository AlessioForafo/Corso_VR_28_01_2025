#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.WebUI;
using FTOptix.Alarm;
using FTOptix.Recipe;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.ODBCStore;
using FTOptix.OPCUAServer;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.Core;
#endregion

public class DesignTimeNetLogic1 : BaseNetLogic
{
    [ExportMethod]
    public void Test()
    {
        // Insert code to be executed by the method
        Log.Info("This is an info message");
        Log.Info(LogicObject.GetVariable("MessaggioLog").Value);
    }

    [ExportMethod]
    public void CreaVariabili()
    {
        // Insert code to be executed by the method
        var myVar = InformationModel.MakeVariable("MyVar", OpcUa.DataTypes.Float);
        Project.Current.Get("Model/VariabiliCreateDaCodice").Add(myVar);
    }

    [ExportMethod]
    public void CreaAllarmi()
    {
        for (uint i = 0; i < 10; i++)
        {
            var mioAllarme = InformationModel.Make<DigitalAlarm>("Allarme_" +  i);
            mioAllarme.Message = "Testo allarme " + i;
            mioAllarme.InputValueVariable.SetDynamicLink(Project.Current.GetVariable("Model/Allarmi"), i, DynamicLinkMode.Read);
            Project.Current.Get("Alarms").Add(mioAllarme);
        }
    }

 }
