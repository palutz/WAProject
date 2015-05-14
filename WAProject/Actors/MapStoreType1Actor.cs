using System;
using System.Collections.Generic;
using Akka.Actor;

namespace WAProject
{
	public class MapStoreType1Actor : UntypedActor
	{
		private List<int[]> _listType1 = new List<int[]>();

		protected override void OnReceive (object message)
		{
			if (message is FileMessages.StoreType1) {
				var msg = message as FileMessages.StoreType1;
				this._listType1.Add (Array.ConvertAll(msg.Row, int.Parse));
			} else if (message is FileMessages.EndOfFile) {
				var resulMsg = new FileMessages.ResultMapType1 (this._listType1);
				Sender.Tell (resulMsg);
			} else
				Unhandled (message);
		}
	}
}

