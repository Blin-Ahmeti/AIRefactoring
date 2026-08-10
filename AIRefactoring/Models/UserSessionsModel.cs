using AIRefactoring.Entities;

namespace AIRefactoring.Models
{
	public class UserSessionsModel
	{
		public List<UserSession> UserSessions { get; set; } = [];
		public Guid? CurrentSessionId { get; set; }
	}
}
