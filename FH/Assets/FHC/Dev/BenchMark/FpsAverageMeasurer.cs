using UnityEngine;
using UnityEngine.UI;

namespace FH.DevTool
{
    public class FpsAverageMeasurer : MonoBehaviour
    {
        public Text currentFPS;
        private float min = 60;

        const float fpsMeasurePeriod = 0.1f;
        private int m_FpsAccumulator = 0;
        private float m_FpsNextPeriod = 0;
        private int m_CurrentFps;
        private Text m_Text;
        private float lastUpdateTime;

        private void Awake()
        {
            lastUpdateTime = Time.realtimeSinceStartup;
            m_FpsNextPeriod = lastUpdateTime + fpsMeasurePeriod;
        }

        private void Update()
        {
            // measure average frames per second
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)
            {
                m_CurrentFps = (int)(m_FpsAccumulator / (Time.realtimeSinceStartup - lastUpdateTime));
                m_FpsAccumulator = 0;
                currentFPS.text = m_CurrentFps.ToFixedString();
                lastUpdateTime = Time.realtimeSinceStartup;
                m_FpsNextPeriod = lastUpdateTime + fpsMeasurePeriod;
            }
        }
    }

}