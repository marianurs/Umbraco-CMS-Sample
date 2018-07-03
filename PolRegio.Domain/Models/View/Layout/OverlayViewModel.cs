namespace PolRegio.Domain.Models.View.Layout
{
    public class OverlayViewModel
    {
        public string DesktopImageUrl { get; set; }
        public string MobileImageUrl { get; set; }
        public string ButtonUrl { get; set; }
        public bool ButtonIsNewTab { get; set; }
        public int Capping { get; set; }
        public string ImageAlt { get; set; }
    }
}