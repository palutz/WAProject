using System;
using Akka.Actor;

namespace WAProject
{
	public class ReduceType1Actor : UntypedActor
	{
		protected override void OnReceive (object message)
		{
			if (message is FileMessages.ReduceType1) {
			} else
				Unhandled (message);
		}
	}
}

