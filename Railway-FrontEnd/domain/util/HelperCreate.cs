using Railway_FrontEnd.domain.actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway_FrontEnd.domain.util
{
	class HelperCreate
	{
		public static object createObject(string actorObjectType=null, int empId =0, string empName = null, string empSname = null)
		{ // Would use SRP, but no time
			switch (actorObjectType)
			{
				case "announcer":
					Announcer announcer = new Announcer(empId, empName, empSname);
					return announcer;
				case "doorman":
					DoorMan doorMan = new DoorMan(empId, empName, empSname);
					return doorMan;
				case "driver":
					Driver driver = new Driver(empId, empName, empSname);
					return driver;
				case "security":
					Security security = new Security(empId, empName, empSname);
					return security;
				case "ticketchecker":
					TicketChecker ticketChecker = new TicketChecker(empId, empName, empSname);
					return ticketChecker;
				case "ticketclerk":
					TicketClerk ticketClerk = new TicketClerk(empId, empName, empSname);
					return ticketClerk;
			}
			return null;
		}
	}
}