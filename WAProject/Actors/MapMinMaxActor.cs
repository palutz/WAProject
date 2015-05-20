using System;
using System.Collections;
using Akka.Actor;

namespace WAProject
{
	public class MapMinMaxActor : UntypedActor
	{
		private ArrayList _minMapped;
		private ArrayList _maxMapped;

		protected override void OnReceive (object message)
		{
			if (message is FileMessages.FirstRow) {
				var msg = message as FileMessages.RowFile;
				Initialize (msg.Row.Length);
			} else if (message is FileMessages.RowFile) {
				var msg = message as FileMessages.RowFile;
				// mybe to split in 2 other actors (min and max)
				for (int i = 1; i < msg.Row.Length - 1; i++) {
					var aValue = Convert.ToInt32 (msg.Row [i]);
					if (this._maxMapped [i] != null) {
						if (aValue > (int)this._maxMapped [i])
							this._maxMapped[i] = aValue;
					} else
						this._maxMapped [i] = aValue;
					
					if (this._minMapped [i] != null) {
						if (aValue < (int)this._minMapped [i]	)
							this._minMapped [i] = aValue;
					} else
						this._minMapped [i] = Convert.ToInt32 (msg.Row [i]);
				} 
			} else if(message is FileMessages.AskMaxMinValue) {
				Sender.Tell(new FileMessages.ResultMaxMinValue(
					(int[]) this._maxMapped.ToArray( typeof( int )),
					(int[]) this._minMapped.ToArray( typeof( int ))
				));
			} else
				Unhandled (message);
		}

		private void Initialize(int size){
			this._maxMapped = new ArrayList (size);
			this._minMapped = new ArrayList (size);

			for (int i = 0; i < size; i++ ) {
				this._maxMapped [i] = null;
				this._minMapped [i] = null;
			}
		}
	}
}

