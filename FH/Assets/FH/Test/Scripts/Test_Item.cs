using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FH.Gameplay;
using FH.Core.Architecture.Pool;

namespace FH.Test
{
    public class Test_Item : MonoBehaviour
    {
        [SerializeField]
        int step = 5;

        [Header("Item prototypes")]
        [SerializeField]
        GeneralPoolMember generalItemPrototype;
        [SerializeField]
        GeneralPoolMember maximumItemPrototype;
        [SerializeField]
        GeneralPoolMember miniumItemPrototype;


        MultiPrototypesPool<GeneralPoolMember> generalPool;
        HillSegmentPoints hillSegmentPoints;

        List<GeneralPoolMember> spawnedItems = new List<GeneralPoolMember>();

        public void Awake()
        {
            InitializePool();

            hillSegmentPoints = GetComponent<HillSegmentPoints>();
            hillSegmentPoints.OnPointsSet += HillSegmentPoints_OnPointsSet;
        }

        private void InitializePool()
        {
            generalPool = GameplayEntry.Instance.GeneralPool;

            if (!generalPool.ContainsPrototype(generalItemPrototype.PrototypeId))
            {
                generalPool.PushPrototype(generalItemPrototype);
            }

            if (!generalPool.ContainsPrototype(maximumItemPrototype.PrototypeId))
            {
                generalPool.PushPrototype(maximumItemPrototype);

            }
            if (!generalPool.ContainsPrototype(miniumItemPrototype.PrototypeId))
            {
                generalPool.PushPrototype(miniumItemPrototype);
            }
        }

        private void HillSegmentPoints_OnPointsSet()
        {
            for (int i = 0; i < hillSegmentPoints.PointsCount; i += step)
            {
                NodeData nodeData = hillSegmentPoints.GetNodeData(i);
                GeneralPoolMember item = GetItem(nodeData.Extremeness);
                item.gameObject.SetActive(true);
                item.transform.SetParent(transform);
                item.transform.localPosition = hillSegmentPoints.GetPoint(i);
                item.transform.right = nodeData.Velocity;
                spawnedItems.Add(item);
            }
        }

        GeneralPoolMember GetItem(Extremeness extreme)
        {
            switch (extreme)
            {
                case Extremeness.None:
                    return generalPool.TakeInstance(generalItemPrototype.PrototypeId, true);
                case Extremeness.Maximum:
                    return generalPool.TakeInstance(maximumItemPrototype.PrototypeId, true);
                case Extremeness.Minimum:
                    return generalPool.TakeInstance(miniumItemPrototype.PrototypeId, true);
                default:
                    throw new System.NotImplementedException();
            }
        }

        public void OnDisable()
        {
            for (int i = 0; i < spawnedItems.Count; i++)
            {
                var item = spawnedItems[i];
                item.gameObject.SetActive(false);
                generalPool.PushInstance(item);
            }
        }
    }

}