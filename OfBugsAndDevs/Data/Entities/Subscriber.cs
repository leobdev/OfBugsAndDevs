using System.ComponentModel.DataAnnotations;

namespace OfBugsAndDevs.Data.Entities
{
	public class Subscriber
	{
		public long SubscriberID { get; set; }

		[Required, MaxLength(150)]
		public string Email { get; set; }

		[Required, MaxLength(25)]
		public string SubscriberName { get; set; }

		public DateTime SSubscribedOn { get; set; }



	}
}
