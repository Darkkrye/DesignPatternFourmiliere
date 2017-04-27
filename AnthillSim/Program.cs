using System;
using LibMetier;

namespace AnthillSim
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Simulation Fourmiliaire");
			FabriqueFourmilliere nouvelleFabrique = FabriqueFourmilliere.Instance();
		}
	}
}
