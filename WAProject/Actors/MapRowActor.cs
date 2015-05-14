using System;
using System.Collections.Generic;
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
		List<int[]> _mapResType0;
		List<int[]> _mapResType1;
		int[] _minValues;
		int[] _maxValues;
		bool _mappedMaxMin = false;

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
				this._mapResType0 = null;
				this._mapResType1 = null;
				this._mapMAxMin = false;
				_mapMinMax.Tell(message); // initialize the mapper
			} else  if (message is FileMessages.RowFile) {
				var msg = message as FileMessages.RowFile;
				int len = msg.Row.Length;
				if (len > 0) {
					if (String.Compare (msg.Row [len - 1], "0") == 0) {
						_mapType0.Tell (msg);
					} else if (String.Compare (msg.Row [len - 1], "1") == 0) {
						_mapType1.Tell (msg);
					} else
						_mapTypeErr.Tell (msg);
				} else {
					_mapTypeErr.Tell (msg);
				}
			} else if (message is FileMessages.EndOfFile) {
				// TODO check properly if all the computation mapping is finished
				string fName = ((FileMessages.EndOfFile)message).FileName;
				_mapMinMax.Tell(new FileMessages.AskMaxMinValue (fName));
				_mapStoreType0.Tell (new FileMessages.EndOfFile (message));
				_mapStoreType1.Tell (new FileMessages.EndOfFile (message));
			} else if(message is FileMessages.ResultMaxMinValue) {  // rethink a better way to verify the map is ended
				var msg = message as FileMessages.ResultMaxMinValue;
				this._maxValues = msg.MaxValue;
				this._minValues = msg.MinValue;
				this._mappedMaxMin = true;
				if(IsMappingEnded())
					Sender.Tell(new FileMessages.MapProcessEnded(this._mapType0, this._mapType1, this._minValues, this._maxValues));
			} else if(message is FileMessages.ResultMapType0) {
				var msg = message as FileMessages.ResultMapType0;
				this._mapType0 = msg.MapResult;
				if(IsMappingEnded())
					Sender.Tell(new FileMessages.MapProcessEnded(this._mapType0, this._mapType1, this._minValues, this._maxValues));
			} else if(message is FileMessages.ResultMapType1) {
				var msg = message as FileMessages.ResultMapType1;
				this._mapType1 = msg.MapResult;
				if(IsMappingEnded())
					Sender.Tell(new FileMessages.MapProcessEnded(this._mapType0, this._mapType1, this._minValues, this._maxValues));
			} else {
				Unhandled(message);
			}
		}

		private bool IsMappingEnded()
		{
			return (this._mapResType0 != null) && (this._mapResType1 != null) && this._mappedMaxMin; // are all the mapping endned?
		}
	}
}

