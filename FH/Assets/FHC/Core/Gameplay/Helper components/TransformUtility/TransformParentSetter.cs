using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FH.Core.Gameplay.HelperComponent
{
    public class TransformParentSetter : MonoBehaviour
    {
        [SerializeField]
        Transform targetTransform;

        [Header("TransformParentSetter")]
        [SerializeField]
        List<Transform> parentsList = new List<Transform>();

        public void SetToRoot()
        {
            targetTransform.parent = null;
        }

        public void SetParent(int index)
        {
            targetTransform.SetParent(parentsList[index]);
        }
        public void SetParentWithoutChangingTransform(int index)
        {
            targetTransform.SetParent(parentsList[index], false);
        }

    }

}