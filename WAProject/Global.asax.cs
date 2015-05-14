
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Akka.Actor;

namespace WAProject
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static ActorSystem MyActorSystem;

		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

			routes.MapRoute (
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = "" }
			);

		}

		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new HandleErrorAttribute ());
		}

		protected void Application_Start ()
		{
			// Start the actor system....
			MyActorSystem = ActorSystem.Create ("MyActorSystem");

			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);
		}

		protected void Application_End()
		{
			MyActorSystem.Shutdown ();
			MyActorSystem.AwaitTermination();
		}
	}
}
