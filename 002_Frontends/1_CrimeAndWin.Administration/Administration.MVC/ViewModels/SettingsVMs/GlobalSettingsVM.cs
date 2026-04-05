using System.ComponentModel.DataAnnotations;

namespace Administration.MVC.ViewModels.SettingsVMs
{
    public class GlobalSettingsVM
    {
        [Display(Name = "Bakım Modu")]
        public bool IsMaintenanceMode { get; set; } = false;

        [Display(Name = "Global Başarı Çarpanı")]
        [Range(0.1, 5.0)]
        public double SuccessRateMultiplier { get; set; } = 1.0;

        [Display(Name = "Global Cooldown Çarpanı")]
        [Range(0.1, 5.0)]
        public double CooldownMultiplier { get; set; } = 1.0;

        [Display(Name = "Minimum Aksiyon Enerjisi")]
        public int MinEnergyRequired { get; set; } = 5;

        [Display(Name = "Sistem Duyurusu")]
        public string GlobalAnnouncement { get; set; } = "";
    }
}
