using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway_FrontEnd.domain.actors
{
	class Security
	{
		[JsonProperty]
		private int employeeNumber;
		[JsonProperty]
		private string name;
		[JsonProperty]
		private string surname;

		public Security(int employeeNumber, string name, string surname)
		{
			this.employeeNumber = employeeNumber;
			this.name = name;
			this.surname = surname;
		}

		public string getName()
		{
			return name;
		}

		public string getSurname()
		{
			return surname;
		}

		public int getEmployeeNumber()
		{
			return employeeNumber;
		}
	}
}
