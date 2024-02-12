using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.BaseFixtures {

    public class BaseFixtureMovingHead<T> : MonoBehaviour, IFixtureController where T : Enum {
        #region IFixtureController

        public List<ushort> Channels => channels;
        public Int16 Address => address;

        #endregion

        public Color color = Color.black;
        public Light targetLight;
        public float multiplicationFactor = 5f;
        public MeshRenderer meshRenderer;
        public float pan = 0f;
        public float tilt = 0f;
        public GameObject panObject;
        public GameObject tiltObject;

        public float valueDifferenceThreshold = 10f;
        public float speedDifferenceThreshold = 2f;
        public float speedStep = .5f;
        public float nominalSpeed = 5f;
        
        [SerializeField] protected List<ushort> channels;
        [SerializeField] private Int16 address = 1;

        private MovingHeadRotation _panRotation = new MovingHeadRotation();
        private MovingHeadRotation _tiltRotation = new MovingHeadRotation();

        void Start()
        {
            InitChannels();
        }

        private void InitChannels()
        {
            channels = new List<ushort>();
            foreach (var _ in Enum.GetNames(typeof(T)))
            {
                channels.Add(0);
            }
        }

        void Update()
        {
            _panRotation.ValueDifferenceThreshold = valueDifferenceThreshold;
            _panRotation.SpeedDifferenceThreshold = speedDifferenceThreshold;
            _panRotation.SpeedStep = speedStep;
            _panRotation.NominalSpeed = nominalSpeed;
            
            _tiltRotation.ValueDifferenceThreshold = valueDifferenceThreshold;
            _tiltRotation.SpeedDifferenceThreshold = speedDifferenceThreshold;
            _tiltRotation.SpeedStep = speedStep;
            _tiltRotation.NominalSpeed = nominalSpeed;

            MapChannels();
            if (targetLight is not null)
            {
                targetLight.color = color;
                var p = new MaterialPropertyBlock();
                p.SetColor("_EmissiveColor", color * multiplicationFactor);
                meshRenderer.SetPropertyBlock(p);
            }

            if (panObject is not null)
            {
                _panRotation.SetTargetValue(pan);
                panObject.transform.SetLocalPositionAndRotation(
                    panObject.transform.localPosition,
                    Quaternion.Euler(0, -_panRotation.Update(), 0)
                );
            }

            if (tiltObject is not null)
            {
                _tiltRotation.SetTargetValue(tilt);
                tiltObject.transform.SetLocalPositionAndRotation(
                    tiltObject.transform.localPosition,
                    Quaternion.Euler(-_tiltRotation.Update(), 0, 0)
                );
            }
        }

        protected virtual void MapChannels() { }
    }
}
