using System;
using Akka.Actor;

namespace WAProject
{
	public class MapType1Actor : UntypedActor
	{
		protected override void OnReceive (object message)
		{
			if (message is FileMessages.RowFile) {
				var msg = message as FileMessages.RowFile;
				Context.ActorSelection ("/user/fileCoordinatorActor/mapFileActor/mapStoreType1Actor").Tell (msg);
				Context.ActorSelection ("/user/fileCoordinatorActor/mapFileActor/mapMinMaxActor").Tell (msg);

				// Semder.Tell --- done
			} else
				Unhandled (message);
		}
	}
}

