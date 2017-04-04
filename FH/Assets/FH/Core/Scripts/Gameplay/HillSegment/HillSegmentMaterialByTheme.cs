using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FH.Gameplay
{
    [RequireComponent(typeof(HillSegmentPoints), typeof(Renderer))]
    public class HillSegmentMaterialByTheme : MonoBehaviour
    {
        [SerializeField]
        Material fallbackMaterial;
        [SerializeField]
        List<Material> materialsByTheme = new List<Material>();

        HillSegmentPoints hillSegmentPoints;
        new Renderer renderer;

        public void Awake()
        {
            hillSegmentPoints = GetComponent<HillSegmentPoints>();
            renderer = GetComponent<Renderer>();

            hillSegmentPoints.OnPointsSet += HillSegmentPoints_OnPointsSet;

            SetMaterial();
        }

        private void HillSegmentPoints_OnPointsSet()
        {
            SetMaterial();
        }

        void SetMaterial()
        {
            renderer.sharedMaterial = GetCurrentMaterial();
        }

        Material GetCurrentMaterial()
        {
            if (hillSegmentPoints.ThemeId < materialsByTheme.Count)
            {
                return materialsByTheme[hillSegmentPoints.ThemeId];
            }
            else
            {
                return fallbackMaterial;
            }
        }
    }

}