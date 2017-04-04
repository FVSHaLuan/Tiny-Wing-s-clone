using FH.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH.Test
{
    public class Test_NearestNode : MonoBehaviour
    {
        [SerializeField]
        UnityEngine.UI.Text displayText;

        PlayerCurrentNearestNode playerCurrentNearestNode;

        public void Awake()
        {
            playerCurrentNearestNode = GameplayEntry.Instance.Player.CurrentNearestNode;
        }

        public void Update()
        {
            if (!playerCurrentNearestNode.DataReady)
            {
                return;
            }

            int nearestNodeId = playerCurrentNearestNode.NearestNodeId;
            NodeData nearestNodeData = playerCurrentNearestNode.NearestNodeData;

            displayText.text = string.Format("{0} - {1} - {2}", nearestNodeId, nearestNodeData.LastExtremeness, nearestNodeData.LastExtremeNodeInterval);
        }
    }

}