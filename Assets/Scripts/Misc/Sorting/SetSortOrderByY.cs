using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Misc.Utility
{
    [ExecuteAlways]
    public class SetSortOrderByY : SetSortOrderByYAbs
    {
        [SerializeField] List<SpriteRenderer> srs = new List<SpriteRenderer>();

        protected override void Reset()
        {
            base.Reset();
            srs = GetComponentsInChildren<SpriteRenderer>().ToList();
        }
        protected override void Start()
        {
            base.Start();
            if (srs == null || srs.Count == 0)
            {
                srs = GetComponentsInChildren<SpriteRenderer>().ToList();
            }
        }
        protected override void UpdateSortOrder(int i)
        {
            foreach (SpriteRenderer sr in srs)
            {
                sr.sortingOrder = i;
            }
        }
    }
}