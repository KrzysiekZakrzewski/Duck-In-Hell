using System.Collections.Generic;
using UnityEngine;

namespace Game.Particle
{
    public class ParticleParameterSetter : MonoBehaviour
    {
        private Dictionary<int, ParticleSystem> particlesLUT;

        private void Awake()
        {
            particlesLUT = new()
            {
                {0, GetComponent<ParticleSystem>() }
            };

            for(int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var particleSystem = child.GetComponent<ParticleSystem>();

                particlesLUT.Add(i + 1, particleSystem);
            }
        }

        public void UpdateDatas(ParticleSetterData[] setterDatas)
        {
            foreach(var setterData in setterDatas)
            {
                if (!particlesLUT.TryGetValue(setterData.Id, out var particleData)) continue;

                var main = particleData.main;

                SetData(main, setterData.DataType, setterData.Value);
            }
        }

        private void SetData(ParticleSystem.MainModule main, ParameterDataType dataType, float value)
        {
           switch(dataType)
            {
                case ParameterDataType.Duration:
                    main.duration = value;
                    break;
                case ParameterDataType.Delay:
                    main.startDelay = value;
                    break;
                case ParameterDataType.LifeTime:
                    main.startLifetime = value;
                    break;
                case ParameterDataType.Scale:
                    main.startSize = value;
                    break;
                default:
                    break;
            }
        }
    }
    [System.Serializable]
    public class ParticleSetterData
    {
        public int Id;
        public ParameterDataType DataType;
        public bool IsConstValue;
        [ShowIf(nameof(IsConstValue), true)]public float Value;
    }
    [System.Serializable]
    public class ParticleDatas
    {
        public int Id;
        public ParticleSystem ParticleSystem;
    }
    public enum ParameterDataType
    {
        Duration,
        Delay,
        LifeTime,
        Scale
    }
}