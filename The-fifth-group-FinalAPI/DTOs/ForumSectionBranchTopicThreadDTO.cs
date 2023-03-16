namespace The_fifth_group_FinalAPI.DTOs
    {
    public class ForumSectionBranchTopicThreadDTO
        {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int TopicState { get; set; }
        public int ReplyNumber { get; set; }
        public string? ReplyContent { get; set; }
        public DateTime ReplyTime { get; set; }
        public int ReplyState { get; set; }
        public int ReplyMemberId { get; set; }



        public int SectionNameId { get; set; }
        public string? BranchName { get; set; }

        }
    }
