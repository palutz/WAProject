using System;
using System.IO;
using Akka.Actor;

namespace WAProject
{
	public class FileWriterActor : UntypedActor
	{
		protected override void OnReceive (object message)
		{
			if (message is FileMessages.ReduceResult) {
				//
				// TODO just write all the rows in the file...

			} else
			{
				Unhandled(message);
			}
		}
	}
}

