using System;
namespace LibAbstraite
{
	public abstract class AccesAbstrait
	{
		public abstract ZoneAbstraite debut { get; set; }
		public abstract ZoneAbstraite fin { get; set; }

		protected AccesAbstrait(ZoneAbstraite debut, ZoneAbstraite fin)
		{
			this.debut = debut;
			this.fin = fin;
		}
	}
}
