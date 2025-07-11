using UnityEngine;

namespace Game.Particle
{
    public class ParticleToRadiusScale : MonoBehaviour
    {
        [SerializeField] private ParticleToRadiusScaleData[] particleDatas;

        private void Awake()
        {
            for (int i = 0; i < particleDatas.Length; i++)
                particleDatas[i].SetBaseSize();
        }

        public void UpdateParticleScale(float radius)
        {
            for (int i = 0; i < particleDatas.Length; i++)
                particleDatas[i].Scale(radius);
        }

        [System.Serializable]
        private class ParticleToRadiusScaleData
        {
            [SerializeField] private ParticleSystem particleSystem;
            [SerializeField] private float radiusScale;

            private float baseSize;

            public void SetBaseSize()
            {
                var main = particleSystem.main;
                baseSize = main.startSize.constant;
            }

            public void Scale(float raius)
            {
                var main = particleSystem.main;
               
                main.startSize = raius * radiusScale;
            }
        }
    }
}