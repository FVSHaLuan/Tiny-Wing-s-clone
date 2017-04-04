using UnityEngine;
using System.Collections;
using System;
using FH.Core.Architecture.ParametersTable;

namespace FH.DevTool
{
    public class ParametersTableChangeDetector : EditorChangeDetector, IParametersTableChangeListener
    {
        public void OnParametersTableChanged(IParametersTable parametersTable)
        {
            DetectedChanged();
        }

        public override void OnReset()
        {

        }
    }

}