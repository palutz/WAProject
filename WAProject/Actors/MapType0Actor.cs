using System;
using Akka.Actor;

namespace WAProject
{
	public class MapType0Actor : UntypedActor
	{
		protected override void OnReceive (object message)
		{
			if (message is FileMessages.RowFile) {
				var msg = message as FileMessages.RowFile;
				Context.ActorSelection ("/user/fileCoordinatorActor/mapFileActor/mapStoreType0Actor").Tell (msg);

				// Semder.Tell --- done
			} else
				Unhandled (message);
		}
	}
}

