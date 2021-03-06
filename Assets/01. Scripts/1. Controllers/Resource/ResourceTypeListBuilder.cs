﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JK.GameData;
using JK.View;


namespace JK
{
	namespace Controller
	{


		public class ResourceTypeListBuilder : MonoBehaviour
		{
			protected List<ResourceType> resourceTypes;
			public Register register;
			public ResourceTypeListDisplay resourceListDisplayPrefab;
			public Transform target;
			public ResourceTypeDisplay resourceTypeDeleteDisplay;

			ResourceTypeListDisplay TypeSelect;
			List<ResourceTypeDisplay> resourceDisplayList = new List<ResourceTypeDisplay> ();
			List<ResourceType> availableResourceTypes;

			public delegate void ResourceTypeListUpdate (List<ResourceType> _resourceTypes);

			public event ResourceTypeListUpdate onListUpdate;

			void ListUpdate ()
			{
				if (onListUpdate != null)
					onListUpdate.Invoke (resourceTypes);

			}


			#region Event Callers




			void OnTypeSelect (ResourceType _resourceType)
			{
				if (resourceTypes == null)
					resourceTypes = new List<ResourceType> ();

				if (!resourceTypes.Contains (_resourceType))
					resourceTypes.Add (_resourceType);


				Prime (resourceTypes);
			}



			#endregion

			public void SetResourceTypes (List<ResourceType> _ResourceTypes)
			{
				availableResourceTypes.Clear ();
				availableResourceTypes.AddRange (_ResourceTypes);
			}

			public void Prime (List<ResourceType> _resourcesTypes)
			{
				clearList ();
				resourceTypes = _resourcesTypes;
				foreach (var resourceType in resourceTypes)
				{
					ResourceTypeDisplay listItem = (ResourceTypeDisplay)Instantiate (resourceTypeDeleteDisplay);
					listItem.transform.SetParent (target, false);
					listItem.Prime (resourceType);
					listItem.gameObject.tag = "ResourceDisplay";
					resourceDisplayList.Add (listItem);	
					listItem.onDelete += onDeleteResource;
				}
				ListUpdate ();
			}

			public void AddResourceType ()
			{
				TypeSelect = (ResourceTypeListDisplay)Instantiate (resourceListDisplayPrefab);
				TypeSelect.Prime (availableResourceTypes);
				TypeSelect.onClick += onTypeSelect;
				TypeSelect.onClose += closeTypeSelect;

			}

			void closeTypeSelect ()
			{
				TypeSelect.onClick -= onTypeSelect;
				TypeSelect.onClose -= closeTypeSelect;
				TypeSelect.destroy ();
			}

			void onTypeSelect (ResourceType _resourceType)
			{
				if (resourceTypes == null)
					resourceTypes = new List<ResourceType> ();

				if (!resourceTypes.Contains (_resourceType))
					resourceTypes.Add (_resourceType);

				TypeSelect.onClick -= onTypeSelect;
				TypeSelect.onClose -= closeTypeSelect;
				TypeSelect.destroy ();
				Prime (resourceTypes);

			}

			void onDeleteResource (ResourceType _resourceType)
			{
				if (resourceTypes.Contains (_resourceType))
					resourceTypes.Remove (_resourceType);

				Prime (resourceTypes);
			}

			void Awake ()
			{
				availableResourceTypes = new List<ResourceType> ();
				availableResourceTypes.AddRange (register.AllResources);
			}



			void clearList ()
			{

				for (int i = 0; i < target.childCount; i++)
				{
					if (target.GetChild (i).gameObject.tag == "ResourceDisplay")
					{
						Destroy (target.GetChild (i).gameObject);
					}
				}

			}
		
		}
	}
}

