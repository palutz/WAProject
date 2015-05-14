using System;
using Akka.Actor;

namespace WAProject
{
	public class ReduceType0Actor : UntypedActor
	{
		protected override void OnReceive (object message)
		{
			if (message is FileMessages.ReduceType0) {
				var msg = message as FileMessages.ReduceType0;
				var storeActor = Context.ActorSelection ("/user/fileCoordinatorActor/reduceFileActor/reducedStoreActor");
				foreach(var row in msg.MapType0)
				{
					if(ValidateMsg(row, msg.MaxValues, msg.MinValues))
						storeActor.Tell (new FileMessages.ReduceStore(row));
				}
				Sender.Tell (new FileMessages.ReduceType0End(""));  // TODO missed the filename
			} else
				Unhandled (message);
		}

		private bool ValidateMsg(int[] msg, int[] MaxValues, int[] MinValues) 
		{
			bool ret = true;
			int size = msg.Length;
			for (int i = 0; i < size && ret; i++) {
				ret &= (msg [i] >= MinValues [i]) && (msg [i] <= MaxValues [i]);
			}

			return ret;
		}
	}
}

