﻿using System;
using System.IO;
using Akka.Actor;

namespace WAProject
{
	public class FileReaderActor : UntypedActor
	{
		protected override void OnReceive (object message)
		{
			if (message is FileMessages.ReadFile) {  // received the message that ask to read a file...
				var msg = message as FileMessages.ReadFile;
				var fileStream = new FileStream (Path.GetFullPath (msg.FileName), FileMode.Open, FileAccess.Read, FileShare.Read);
				var fileStreamReader = new StreamReader (fileStream);

				long i = 0; 
				while (!fileStreamReader.EndOfStream) {
					var row = fileStreamReader.ReadLine ();
					string[] data = row.Split (new char[] {','}, StringSplitOptions.None);
					var rowMsg = new FileMessages.RowFile (data, msg.FileName);
					if (i > 0)
						Context.ActorSelection ("/user/fileCoordinatorActor/mapFileActor").Tell (rowMsg);
					else
						Context.ActorSelection ("/user/fileCoordinatorActor/mapFileActor").Tell (rowMsg as FileMessages.FirstRow);
					Sender.Tell (rowMsg);
					i++;
				}
				Sender.Tell (new FileMessages.EndOfFile (msg.FileName, i));
			} else if (message is FileMessages.MapProcessEnded) { 
				var msg = message as FileMessages.MapProcessEnded;
				Context.ActorSelection ("/user/fileCoordinatorActor/reduceFileActor").Tell (msg);
			} else
			{
				Unhandled(message);
			}
		}
	}
}

