using System;

namespace CarsLogWorkig.Models
{
    public class UserViewSession
    {
        public Guid SessionId { get; } = Guid.NewGuid(); 
        public User TargetUser { get; }
        public UserRole ObservedRole { get; }
        public DateTime StartedAt { get; } = DateTime.UtcNow;

        public UserViewSession(User targetUser)
        {
            if (targetUser == null)
                throw new ArgumentNullException(nameof(targetUser)); 
            TargetUser = targetUser;
            ObservedRole = targetUser.Role;
        }

        public override string ToString() =>
            $"[ViewSession] Observing: {TargetUser.FullName} ({ObservedRole}) since {StartedAt:dd.MM.yyyy HH:mm:ss}";
    }
}
