using AreaCarouselOrnek.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AreaCarouselOrnek.Areas.Admin.Models
{
    public class DuzenleSlaytViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} girilmesi zorunludur.")]
        public string Baslik { get; set; } = null!;

        [Required(ErrorMessage = "{0} girilmesi zorunludur.")]
        public string Aciklama { get; set; } = null!;

        [Required(ErrorMessage = "{0} girilmesi zorunludur.")]
        public int Sira { get; set; }

        public string ResimYolu { get; set; } = null!;

        [Required(ErrorMessage = "{0} yüklemek zorunludur.")]
        [GecerliResim(MaxDosyaBoyutuMb = 1.2)]
        public IFormFile Resim { get; set; } = null!;
    }
}
