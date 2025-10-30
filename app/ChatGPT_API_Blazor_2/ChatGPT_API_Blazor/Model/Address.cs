using System.ComponentModel.DataAnnotations;

namespace ChatGPT_API_Blazor.Model
{
    public class Address
    {
        public int Id { get; set; }  // 主キーを追加
        [Required(ErrorMessage = "お名前を入力してください")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "番地・丁目などを入力してください")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "市区町村を入力してください")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "都道府県を入力してください")]
        public string Prefecture { get; set; } = string.Empty;

        [Required(ErrorMessage = "郵便番号を入力してください")]
        [RegularExpression(@"^\d{3}-\d{4}$", ErrorMessage = "郵便番号は 000-0000 の形式で入力してください")]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "電話番号を入力してください")]
        [Phone(ErrorMessage = "有効な電話番号を入力してください")]
        public string Phone { get; set; } = string.Empty;
    }
}
