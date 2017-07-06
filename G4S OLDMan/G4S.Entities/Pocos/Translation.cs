using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class Translation : EntityBase
    {
        [Required]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        /// <summary>
        /// verzameling van messages bv errors
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// origin text
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// translated value
        /// </summary>
        public string Value { get; set; }
    }
}
