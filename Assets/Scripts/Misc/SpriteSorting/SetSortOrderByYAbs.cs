using UnityEngine;
using System.Collections.Generic;
using System;

namespace Misc.Utility
{
	[ExecuteAlways]
    public abstract class SetSortOrderByYAbs : MonoBehaviour
	{
		public bool IsStaticSR => isStatic;
		public int CustomOffset => customOffset;
		public bool UsesCustomOffset => usesCustomOffset;
		
		[SerializeField] int multiplayer = -1000;
        [SerializeField] Transform transformToMatch = null;
		[SerializeField] bool isStatic = false;
		
		[Header("Same Y Offset")]
		[Tooltip("when we are on same Y, with multiple characters, and this is true, we use 'customOffset' otherwise we use index")]
		[SerializeField] bool usesCustomOffset = false;
		[Tooltip("when we are on same Y, with multiple characters, if usesCustomOffset is true, we use this value, to offset the Y")]
		[SerializeField] int customOffset = 20;
		
        [Header("Read Only")]
        [SerializeField] int i = 0;//last value we used
		public int myOffset = 0;//assigned by the controller class
		[SerializeField] int sortIndexWithNoOffset = 0;//for visualization

        protected virtual void Reset()
	    {
		    GameObject go = Extentions.GetChildWithName(gameObject, "Sort Pivot Point", false);
		    if (go)
			    return;
			
	        GameObject prefab = Resources.Load<GameObject>("Sort Pivot Point");
	        transformToMatch = GameObject.Instantiate(prefab).transform;
            transformToMatch.name = "Sort Pivot Point";
            transformToMatch.SetParent(transform);
            transformToMatch.localPosition = Vector3.zero;
	    }
	    protected virtual void OnEnable()
	    {
	    	if (!Application.isPlaying)
		    	return;
		    SetSortOrderByYController.instance.Add(this);
	    }
        protected virtual void Start()
        {
            UpdateSortOrder(i);
        }
        protected virtual void Update()
        {
            if (isStatic)
                return;
	        CalculateSortOrder();
            UpdateSortOrder(i);
        }
	    protected virtual void OnDisable()
	    {
	    	if (!Application.isPlaying)
		    	return;
		    if (!SetSortOrderByYController.s_instance)
			    return;
	    	SetSortOrderByYController.s_instance.Remove(this);
	    }
        private void CalculateSortOrder()
        {
	        sortIndexWithNoOffset = (int)(transformToMatch.position.y * multiplayer);
	        i = sortIndexWithNoOffset;
	        if (!Application.isPlaying)
		        return;
	        i = sortIndexWithNoOffset + myOffset;
        }
        protected abstract void UpdateSortOrder(int i);
    }
}