using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway_FrontEnd.domain.timings
{
	class NewSchedule
	{
		public int trainNumber;
		public int capacity;
		public string departureDate;
		public string departure;
		public string arrival;

		public NewSchedule(int trainNumber, int capacity, string departureDate, string departure, string arrival)
		{
			this.trainNumber = trainNumber;
			this.capacity = capacity;
			this.departureDate = departureDate;
			this.departure = departure;
			this.arrival = arrival;
		}



	}
}
