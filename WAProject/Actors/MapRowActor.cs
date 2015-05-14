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
		private IActorRef _mapStoreType1;
		private IActorRef _mapStoreType0;
		private IActorRef _mapMinMax;

		protected override void PreStart ()
		{
			Props mapRow0Props = Props.Create (() => new MapType0Actor ());
			_mapType0 = Context.ActorOf (mapRow0Props, "mapType0Actor");

			Props mapRow1Props = Props.Create (() => new MapType1Actor ());
			_mapType1 = Context.ActorOf (mapRow1Props, "mapType1Actor");

			Props mapRowErrorProp = Props.Create (() => new MapTypeErrActor ());
			_mapTypeErr = Context.ActorOf (mapRowErrorProp, "mapTypeErrorActor");

			Props mapStoreT0Prop = Props.Create (() => new MapStoreType0Actor ());
			_mapStoreType0 = Context.ActorOf (mapStoreT0Prop, "mapStoreType0Actor");

			Props mapStoreT1Prop = Props.Create (() => new MapStoreType1Actor ());
			_mapStoreType1 = Context.ActorOf (mapStoreT1Prop, "mapStoreType1Actor");

			Props mapMinMaxProp = Props.Create (() => new MapMinMaxActor ());
			_mapMinMax = Context.ActorOf (mapStoreT1Prop, "mapMinMaxActor");
		}

		protected override void OnReceive (object message)
		{
			if (message is FileMessages.FirstRow) {
				// TODO ... manage the scenario in which the column are not in a fixed position
			}
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

