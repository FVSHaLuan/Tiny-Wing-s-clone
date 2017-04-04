using UnityEngine;
using System.Collections;

namespace FH.Core.Architecture.ParametersTable
{
    public interface IParametersTableChangeListener
    {
        void OnParametersTableChanged(IParametersTable parametersTable);
    }

}