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

        [SerializeField] protected List<ushort> channels;
        [SerializeField] private Int16 address = 1;

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
                panObject.transform.SetLocalPositionAndRotation(
                    panObject.transform.localPosition,
                    Quaternion.Euler(0, pan, 0)
                );
            }

            if (tiltObject is not null)
            {
                tiltObject.transform.SetLocalPositionAndRotation(
                    tiltObject.transform.localPosition,
                    Quaternion.Euler(tilt, 0, 0)
                );
            }
        }

        protected virtual void MapChannels() { }
    }
}
