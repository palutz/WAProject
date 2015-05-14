using System;
using Akka.Actor;

namespace WAProject
{
	public class ReduceType1Actor : UntypedActor
	{
		protected override void OnReceive (object message)
		{
			if (message is FileMessages.ReduceType1) {
				var msg = message as FileMessages.ReduceType1;
				var storeActor = Context.ActorSelection ("/user/fileCoordinatorActor/reduceFileActor/reducedStoreActor");
				foreach(var row in msg.MapType1)
				{
					storeActor.Tell (new FileMessages.ReduceStore(row));
				}
				Sender.Tell (new FileMessages.ReduceType1End(""));  // TODO missed the filename
			} else
				Unhandled (message);
		}
	}
}

