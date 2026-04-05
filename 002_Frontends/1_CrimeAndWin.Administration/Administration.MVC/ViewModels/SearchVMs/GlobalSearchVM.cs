namespace Administration.MVC.ViewModels.SearchVMs
{
    public class GlobalSearchVM
    {
        public string Query { get; set; }
        public List<GlobalSearchResultVM> Results { get; set; } = new();
    }

    public class GlobalSearchResultVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } // "User", "Player", "Action", "World", etc.
        public string Icon { get; set; } // ki-duotone icon name
        public string BadgeColor { get; set; } // "primary", "success", etc.
        public string Url { get; set; }
    }
}
