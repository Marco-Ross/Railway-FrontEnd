using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Railway_FrontEnd.domain.actors
{
	class Customer
	{
		[JsonProperty]
		private int idNumber;
		[JsonProperty]
		private string name;
		[JsonProperty]
		private string surname;
		[JsonProperty]
		private int age;
		[JsonProperty]
		private double balance;

		public Customer(int idNumber, string name, string surname, int age, double balance)
		{
			this.idNumber = idNumber;
			this.name = name;
			this.surname = surname;
			this.age = age;
			this.balance = balance;
		}

		public string getName()
		{
			return name;
		}

		public string getSurname()
		{
			return surname;
		}

		public int getIdNumber()
		{
			return idNumber;
		}

		public int getAge()
		{
			return age;
		}

		public double getBalance()
		{
			return balance;
		}

	}
}
