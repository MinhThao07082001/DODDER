using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Dodder.Models
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            ConversationUserAccountId2Navigations = new HashSet<Conversation>();
            ConversationUserAccountIdCreatorNavigations = new HashSet<Conversation>();
            GenderInteresteds = new HashSet<GenderInterested>();
            MessageUserAccountIdReceiverNavigations = new HashSet<Message>();
            MessageUserAccountIdSenderNavigations = new HashSet<Message>();
            Notifications = new HashSet<Notification>();
            RelationInteresteds = new HashSet<RelationInterested>();
            UserBlockUserAccountIdBlockNavigations = new HashSet<UserBlock>();
            UserBlockUserAccounts = new HashSet<UserBlock>();
            UserDislikeUserAccountIdDislikeNavigations = new HashSet<UserDislike>();
            UserDislikeUserAccounts = new HashSet<UserDislike>();
            UserLikeUserAccountIdLikeNavigations = new HashSet<UserLike>();
            UserLikeUserAccounts = new HashSet<UserLike>();
            UserPhotos = new HashSet<UserPhoto>();
            UserReportUserAccountIdReportNavigations = new HashSet<UserReport>();
            UserReportUserAccounts = new HashSet<UserReport>();
        }

        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email not empty")]
        [RegularExpression(pattern: @"^[\w\.]+@([\w-]+\.)+[\w-]{2,3}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password not empty")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone not empty")]
        [RegularExpression(pattern: @"^[0]\d{9}$", ErrorMessage = "Phone is not valid")]
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int GenderId { get; set; }
        public DateTime Dob { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Introduce { get; set; }
        public int? MoneyLeft { get; set; }
        public string Status { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int? Authentication { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual ICollection<Conversation> ConversationUserAccountId2Navigations { get; set; }
        public virtual ICollection<Conversation> ConversationUserAccountIdCreatorNavigations { get; set; }
        public virtual ICollection<GenderInterested> GenderInteresteds { get; set; }
        public virtual ICollection<Message> MessageUserAccountIdReceiverNavigations { get; set; }
        public virtual ICollection<Message> MessageUserAccountIdSenderNavigations { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<RelationInterested> RelationInteresteds { get; set; }
        public virtual ICollection<UserBlock> UserBlockUserAccountIdBlockNavigations { get; set; }
        public virtual ICollection<UserBlock> UserBlockUserAccounts { get; set; }
        public virtual ICollection<UserDislike> UserDislikeUserAccountIdDislikeNavigations { get; set; }
        public virtual ICollection<UserDislike> UserDislikeUserAccounts { get; set; }
        public virtual ICollection<UserLike> UserLikeUserAccountIdLikeNavigations { get; set; }
        public virtual ICollection<UserLike> UserLikeUserAccounts { get; set; }
        public virtual ICollection<UserPhoto> UserPhotos { get; set; }
        public virtual ICollection<UserReport> UserReportUserAccountIdReportNavigations { get; set; }
        public virtual ICollection<UserReport> UserReportUserAccounts { get; set; }
    }
}
