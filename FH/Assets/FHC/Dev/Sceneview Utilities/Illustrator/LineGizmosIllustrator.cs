using UnityEngine;
using System.Collections;
using System;
using FH.Core.Architecture.ParametersTable;

namespace FH.DevTool.SceneViewUtility
{
    public class LineGizmosIllustrator : GizmosIllustrator, IParametersTableChangeListener
    {
        [SerializeField]
        FloatParameterGetter offsetX = new FloatParameterGetter();
        [SerializeField]
        FloatParameterGetter offsetY = new FloatParameterGetter();
        [SerializeField]
        FloatParameterGetter leftLength = new FloatParameterGetter();
        [SerializeField]
        FloatParameterGetter rightLength = new FloatParameterGetter();
        [SerializeField]
        FloatParameterGetter upLength = new FloatParameterGetter();
        [SerializeField]
        FloatParameterGetter downLength = new FloatParameterGetter();

        protected override void DrawGizmos()
        {
            Vector2 startPoint = TargetTransform.position;
            startPoint.x += offsetX.Value;
            startPoint.y += offsetY.Value;

            Gizmos.DrawLine(startPoint, startPoint + Vector2.left * leftLength);
            Gizmos.DrawLine(startPoint, startPoint + Vector2.right * rightLength);
            Gizmos.DrawLine(startPoint, startPoint + Vector2.up * upLength);
            Gizmos.DrawLine(startPoint, startPoint + Vector2.down * downLength);
        }

        public void OnParametersTableChanged(IParametersTable parametersTable)
        {
            offsetX.UpdateValue(parametersTable);
            offsetY.UpdateValue(parametersTable);
            leftLength.UpdateValue(parametersTable);
            rightLength.UpdateValue(parametersTable);
            upLength.UpdateValue(parametersTable);
            downLength.UpdateValue(parametersTable);

            ForceDraw();
        }
    }

}