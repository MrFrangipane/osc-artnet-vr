using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts {

    public class BaseFixtureSimpleRGB<T> : MonoBehaviour, IFixtureController where T : Enum {
        #region IFixtureController

        public List<ushort> Channels => channels;
        public Int16 Address => address;

        #endregion

        public Color color = Color.black;
        public Light targetLight;
        public float multiplicationFactor = 5f;
        public MeshRenderer meshRenderer;
        
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
            if (targetLight != null) {
                targetLight.color = color;
                var p = new MaterialPropertyBlock();
                p.SetColor("_EmissiveColor", color * multiplicationFactor);
                meshRenderer.SetPropertyBlock(p);
            }
        }
        
        protected virtual void MapChannels() { }
    }
}
