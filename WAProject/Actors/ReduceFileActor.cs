using System;
using Akka.Actor;

namespace WAProject
{
	public class ReduceFileActor : UntypedActor
	{
		private IActorRef _reduceType0;
		private IActorRef _reduceType1;
		private IActorRef _reduceStorage;

		private bool _reduce0Ended = false;
		private bool _reduce1Ended = false;

		protected override void PreStart ()
		{
			Props reduceRow0Props = Props.Create (() => new ReduceType0Actor ());
			_reduceType0 = Context.ActorOf (reduceRow0Props, "reduceType0Actor");

			Props reduceRow1Props = Props.Create (() => new ReduceType1Actor ());
			_reduceType1 = Context.ActorOf (reduceRow1Props, "reduceType1Actor");

			Props reduceStoreProps = Props.Create (() => new ReducedStore ());
			_reduceStorage = Context.ActorOf (reduceStoreProps, "reducedStoreActor");
		}

		protected override void OnReceive (object message)
		{
			if (message is FileMessages.MapProcessEnded) {
				_reduce0Ended = false;
				_reduce1Ended = false;
				var msg = message as FileMessages.MapProcessEnded;
				var reduce0Msg = new FileMessages.ReduceType0 (msg.MapResultType0, msg.MinValues, msg.MaxValues);
				var reduce1Msg = new FileMessages.ReduceType1 (msg.MapResultType1);

				_reduceType0.Tell (reduce0Msg);
				_reduceType1.Tell (reduce1Msg);
			} else if (message is FileMessages.ReduceType0End) {
				_reduce0Ended = true;
				Self.Tell (new FileMessages.CheckReduceEnded ());
			} else if (message is FileMessages.ReduceType1End) {
				_reduce1Ended = true;
				Self.Tell (new FileMessages.CheckReduceEnded ());
			} else if (message is FileMessages.CheckReduceEnded) {
				if (ReduceJobEnded ())
					_reduceStorage.Tell (new FileMessages.CheckReduceEnded ());
			} else
				Unhandled (message);
		}

		private bool ReduceJobEnded()
		{
			return _reduce0Ended && _reduce1Ended;
		}
	}
}

