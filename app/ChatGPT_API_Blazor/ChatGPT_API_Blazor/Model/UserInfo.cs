namespace ChatGPT_API_Blazor
{
    public class UserInfo
    {
        public int Id { get; set; }  // 主キーを追加
        public bool IsAuthenticated { get; set; }

        public string? Name { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }

    }
}
