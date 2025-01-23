using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Misc.Utility
{
    [ExecuteAlways]
    public class SetSortOrderByYParticles : SetSortOrderByYAbs
    {
        [SerializeField] List<ParticleSystem> particles = new List<ParticleSystem>();
        List<ParticleSystemRenderer> renderers = new List<ParticleSystemRenderer>();

        protected override void Reset()
        {
            base.Reset();
            particles = GetComponentsInChildren<ParticleSystem>().ToList();
        }
        protected override void Start()
        {
            base.Start();
            if (particles == null || particles.Count == 0)
            {
                particles = GetComponentsInChildren<ParticleSystem>().ToList();
            }
            foreach (var p in particles)
            {
                renderers.Add(p.GetComponent<ParticleSystemRenderer>());
            }
        }
        protected override void UpdateSortOrder(int i)
        {
            foreach (ParticleSystemRenderer r in renderers)
            {
                r.sortingOrder = i;
            }
        }
    }
}