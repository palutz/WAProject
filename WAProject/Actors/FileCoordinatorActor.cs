﻿using System;
using System.IO;
using Akka.Actor;


namespace WAProject
{
	public class FileCoordinatorActor : UntypedActor
	{
		private IActorRef _fileReaderActor;
		private IActorRef _mapFileRow;


		protected override void PreStart ()
		{
			Props fileReaderProps = Props.Create (() => new FileReaderActor ());
			_fileReaderActor = Context.ActorOf (fileReaderProps, "fileReaderActor");

			Props mapFileRow = Props.Create (() => new MapRowActor ());
			_mapFileRow = Context.ActorOf (mapFileRow, "mapFileActor");
		}

		/// <summary>
		/// Main Actor acting as a coordinator on the file activities
		///   - open a file to read (send to the fileReaderActor)
		///   - one row read from the file (send to the MapRowActor)
		/// </summary>
		/// <param name="message">The message.</param>
		protected override void OnReceive (object message)
		{
			if (message is FileMessages.ReadFile) {  // received the message that ask to read a file...
				var msg = message as FileMessages.ReadFile;
				if (ValidateFile (msg.FileName)) {
					_fileReaderActor.Tell (msg);
				}	
			} else if (message is FileMessages.RowFile) {  // received the message with a row read from a file...
				// TODO... Log, Statiscs, keep alive... 
			} else if (message is FileMessages.EndOfFile) {
				// TODO ... 
			} else
			{
				Unhandled(message);
			}
		}

		private bool ValidateFile(string fpath)
		{
			return File.Exists (fpath);
		}
	}
}
