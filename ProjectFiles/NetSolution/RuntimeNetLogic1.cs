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

public class RuntimeNetLogic1 : BaseNetLogic
{
    private PeriodicTask myPeriodicTask;
    private IUAVariable varPLC;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        Log.Error("avvio progetto!");
        myPeriodicTask = new PeriodicTask(IncrementVariable, 1000, LogicObject);
        myPeriodicTask.Start();

        varPLC = Project.Current.GetVariable("Model/VarPLC");
        varPLC.VariableChange += VarPLC_VariableChange;
    }

    private void VarPLC_VariableChange(object sender, VariableChangeEventArgs e)
    {
        if (e.NewValue == 10)
            varPLC.Value = 0;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        Log.Error("stop progetto!");
        myPeriodicTask.Dispose();
        varPLC.VariableChange -= VarPLC_VariableChange;
    }

    [ExportMethod]
    public void mioMetodo(string testoMessaggio)
    {
        Log.Info(testoMessaggio);
    }

    private void IncrementVariable()
    {
        var variableCSharp = LogicObject.GetVariable("Variable1");
        variableCSharp.Value = variableCSharp.Value + 1; 
    }
}
