using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway_FrontEnd.domain.transit
{
	class Station
	{
		public int stationNumber;
		public string stationName;

		public Station(int stationNumber, string stationName)
		{
			this.stationNumber = stationNumber;
			this.stationName = stationName;
		}

		public int getStationNumber()
		{
			return stationNumber;
		}

		public string getStationName()
		{
			return stationName;
		}

	}
}
