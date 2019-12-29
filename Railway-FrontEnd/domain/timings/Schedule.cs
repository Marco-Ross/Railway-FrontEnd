using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway_FrontEnd.domain.timings
{
	class Schedule
	{
		public virtual int scheduleID { get; set; }
		
		public virtual string departure { get; set; }
		public virtual string arrival { get; set; }

		public override string ToString()
		{
			return scheduleID + "	" + departure + "	" + arrival;
		}
	}
}
