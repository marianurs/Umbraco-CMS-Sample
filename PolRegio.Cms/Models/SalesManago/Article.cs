using System.Collections.Generic;
using System.Xml.Serialization;

namespace PolRegio.Cms.Models.SalesManago
{
    /// <summary>
    /// Model artyku³u wysy³any do sales manago 
    /// </summary>
    public class Article
    {
        public Article()
        {
            Regions = new List<string>();
        }

        [XmlAttribute]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Lead { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string Url { get; set; }

        [XmlAttribute]
        public int Priority { get; set; }

        [XmlElement("Region")]
        public List<string> Regions { get; set; }

        [XmlAttribute]
        public string Lang { get; set; }
    }
}