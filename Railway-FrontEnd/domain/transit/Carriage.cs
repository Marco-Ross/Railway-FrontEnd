using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway_FrontEnd.domain.transit
{
	class Carriage
	{
		public virtual int carriageNumber { get; set; }
		public virtual int capacity { get; set; }

		public override string ToString()
		{
			return carriageNumber + " " + capacity;
 		}
	}
}
