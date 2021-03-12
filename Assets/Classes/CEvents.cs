using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public delegate void SubscriberFunction();

	public enum EEventType
	{
		eRestartGameEvent
	}
		
	public class CEvents

	
	{
		public void Register(EEventType mEventType, SubscriberFunction mSubscriberFunction)
		{
			FunctionList(mEventType).Add(mSubscriberFunction);
		}
		public void Unregister(EEventType mEventType, SubscriberFunction mSubscriberFunction)
		{
			FunctionList(mEventType).Remove(mSubscriberFunction);
		}

		public void Invoke(EEventType mEventType)
		{
			foreach (SubscriberFunction mSubscriberFunction in FunctionList(mEventType))
			{
				mSubscriberFunction.Invoke();
			}
		}

		protected Dictionary<EEventType, List<SubscriberFunction>> mDictionary = new Dictionary<EEventType, List<SubscriberFunction>>();

		protected List<SubscriberFunction> FunctionList(EEventType mEventType)
		{
			if (mDictionary.ContainsKey(mEventType) == false)
			{
				mDictionary = new Dictionary<EEventType, List<SubscriberFunction>>();
			}

			if (mDictionary.ContainsKey(mEventType) == false)
			{
				mDictionary[mEventType] = new List<SubscriberFunction>();
			}

			return mDictionary[mEventType];
		}


	}


