using System;
using Akka.Actor;

namespace WAProject
{
	/// <summary>
	/// Coordinator actor for the file rows mapping
	/// </summary>
	public class MapRowActor : UntypedActor
	{
		private IActorRef _mapType0;
		private IActorRef _mapType1;
		private IActorRef _mapTypeErr;

		protected override void PreStart ()
		{
			Props mapRow0Props = Props.Create (() => new MapType0Actor ());
			_mapType0 = Context.ActorOf (mapRow0Props, "mapType0Actor");

			Props mapRow1Props = Props.Create (() => new MapType1Actor ());
			_mapType1 = Context.ActorOf (mapRow1Props, "mapType1Actor");

			Props mapRowError = Props.Create (() => new MapTypeErrActor ());
			_mapTypeErr = Context.ActorOf (mapRowError, "mapTypeError");
		}

		protected override void OnReceive (object message)
		{
			if (message is FileMessages.RowFile) {
				var msg = message as FileMessages.RowFile;
				int len = msg.Row.Length;
				if (len > 0) {
					if (msg.Row [len - 1] == 0) {
						_mapType0.Tell (msg);
					} else if (msg.Row [len - 1] == 1) {
						_mapType1.Tell (msg);
					} else
						_mapTypeErr.Tell (msg);
				} else {
					_mapTypeErr.Tell (msg);
				}
			} else
			{
				Unhandled(message);
			}
		}
	}
}

