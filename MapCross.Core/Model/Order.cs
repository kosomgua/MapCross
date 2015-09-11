using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace MapCross.Core
{
	public class Order
	{ 
		[PrimaryKey, AutoIncrement, Column("_id")]

		public int Id { get; set; }
		public string FirstName { get; set;}
		public string LastName { get; set;}
		public string HamsterLatitude { get; set;}
		public string HamsterLongitude { get; set;}
		public HamsterColor Color { get; set;}
		public bool IsSelected { get; set; }

		public string Name
		{
			get { return string.Format ("  {0}  {1}", FirstName, LastName); }
		}

		public string ImageName 
		{
			get {
				if (Color == HamsterColor.Red)
					return "res:hamster_red_icon";
				if (Color == HamsterColor.Yellow) {
					return "res:hamster_yellow_icon";
				}
				return "res:hamster_green_icon";
			}
		}
	}
}

