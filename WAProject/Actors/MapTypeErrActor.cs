using System;
using Akka.Actor;

namespace WAProject
{
	public class MapTypeErrActor : UntypedActor
	{
		protected override void OnReceive (object message)
		{
			throw new NotImplementedException ();
		}
	}
}

