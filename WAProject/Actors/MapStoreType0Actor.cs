using System;
using System.Collections.Generic;
using Akka.Actor;

namespace WAProject
{
	public class MapStoreType0Actor : UntypedActor
	{
		private List<int[]> _listType0 = new List<int[]>();

		protected override void OnReceive (object message)
		{
			if (message is FileMessages.StoreType0) {
				var msg = message as FileMessages.StoreType0;
				this._listType0.Add (Array.ConvertAll(msg.Row, int.Parse));
			} else if (message is FileMessages.EndOfFile) {
				var resulMsg = new FileMessages.ResultMapType0 (this._listType0);
				Sender.Tell (resulMsg);
			} else
				Unhandled (message);
		}
	}
}

