using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderBoard.Models
{
    public class Record
    {
        //姓名学号性别分数
        public int ID { get; set; }
        [Display(Name="学号")]
        [Required]
        [Range(0,3000000000)]
        public int Number { get; set; }
        [Display(Name="姓名")]
        [Required]

        public string Name { get; set; }
        [Display(Name ="性别")]
        [StringLength(1)]
        [Required]
        public string Sex { get; set; }
        [Display(Name ="分数")]
        [Required]

        public decimal Score { get; set; }
    }
}
