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

public class CambioColoreRettangolo : BaseNetLogic
{
    private IUAVariable varPLC;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        varPLC = Project.Current.GetVariable("Model/VarPLC");
        varPLC.VariableChange += VarPLC_VariableChange;
        CambioColore(varPLC.Value);
        
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        varPLC.VariableChange -= VarPLC_VariableChange;
    }

    private void VarPLC_VariableChange(object sender, VariableChangeEventArgs e)
    {
        CambioColore(e.NewValue);
    }

    public void CambioColore(int numeroColore)
    {
        var mioRettangolo = Owner.Get<Rectangle>("Rectangle1");
        if (numeroColore > 10)
            mioRettangolo.FillColor = Colors.Red;
        else
            mioRettangolo.FillColor = Colors.Blue;
    }
}
