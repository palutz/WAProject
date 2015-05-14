using System;
using Akka.Actor;

namespace WAProject
{
	public class MapType1Actor : UntypedActor
	{
		protected override void OnReceive (object message)
		{
			if (message is FileMessages.RowFile) {

			} else
				Unhandled (message);
		}
	}
}

