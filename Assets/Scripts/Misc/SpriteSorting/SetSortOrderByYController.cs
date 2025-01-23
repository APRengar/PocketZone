using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Misc.Utility
{
	/// <summary>
	/// this class, handles detecting characters that are on Same Y, and then use 
	/// the 'myOffset' variables, based on the 
	/// </summary>
	public class SetSortOrderByYController : MonoBehaviour
	{
		[Serializable]
		public class ItemsOnY
		{
			public const float removalDur = 10;
			public const float sameYTolerance = 0.025f;
				
			public float yCoordinate = 0;
			public List<SetSortOrderByYAbs> sorters = new List<SetSortOrderByYAbs>();
			Dictionary<SetSortOrderByYAbs, float> sortersToDiffYDur = new Dictionary<SetSortOrderByYAbs, float>();
			Dictionary<SetSortOrderByYAbs, int> occupiedVals = new Dictionary<SetSortOrderByYAbs, int>();
			List<SetSortOrderByYAbs> sortersToRemove = new List<SetSortOrderByYAbs>();
			
			public ItemsOnY() { }
			public ItemsOnY(float yCoordinate)
			{
				this.yCoordinate = yCoordinate;
			}
			public bool IsSameY(float y)
			{
				return Mathf.Abs(yCoordinate - y) < sameYTolerance;
			}
			public bool HasSorter(SetSortOrderByYAbs sorter)
			{
				return sorters.Contains(sorter);
			}
			public void TryAdd(SetSortOrderByYAbs sorter)
			{
				if (sorters.Contains(sorter))
					return;
				
				sorters.Add(sorter);
				sortersToDiffYDur[sorter] = 0;
				int newOffset = sorters.Count;
					
				//calculate the smallest possible new unique offset.
				while (IsSorterOffsetInUse(newOffset))
				{
					int testOffset = newOffset - 1;
					if(!IsSorterOffsetInUse(testOffset))
					{
						newOffset = testOffset;
						break;
					}
					testOffset = newOffset + 1;
					if(!IsSorterOffsetInUse(testOffset))
					{
						newOffset = testOffset;
						break;
					}
				}
				occupiedVals[sorter] = newOffset;
			}
			public void Remove(SetSortOrderByYAbs sorter)
			{
				if (!sorters.Contains(sorter))
					return;
				
				if (sorter)
					sorter.myOffset = 0;
				
				sorters.Remove(sorter);
				sortersToDiffYDur.Remove(sorter);
				occupiedVals.Remove(sorter);
			}
			public void Update()
			{
				sortersToRemove.Clear();
				
				for (int i = 0; i < sorters.Count; i++) 
				{
					SetSortOrderByYAbs sorter = sorters[i];
					
					// nulls are removed at the end of this update.
					if (sorter == null)
						continue;
					
					float sorterY = sorter.transform.position.y;
					
					// if we are in same y, we just apply the offset
					if (IsSameY(sorterY)) 
					{
						sortersToDiffYDur[sorter] = 0;
						sorter.myOffset = occupiedVals[sorter];
						if(sorter.UsesCustomOffset)
							sorter.myOffset = sorter.CustomOffset;
						continue;
					}
					
					// we are NOT in same Y, but we are in group,
					// we wait until it is disabled, or until time runs out, then we remove
					sorter.myOffset = 0;
					sortersToDiffYDur[sorter] = sortersToDiffYDur[sorter] + Time.deltaTime;
					if (sortersToDiffYDur[sorter] >= removalDur || !sorters[i].gameObject.activeInHierarchy)
					{
						sortersToRemove.Add(sorters[i]);
					}
				}
				
				// remove all elements that are queued for removal
				foreach (SetSortOrderByYAbs sorter in sortersToRemove)
				{
					Remove(sorter);
				}
				
				sorters.RemoveAll(s=>
				{
					if (s == null)
					{
						Remove(s);
						return true;
					}
					return false;
				});
			}
			private bool IsSorterOffsetInUse(int newOffset)
			{
				foreach (KeyValuePair<SetSortOrderByYAbs,int> kv in occupiedVals)
				{
					if (kv.Value == newOffset)
					{
						return true;
					}
				}
				return false;
			}
		}
			
		public static SetSortOrderByYController instance
		{
			get
			{
				if(s_instance == null)
					s_instance = new GameObject("SetSortOrderByYController").AddComponent<SetSortOrderByYController>();
				return s_instance;
			}
		}
		public static SetSortOrderByYController s_instance = null;
			
		[Header("Read Only")]
		public List<SetSortOrderByYAbs> allSorters = new List<SetSortOrderByYAbs>();
		public List<ItemsOnY> itemsOnY = new List<ItemsOnY>();
			
		void Update()
		{
			// Ensure all sorters have their own 'ItemOnY'
			foreach (SetSortOrderByYAbs sorter in allSorters)
			{
				// Static elements, do not need to be splitted on same y coordinate.
				if (sorter.IsStaticSR)
					continue;
				
				float sorterY = sorter.transform.position.y;
				ItemsOnY itemOnY = GetItemsOnY(sorter);
				if (itemOnY == null)
				{
					itemsOnY.Add(new ItemsOnY(sorterY));
					itemOnY = itemsOnY[itemsOnY.Count - 1];
				}
				itemOnY.TryAdd(sorter);
			}
			
			// Update all 'ItemsOnY' group that traccks which items are on curr y pos
			foreach (ItemsOnY item in itemsOnY)
			{
				item.Update();
			}
			
			// remove all empty groups
			itemsOnY.RemoveAll(item => item.sorters.Count == 0);
		}
		public void Add(SetSortOrderByYAbs s)
		{
			allSorters.Add(s);
		}
		public void Remove(SetSortOrderByYAbs sorter)
		{
			allSorters.Remove(sorter);
			//Clean up the removed sorter from Items On Y
			foreach (ItemsOnY item in itemsOnY)
			{
				item.Remove(sorter);
			}
		}
		
		private ItemsOnY GetItemsOnY(SetSortOrderByYAbs sorter)
		{
			foreach (ItemsOnY item in itemsOnY)
			{
				if (item.IsSameY(sorter.transform.position.y) || item.HasSorter(sorter))
				{
					return item;
				}
			}
			return null;
		}
	}
}