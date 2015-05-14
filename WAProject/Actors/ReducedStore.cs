using System;
using Akka.Actor;
using System.Collections.Generic;

namespace WAProject
{
	public class ReducedStore : UntypedActor
	{
		private List<string> _store = new List<string> ();

		protected override void OnReceive (object message)
		{
			if (message is FileMessages.ReduceStore) {
				var msg = message as FileMessages.ReduceStore;
				var arStr = Array.ConvertAll(msg.storeValue, el => el.ToString());
				this._store.Add (string.Join(",", arStr)); // convert back to CSV
			} else if(message is FileMessages.ReduceResult)
			{
				Context.ActorSelection ("/user/fileCoordinatorActor/fileWriterActor").Tell (new FileMessages.ReduceResult (this._store));
			} else
				Unhandled (message);
		}
	}
}

