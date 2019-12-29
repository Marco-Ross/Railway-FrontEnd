using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway_FrontEnd.domain.transit
{
	class Train
	{
		public int trainNumber;
		public int capacity;

		public Train(int trainNumber, int capacity)
		{
			this.trainNumber = trainNumber;
			this.capacity = capacity;
		}
		public int getTrainNumber()
		{
			return trainNumber;
		}

		public int getCapacity()
		{
			return capacity;
		}
	}
}
