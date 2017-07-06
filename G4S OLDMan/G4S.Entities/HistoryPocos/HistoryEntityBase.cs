using System;
using System.ComponentModel.DataAnnotations;

namespace G4S.Entities.HistoryPocos
{
    public class HistoryEntityBase
    {
        [Key]
        public int HistoryId { get; set; }
        public string HistoryAction { get; set; }
        public string HistoryUserName { get; set; }
        public DateTime HistoryDate { get; set; }
    }
}
